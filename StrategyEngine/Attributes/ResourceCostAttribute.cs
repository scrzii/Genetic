using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ResourceCostAttribute : Attribute
    {
        public string Resource;
        public int Amount;

        public ResourceCostAttribute(string resource, int amount)
        {
            Resource = resource;
            Amount = amount;
        }
    }
}
