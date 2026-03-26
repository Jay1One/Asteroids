using UnityEngine;
using Zenject;

namespace GamePlay.Factories
{
    public class ObjectFactory<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly DiContainer _container;
        
        public ObjectFactory(T prefab, DiContainer container)
        {
            _prefab = prefab;
            _container = container;
        }

        public T Create()
        {
            return _container.InstantiatePrefab(_prefab).GetComponent<T>();
        }
    }
}