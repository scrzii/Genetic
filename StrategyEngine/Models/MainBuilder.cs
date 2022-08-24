using StrategyEngine.Attributes;
using StrategyEngine.Constants;
using StrategyEngine.Interfaces;
using StrategyEngine.Models.Actions;
using StrategyEngine.Models.Constraints;
using StrategyEngine.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Models
{
    public class MainBuilder : BaseConstruction
    {
        public MainBuilder(Context context, Assembly assembly) : base(context)
        {
            InitActions(assembly);
        }

        private void InitActions(Assembly assembly)
        {
            _actions = new List<IAction>();

            var types = assembly.GetTypes()
                .Where(_ => Attribute.GetCustomAttribute(_, typeof(ConstructionAttribute)) != null).ToList();
            foreach (var type in types)
            {
                var createAction = new CreateAction(this, type, GetPrice(type), GetConstructionCreateConstraints(type).ToArray());
                createAction.ActionName = $"Create_{GetConstructionName(type)}";
                _actions.Add(createAction);
            }
        }

        #region Private methods
        private IEnumerable<IConstraint> GetConstructionCreateConstraints(Type type)
        {
            var constraints = Attribute.GetCustomAttributes(type, typeof(ConstraintAttribute)).Cast<ConstraintAttribute>();
            foreach (var attribute in constraints)
            {
                yield return attribute.Target;
            }

            foreach (var resource in GetPrice(type))
            {
                yield return new ResourceConstraint(resource.Key, resource.Value);
            }

            yield return new ConstructionLimitConstraint(type, GetMaxCount(type));
        }

        private string GetConstructionName(Type constructionType)
        {
            return constructionType.GetCustomAttribute<ConstructionAttribute>().ConstructionName;
        }

        private int GetMaxCount(Type constructionType)
        {
            return constructionType.GetCustomAttribute<ConstructionAttribute>().MaxCount;
        }

        private Dictionary<string, int> GetPrice(Type type)
        {
            var costs = Attribute.GetCustomAttributes(type, typeof(ResourceCostAttribute)).Cast<ResourceCostAttribute>();
            return costs.ToDictionary(_ => _.Resource, _ => _.Amount);
        }
        #endregion
    }
}
