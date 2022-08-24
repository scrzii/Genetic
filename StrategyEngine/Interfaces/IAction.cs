using StrategyEngine.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Interfaces
{
    public interface IAction
    {
        string ActionName { get; }

        bool CanExecute();
        void Execute();
    }
}
