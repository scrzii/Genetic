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

        public BaseAction(BaseConstruction target, params IConstraint[] constraints)
        {
            _constraints = constraints.ToList() ?? new List<IConstraint>();
            Target = target;
            ActionName = $"{nameof(BaseAction)}_{target.Name}";
        }

        public bool CanExecute(IUserContext context)
        {
            return _constraints.All(_ => _.Verify(context, Target));
        }

        public void Execute(Context context)
        {
            if (!CanExecute(context))
            {
                throw new InternalEngineException(ExceptionTexts.ProhibitedAction);
            }

            ExecuteSafely(context);
        }

        public abstract void ExecuteSafely(Context context);
    }
}
