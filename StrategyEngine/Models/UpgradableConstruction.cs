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
        private int _maxLevel;

        public UpgradableConstruction(Context context, int maxLevel = 1) : base(context)
        {
            _maxLevel = maxLevel;
            SetProperty(new LevelProperty(1));
            SetConstraintGenerator(Upgrade, UpgradeConstraintGenerator);
        }

        [Action]
        public void Upgrade(Context context)
        {
            var level = GetProperty<LevelProperty>();
            if (level == null)
            {
                throw new Exception(ExceptionTexts.HasNotLevelProperty);
            }

            level.Value++;
        }

        [Action]
        public void Destroy(Context context)
        {
            context.ConstructionsChangeable.RemoveConstruction(this);
        }

        [ConstraintGenerator(nameof(Destroy))]
        protected abstract IConstraint UpgradeConstraintGenerator(IUserContext context, IConstruction target);
    }
}
