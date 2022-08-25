using StrategyEngine.Attributes;
using StrategyEngine.Interfaces;
using StrategyEngine.Models;
using StrategyEngine.Models.Constraints;
using StrategyEngine.Models.Implementations;
using StrategyEngine.Models.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine
{
    [Construction(nameof(BuildingB), 1)]
    public class BuildingB : UpgradableConstruction
    {
        public BuildingB() : base()
        {
            var upgrades = new Upgrade[]
            {
                new Upgrade(new Dictionary<string, int> { ["gold"] = 500 }),
                new Upgrade(new Dictionary<string, int> { ["gold"] = 1000 })
            };
            SetUpgrades(upgrades);
        }

        [Action]
        public void AddGold(Context context)
        {
            var toIncome = GetProperty<LevelProperty>().Value * 100;
            context.ResourcesChangeable.Income("gold", toIncome);
        }

        [ConstraintGenerator(nameof(AddGold))]
        public bool MaxGoldConstraintGenerator(IUserContext context)
        {
            return context.Resources.GetAmount("gold") < 2000;
        }
    }
}
