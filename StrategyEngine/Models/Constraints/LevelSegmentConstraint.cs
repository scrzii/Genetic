using StrategyEngine.Constants;
using StrategyEngine.Exceptions;
using StrategyEngine.Interfaces;
using StrategyEngine.Models.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Models.Constraints
{
    public class LevelSegmentConstraint : IConstraint
    {
        private int _minLevel;
        private int _maxLevel;

        public LevelSegmentConstraint(int minLevel, int maxLevel)
        {
            _minLevel = minLevel;
            _maxLevel = maxLevel;
        }

        public bool Verify(IUserContext context, IConstruction target)
        {
            var targetLevel = target.GetProperty<LevelProperty>();
            if (targetLevel == null)
            {
                throw new FormattedException(ExceptionTexts.HasNotLevelProperty);
            }

            return targetLevel.Value >= _minLevel && targetLevel.Value <= _maxLevel;
        }
    }
}
