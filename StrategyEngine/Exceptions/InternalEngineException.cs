using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Exceptions
{
    public class InternalEngineException : FormattedException
    {
        public InternalEngineException(string text, params string[] parameters) : base(text, parameters)
        {
        }
    }
}
