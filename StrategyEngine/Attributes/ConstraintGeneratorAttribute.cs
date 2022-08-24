using StrategyEngine.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Attributes
{
    public class ConstraintGeneratorAttribute : Attribute
    {
        public string ActionName { get; }

        public ConstraintGeneratorAttribute(string actionName)
        {
            ActionName = actionName;
        }
    }
}
