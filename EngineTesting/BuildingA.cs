using StrategyEngine.Attributes;
using StrategyEngine.Models;
using StrategyEngine.Models.Constraints;
using StrategyEngine.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine
{
    [Construction(nameof(BuildingA))]
    [ResourceCost("gold", 1000)]
    public class BuildingA : BaseConstruction
    {
        public BuildingA(Context context) : base(context)
        {
        }

        [Action]
        public void WriteHelloWorld(Context context)
        {
            Console.WriteLine("Hello world");
        }
    }
}
