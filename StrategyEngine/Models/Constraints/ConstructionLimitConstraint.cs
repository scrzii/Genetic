using StrategyEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Models.Constraints
{
    public class ConstructionLimitConstraint : IConstraint
    {
        private int _maxLimitCount;
        private Type _constructionType;

        public ConstructionLimitConstraint(Type construnctionType, int maxCount)
        {
            _maxLimitCount = maxCount;
            _constructionType = construnctionType;
        }

        public bool Verify(IUserContext context, IConstruction target)
        {
            return context.Constructions.GetConstructions(_constructionType).Count() < _maxLimitCount;
        }
    }
}
