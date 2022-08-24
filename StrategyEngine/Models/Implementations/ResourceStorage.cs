using StrategyEngine.Constants;
using StrategyEngine.Exceptions;
using StrategyEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Models
{
    public class ResourceStorage : IResourceBearer
    {
        private Dictionary<string, int> _container;

        public ResourceStorage()
        {
            _container = new Dictionary<string, int>();
        }

        public void RegisterResource(string name)
        {
            AssertUnregistered(name);

            _container[name] = 0;
        }

        public int GetAmount(string name)
        {
            AssertRegistered(name);

            return _container[name];
        }

        public void Income(string name, int amount)
        {
            AssertRegistered(name);
            AssertPositive(amount);

            _container[name] += amount;
        }

        public void Spend(string name, int amount)
        {
            AssertRegistered(name);
            AssertPositive(amount);
            AssertEnough(name, amount);

            _container[name] -= amount;
        }
        
        #region Private methods
        private bool IsRegistered(string name)
        {
            return _container.ContainsKey(name);
        }

        private void AssertRegistered(string name)
        {
            if (!IsRegistered(name))
            {
                throw new FormattedException(ExceptionTexts.ResourceIsNotRegistered, name);
            }
        }

        private void AssertUnregistered(string name)
        {
            if (IsRegistered(name))
            {
                throw new InternalEngineException(ExceptionTexts.ResourceIsRegistered, name);
            }
        }

        private void AssertPositive(int amount)
        {
            if (amount <= 0)
            {
                throw new InternalEngineException(ExceptionTexts.NegativeAmount);
            }
        }

        private void AssertEnough(string name, int amount)
        {
            if (_container[name] < amount)
            {
                throw new InternalEngineException(ExceptionTexts.NotEnoughResource, name);
            }
        }
        #endregion
    }
}
