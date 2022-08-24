using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Constants
{
    internal class ExceptionTexts
    {
        #region InternalEngine
        public static readonly string ResourceIsNotRegistered = "Resource with name '{0}' is not registered";
        public static readonly string ResourceIsRegistered = "Resource with name '{0}' is already registered";
        public static readonly string NegativeAmount = "Resource value must be positive";
        public static readonly string NotEnoughResource = "Resource with name '{0}' is not enough";

        public static readonly string UndefinedProperty = "Property with type '{0}' not found";

        public static readonly string ProhibitedAction = "Constraint '{0}' prohibits the execution of this action";

        public static readonly string ConstructionIsNotRegistered = "Construction with type '{0}' is not registered";
        #endregion

        public static readonly string NullProperty = "Object of property '{0}' is null";
        public static readonly string NotUpgradableType = "Type '{0}' must be upgradable for this action";
        public static readonly string HasNotLevelProperty = "Instance must has LevelProperty for using this action";
        public static readonly string HasNotActionAttribute = "Method '{0}' must has ActionAttribute";
        public static readonly string InvalidUsing = "You are using this class incorrectly";
    }
}
