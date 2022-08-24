using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Interfaces
{
    public interface IConstructionProperty
    {
        bool Verify<T>(T authoritativeProperty) where T : IConstructionProperty;
    }
}
