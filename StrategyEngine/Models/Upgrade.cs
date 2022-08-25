using StrategyEngine.Interfaces;
using StrategyEngine.Models.Constraints;
using StrategyEngine.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Models
{
    public class Upgrade
    {
        private Dictionary<string, int> _price;

        public IConstraint Constraint { get; }

        public Upgrade(Dictionary<string, int> price, params IConstraint[] constraints)
        {
            _price = price;
            var constraintList = constraints.ToList();
            constraintList.AddRange(price.Select(_ => new ResourceConstraint(_.Key, _.Value)));
            Constraint = new ConstraintComposite(constraintList.ToArray());
        }

        public void Spend(Context context)
        {
            foreach (var resource in _price)
            {
                context.ResourcesChangeable.Spend(resource.Key, resource.Value);
            }
        }
    }
}
