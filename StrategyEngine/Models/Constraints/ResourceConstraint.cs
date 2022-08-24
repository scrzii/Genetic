using StrategyEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Models.Constraints
{
    public class ResourceConstraint : IConstraint
    {
        private string _resourceName;
        private int _minimalAmount;

        public ResourceConstraint(string resourceName, int amount)
        {
            _resourceName = resourceName;
            _minimalAmount = amount;
        }

        public bool Verify(IUserContext context, IConstruction target)
        {
            return context.Resources.GetAmount(_resourceName) >= _minimalAmount;
        }
    }
}
