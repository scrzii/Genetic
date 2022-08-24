using StrategyEngine.Attributes;
using StrategyEngine.Interfaces;
using StrategyEngine.Models;
using StrategyEngine.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine
{
    [Construction(nameof(BuildingB), 1)]
    public class BuildingB : BaseConstruction
    {
        public BuildingB(Context context) : base(context)
        {
        }

        [Action]
        public void Add100Gold(Context context)
        {
            context.ResourcesChangeable.Income("gold", 100);
        }

        [ConstraintGenerator(nameof(Add100Gold))]
        public bool MaxGoldConstraintGenerator(IUserContext context)
        {
            return context.Resources.GetAmount("gold") < 2000;
        }
    }
}
