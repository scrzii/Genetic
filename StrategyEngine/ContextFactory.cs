using StrategyEngine.Interfaces;
using StrategyEngine.Models;
using StrategyEngine.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine
{
    public class ContextFactory
    {
        public static IUserContext Create(string[] resources)
        {
            var result = new Context(resources);
            var assembly = Assembly.GetCallingAssembly();
            result.ConstructionsChangeable.AddConstruction(new MainBuilder(result, assembly));
            return result;
        }
    }
}
