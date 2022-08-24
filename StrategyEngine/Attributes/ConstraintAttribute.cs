using StrategyEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class ConstraintAttribute : Attribute
    {
        public IConstraint Target { get; }

        public ConstraintAttribute(Type constraintType, params object[] parameters)
        {
            Target = (IConstraint)Activator.CreateInstance(constraintType, parameters);
        }
    }
}
