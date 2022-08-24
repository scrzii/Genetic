using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ActionAttribute : Attribute
    {
        public string Name { get; private set; }

        public ActionAttribute([CallerMemberName]string name = null)
        {
            Name = name;
        }
    }
}
