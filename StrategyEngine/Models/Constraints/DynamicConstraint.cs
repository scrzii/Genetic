using StrategyEngine.Delegates;
using StrategyEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Models.Constraints
{
    public class DynamicConstraint : IConstraint
    {
        private MethodInfo _generator;

        public DynamicConstraint(MethodInfo generator)
        {
            _generator = generator;
        }

        public bool Verify(IUserContext context, IConstruction target)
        {
            return (bool)_generator.Invoke(target, new object[] { context });
        }
    }
}
