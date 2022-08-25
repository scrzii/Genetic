using StrategyEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Models.Implementations
{
    public class Context : IUserContext
    {
        public ResourceStorage ResourcesChangeable { get; private set; }
        public ConstructionStorage ConstructionsChangeable { get; private set; }
        public IResourceBearer Resources
        {
            get
            {
                return ResourcesChangeable;
            }
        }
        public IConstructionBearer Constructions
        {
            get
            {
                return ConstructionsChangeable;
            }
        }

        public Context(string[] resources)
        {
            ConstructionsChangeable = new ConstructionStorage();
            ResourcesChangeable = new ResourceStorage();

            foreach (var resource in resources)
            {
                ResourcesChangeable.RegisterResource(resource);
            }
        }

        public IEnumerable<IAction> GetAvailableActions()
        {
            return GetActions().Where(_ => _.CanExecute(this));
        }

        public IEnumerable<IAction> GetActions()
        {
            return Constructions.GetConstructions().SelectMany(_ => _.GetActions(this));
        }

        public void ExecuteAction(IAction action)
        {
            action.Execute(this);
        }
    }
}
