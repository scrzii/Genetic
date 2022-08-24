using StrategyEngine.Interfaces;
using StrategyEngine.Models.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Delegates
{
    public delegate IConstraint ConstraintGeneratorDelegate(IUserContext context, IConstruction target);
    public delegate void ActionDelegate(Context context);
}
