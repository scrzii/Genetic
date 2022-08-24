using StrategyEngine.Interfaces;
using StrategyEngine.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Models.Actions
{
    public class MethodAction : BaseAction
    {
        private MethodInfo _method;

        public MethodAction(BaseConstruction target, MethodInfo method, params IConstraint[] constraints) 
            : base(target, constraints)
        {
            _method = method;
        }

        public override void ExecuteSafely()
        {
            _method.Invoke(Target, new object[] { Target.Context });
        }
    }
}
