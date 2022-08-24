using StrategyEngine.Attributes;
using StrategyEngine.Constants;
using StrategyEngine.Delegates;
using StrategyEngine.Exceptions;
using StrategyEngine.Interfaces;
using StrategyEngine.Models.Actions;
using StrategyEngine.Models.Constraints;
using StrategyEngine.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Models
{
    public abstract class BaseConstruction : IConstruction
    {
        public Context Context { get; }
        public string Name { get; }

        protected Dictionary<Type, IConstructionProperty> _properties;

        protected List<IAction> _actions;
        private Dictionary<string, ConstraintGeneratorDelegate> _constraintGenerators;

        public BaseConstruction(Context context, [CallerMemberName]string name = null)
        {
            Name = GetConstructionName();
            Context = context;

            _properties = new Dictionary<Type, IConstructionProperty>();
            _actions = GetCustomActions().ToList();
            _constraintGenerators = new Dictionary<string, ConstraintGeneratorDelegate>();
        }

        public IEnumerable<IAction> GetActions(IUserContext context)
        {
            return _actions.Where(_ => _.CanExecute());
        }

        public T GetProperty<T>() where T : IConstructionProperty
        {
            return (T)GetProperty(typeof(T));
        }

        public IConstructionProperty GetProperty(Type propertyType)
        {
            if (!_properties.ContainsKey(propertyType))
            {
                return null;
            }
            return _properties[propertyType];
        }

        public void SetProperty<T>(T value) where T : IConstructionProperty
        {
            _properties[typeof(T)] = value;
        }


        protected void SetConstraintGenerator(ActionDelegate action, ConstraintGeneratorDelegate generator)
        {
            var method = action.Method;
            var attribute = method.GetCustomAttribute<ActionAttribute>();
            if (attribute == null)
            {
                throw new FormattedException(ExceptionTexts.HasNotActionAttribute, method.Name);
            }

            _constraintGenerators[attribute.Name] = generator;
        }

        #region Private methods
        private IEnumerable<IAction> GetCustomActions()
        {
            var type = GetType();

            var actions = GetMethodsWithAttrubute<ActionAttribute>(type)
                .ToDictionary(_ => GetActionName(_));

            var generators = GetMethodsWithAttrubute<ConstraintGeneratorAttribute>(type)
                .ToDictionary(_ => _.GetCustomAttribute<ConstraintGeneratorAttribute>().ActionName);

            var constraints = GetMethodsWithAttrubute<ConstraintAttribute>(type)
                .Select(_ => (_.GetCustomAttribute<ConstraintAttribute>(), GetActionName(_)))
                .GroupBy(_ => _.Item2)
                .ToDictionary(_ => _.Key, _ => _.Select(x => x.Item1.Target));

            foreach (var pair in actions)
            {
                var name = pair.Key;
                var method = pair.Value;
                if (generators.ContainsKey(name))
                {
                    yield return GenerateDynamicConstraintAction(method, generators[name]);
                    continue;
                }

                var costs = Attribute.GetCustomAttributes(method, typeof(ResourceCostAttribute));
                yield return constraints.ContainsKey(name)
                    ? GenerateDefaultAction(method, constraints[name].Union(GetResourceCostConstraints(method)).ToArray())
                    : GenerateDefaultAction(method, GetResourceCostConstraints(method).ToArray());
            }
        }

        private IAction GenerateDynamicConstraintAction(MethodInfo action, MethodInfo generator)
        {
            var result = new DynamicAction(new MethodAction(this, action), generator);
            result.ActionName = $"{GetActionName(action)}_{Name}";
            return result;
        }

        private IAction GenerateDefaultAction(MethodInfo action, params IConstraint[] constraints)
        {
            var result = new MethodAction(this, action, constraints);
            result.ActionName = $"{GetActionName(action)}_{Name}";
            return result;
        }

        private IEnumerable<MethodInfo> GetMethodsWithAttrubute<T>(Type type) where T : Attribute
        {
            return type.GetMethods().Where(_ => _.GetCustomAttribute<T>() != null);
        }

        private string GetActionName(MethodInfo action)
        {
            return action.GetCustomAttribute<ActionAttribute>().Name;
        }

        private string GetConstructionName()
        {
            var type = GetType();
            return type.GetCustomAttribute<ConstructionAttribute>()?.ConstructionName ?? type.Name;
        }

        private IEnumerable<IConstraint> GetResourceCostConstraints(MethodInfo action)
        {
            var costs = Attribute.GetCustomAttributes(action, typeof(ResourceCostAttribute)).Cast<ResourceCostAttribute>();
            foreach (var resource in costs)
            {
                yield return new ResourceConstraint(resource.Resource, resource.Amount);
            }
        }
        #endregion
    }
}
