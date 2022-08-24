using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Exceptions
{
    public class FormattedException : Exception
    {
        public FormattedException(string text, params string[] parameters)
            : base(string.Format(text, parameters))
        {
        }
    }
}
