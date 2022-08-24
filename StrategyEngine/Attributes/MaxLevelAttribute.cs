using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MaxLevelAttribute : Attribute
    {
        public int MaxLevel { get; }

        public MaxLevelAttribute(int maxLevel)
        {
            MaxLevel = maxLevel;
        }
    }
}
