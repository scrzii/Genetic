using StrategyEngine.Delegates;
using StrategyEngine.Interfaces;
using StrategyEngine.Models.Constraints;
using StrategyEngine.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Models.Actions
{
    public class DynamicAction : BaseAction
    {
        private BaseAction _action;
        private MethodInfo _generator;

        public DynamicAction(BaseAction action, MethodInfo generator) 
            : base(action.Target, new DynamicConstraint(generator))
        {
            _action = action;
            _generator = generator;
        }

        public override void ExecuteSafely(Context context)
        {
            _action.ExecuteSafely(context);
            UpdateConstraints();
        }

        #region Private methods
        private void UpdateConstraints()
        {
            _constraints = new List<IConstraint>
            {
                new DynamicConstraint(_generator)
            };
        }
        #endregion
    }
}
