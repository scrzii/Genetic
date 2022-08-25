using StrategyEngine.Interfaces;
using StrategyEngine.Models.Implementations;
using StrategyEngine.Models.Properties;
using StrategyEngine.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrategyEngine.Models.Actions;
using StrategyEngine.Attributes;
using System.Reflection;
using StrategyEngine.Delegates;

namespace StrategyEngine.Models
{
    public abstract class UpgradableConstruction : BaseConstruction
    {
        private readonly int _startLevelUpgrade = 2;

        protected Dictionary<int, Upgrade> _upgrades;

        public UpgradableConstruction()
        {
            SetProperty(new LevelProperty(1));
        }

        [Action]
        public void Upgrade(Context context)
        {
            var upgrade = GetUpgrade();
            upgrade.Spend(context);

            var property = GetProperty<LevelProperty>();
            property.Value++;
        }

        [Action]
        public void Destroy(Context context)
        {
            context.ConstructionsChangeable.RemoveConstruction(GetType(), this);
        }

        [ConstraintGenerator(nameof(Upgrade))]
        public bool UpgradeActionGenerator(IUserContext context)
        {
            var nextLevel = GetProperty<LevelProperty>().Value + 1;
            if (!_upgrades.ContainsKey(nextLevel))
            {
                return false;
            }

            return GetUpgrade().Constraint.Verify(context, this);
        }

        protected void SetUpgrades(params Upgrade[] upgrades)
        {
            _upgrades = Enumerable.Range(_startLevelUpgrade, upgrades.Length)
                .ToDictionary(_ => _, _ => upgrades[_ - _startLevelUpgrade]);
        }

        #region Private methods
        private Upgrade GetUpgrade()
        {
            var nextLevel = GetProperty<LevelProperty>().Value + 1;
            return _upgrades[nextLevel];
        }
        #endregion
    }
}
