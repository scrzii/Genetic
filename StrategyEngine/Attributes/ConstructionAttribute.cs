using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConstructionAttribute : Attribute
    {
        public string ConstructionName { get; }
        public int MaxCount { get; }


        /// <summary>
        /// If maxCount = -1 it means this construnction is unlimited
        /// </summary>
        /// <param name="constructionName"></param>
        /// <param name="maxCount"></param>
        public ConstructionAttribute(string constructionName, int maxCount = -1)
        {
            ConstructionName = constructionName;
            MaxCount = maxCount == -1 ? int.MaxValue : maxCount;
        }
    }
}
