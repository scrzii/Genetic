using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Interfaces
{
    public interface IConstraint
    {
        bool Verify(IUserContext context, IConstruction target);
    }
}
