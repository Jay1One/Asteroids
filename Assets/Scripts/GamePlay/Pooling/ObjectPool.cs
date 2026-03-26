using System.Collections.Generic;
using GamePlay.Factories;
using UnityEngine;
using Zenject;

namespace GamePlay.Pooling
{
    public class ObjectPool<T> :IInitializable where T : MonoBehaviour, IPoolableObject
    {
        private readonly int _capacity;
        private Stack<T> _pool;
        private readonly ObjectFactory<T> _objectFactory;
        
        [Inject]
        public ObjectPool(ObjectFactory<T> objectFactory, int capacity)
        {
            _objectFactory = objectFactory;
            _capacity = capacity;
        }

        public void Return(T obj)
        {
            _pool.Push(obj);
            obj.gameObject.SetActive(false);
        }
    
        public T GetObject()
        {
            if (_pool.Count == 0)
            {
                _pool.Push(_objectFactory.Create());
            }
        
            var obj = _pool.Pop();
            obj.gameObject.SetActive(true);
            obj.Activate();
            return obj;
        }

        public void Initialize()
        {
            _pool = new Stack<T>(_capacity);
        
            for (int i = 0; i < _capacity; i++)
            {
                T obj = _objectFactory.Create();
                Return(obj);
            }
        }
    }
}