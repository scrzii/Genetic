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

        public override void ExecuteSafely(Context context)
        {
            var construction = (IConstruction)Activator.CreateInstance(_constructionType);
            SpendResources(context);
            context.ConstructionsChangeable.AddConstruction(_constructionType, construction);
        }

        #region Private methods
        private void SpendResources(Context context)
        {
            foreach (var pair in _price)
            {
                context.ResourcesChangeable.Spend(pair.Key, pair.Value);
            }
        }
        #endregion
    }
}
