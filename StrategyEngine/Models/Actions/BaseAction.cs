using StrategyEngine.Constants;
using StrategyEngine.Exceptions;
using StrategyEngine.Interfaces;
using StrategyEngine.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Models.Actions
{
    public abstract class BaseAction : IAction
    {
        public BaseConstruction Target;
        public string ActionName { get; set; }

        protected List<IConstraint> _constraints;
        protected Context _context
        {
            get
            {
                return Target.Context;
            }
        }

        public BaseAction(BaseConstruction target, params IConstraint[] constraints)
        {
            _constraints = constraints.ToList() ?? new List<IConstraint>();
            Target = target;
            ActionName = $"{nameof(BaseAction)}_{target.Name}";
        }

        public bool CanExecute()
        {
            return _constraints.All(_ => _.Verify(Target.Context, Target));
        }

        public void Execute()
        {
            if (!CanExecute())
            {
                throw new InternalEngineException(ExceptionTexts.ProhibitedAction);
            }

            ExecuteSafely();
        }

        public abstract void ExecuteSafely();
    }
}
