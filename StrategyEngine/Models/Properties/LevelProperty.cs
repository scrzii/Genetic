using StrategyEngine.Constants;
using StrategyEngine.Exceptions;
using StrategyEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Models.Properties
{
    public class LevelProperty : IConstructionProperty
    {
        public int Value;

        public LevelProperty(int value)
        {
            Value = value;
        }

        public bool Verify<T>(T authoritativeProperty) where T : IConstructionProperty
        {
            var subset = authoritativeProperty as LevelProperty;
            if (subset == null)
            {
                throw new FormattedException(ExceptionTexts.NullProperty, typeof(T).Name);
            }

            return Value >= subset.Value;
        }
    }
}
