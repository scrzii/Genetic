using StrategyEngine.Interfaces;
using StrategyEngine.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Models.Actions
{
    public class CreateAction : BaseAction
    {
        private Type _constructionType;
        private Dictionary<string, int> _price;

        public CreateAction(BaseConstruction target, Type constructionType, Dictionary<string, int> price, params IConstraint[] constraints) 
            : base(target, constraints)
        {
            _price = price ?? new Dictionary<string, int>();
            _constructionType = constructionType;
        }

        public override void ExecuteSafely()
        {
            var construction = (IConstruction)Activator.CreateInstance(_constructionType, _context);
            SpendResources();
            AddFromReflection(Target.Context, construction);
        }

        #region Private methods
        private void AddFromReflection(Context context, IConstruction target)
        {
            var storage = context.ConstructionsChangeable;
            var type = typeof(ConstructionStorage);
            var methodAdd = type.GetMethod(nameof(storage.AddConstruction)).MakeGenericMethod(_constructionType);
            methodAdd.Invoke(storage, new object[] { target });
        }

        private void SpendResources()
        {
            foreach (var pair in _price)
            {
                _context.ResourcesChangeable.Spend(pair.Key, pair.Value);
            }
        }
        #endregion
    }
}
