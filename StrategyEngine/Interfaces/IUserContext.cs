using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Interfaces
{
    public interface IUserContext
    {
        IResourceBearer Resources { get; }
        IConstructionBearer Constructions { get; }
        IEnumerable<IAction> GetActions();
    }
}
