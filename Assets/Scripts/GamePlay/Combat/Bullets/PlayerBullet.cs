using GamePlay.Combat.Units;
using GamePlay.Pooling;
using UnityEngine;
using Zenject;

namespace GamePlay.Combat.Bullets
{
    public class PlayerBullet : MonoBehaviour, IPoolableObject
    {
        private int _damage;
        private float _speed;
        private Vector2 _direction;
        private ObjectPool<PlayerBullet> _pool;
        
        [Inject]
        private void Construct(ObjectPool<PlayerBullet> pool)
        {
            _pool = pool;
        }
        
        public void Launch(Vector2 direction, float speed, int damage)
        {
            _speed = speed;
            _direction = direction;
            _damage = damage;
        }
        
        public void Activate()
        {
        }
        
        public void Deactivate()
        {
            _pool.Return(this);
        }
        
        private void Update()
        {
            transform.position+=(Vector3)_direction * (_speed * Time.deltaTime);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(_damage);
                _pool.Return(this);
            }
        }
    }
}