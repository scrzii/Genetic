using StrategyEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Models.Constraints
{
    public class ConstraintComposite : IConstraint
    {
        private List<IConstraint> _constraints;

        public ConstraintComposite(params IConstraint[] constraints)
        {
            _constraints = constraints.ToList();
        }

        public bool Verify(IUserContext context, IConstruction target)
        {
            return _constraints.All(_ => _.Verify(context, target));
        }
    }
}
