using StrategyEngine.Constants;
using StrategyEngine.Exceptions;
using StrategyEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyEngine.Models.Implementations
{
    public class ConstructionStorage : IConstructionBearer
    {
        private Dictionary<Type, List<IConstruction>> _container;

        public ConstructionStorage()
        {
            _container = new Dictionary<Type, List<IConstruction>>();
        }

        public IEnumerable<T> GetConstructions<T>() where T : IConstruction
        {
            return GetConstructions(typeof(T)).Cast<T>();
        }

        public IEnumerable<IConstruction> GetConstructions(Type construnctionType)
        {
            return IsRegistered(construnctionType) 
                ?  _container[construnctionType] 
                : new List<IConstruction> { };
        }

        public IEnumerable<IConstruction> GetConstructions()
        {
            return _container.SelectMany(_ => _.Value);
        }

        public void AddConstruction<T>(T construction) where T : IConstruction
        {
            AddConstruction(typeof(T), construction);
        }

        public void AddConstruction(Type type, IConstruction construction)
        {
            if (!IsRegistered(type))
            {
                _container[type] = new List<IConstruction>();
            }
            _container[type].Add(construction);
        }

        public void RemoveConstruction(Type type, IConstruction construction)
        {
            AssertRegistered(type);

            _container[type].Remove(construction);

            if (!_container[type].Any())
            {
                _container.Remove(type);
            }
        }

        public void RemoveConstruction<T>(T construction) where T : IConstruction
        {
            RemoveConstruction(typeof(T), construction);
        }

        public IEnumerable<Type> GetTypes()
        {
            return _container.Keys;
        }

        #region Private methods
        private bool IsRegistered(Type type)
        {
            return _container.ContainsKey(type);
        }

        private void AssertRegistered<T>() where T : IConstruction
        {
            AssertRegistered(typeof(T));
        }

        private void AssertRegistered(Type type)
        {
            if (!IsRegistered(type))
            {
                throw new InternalEngineException(ExceptionTexts.ConstructionIsNotRegistered, type.Name);
            }
        }
        #endregion
    }
}
