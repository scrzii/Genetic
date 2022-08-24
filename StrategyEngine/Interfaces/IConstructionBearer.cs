using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Interfaces
{
    public interface IConstructionBearer
    {
        IEnumerable<T> GetConstructions<T>() where T : IConstruction;
        IEnumerable<IConstruction> GetConstructions(Type construnctionType);
        IEnumerable<Type> GetTypes();
        IEnumerable<IConstruction> GetConstructions();
    }
}
