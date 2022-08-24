using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Interfaces
{
    public interface IConstruction
    {
        string Name { get; }

        IEnumerable<IAction> GetActions(IUserContext context);
        T GetProperty<T>() where T : IConstructionProperty;
        IConstructionProperty GetProperty(Type propertyType);
    }
}
