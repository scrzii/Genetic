using StrategyEngine.Enums;
using StrategyEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Models.Constraints
{
    public class RequirementConstraint<T> : IConstraint where T : IConstruction
    {
        private List<IConstructionProperty> _requiredProperties;
        private RequirementType _constraintType;

        public RequirementConstraint(RequirementType type = RequirementType.Any, params IConstructionProperty[] properties)
        {
            _requiredProperties = properties.ToList();
            _constraintType = type;
        }

        public bool Verify(IUserContext context, IConstruction target)
        {
            if (_constraintType == RequirementType.Target)
            {
                return VerifyAllParameters(target);
            }

            return context.Constructions.GetConstructions<T>()
                .Any(_ => VerifyAllParameters(_));
        }

        #region Private methods
        private bool VerifyAllParameters(IConstruction target)
        {
            foreach (var property in _requiredProperties)
            {
                var targetProperty = target.GetProperty(property.GetType());
                if (targetProperty == null || targetProperty.Verify(property))
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
    }
}
