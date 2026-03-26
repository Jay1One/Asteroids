using Core.Configs;
using GamePlay.Combat.Bullets;
using GamePlay.Combat.Systems;
using GamePlay.Combat.Units;
using GamePlay.Pooling;
using UnityEngine;
using Zenject;

namespace GamePlay.Combat.Weapons
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform _shootPoint;
        private ObjectPool<PlayerBullet> _bulletPool;
        private Player _player;
        private GameEndTracker _gameEndTracker;
        private readonly Vector2 _startLookDirection=Vector2.up;
        private float _bulletsPerSecond;
        private float _attackDelay;
        private float _bulletSpeed;
        private int _bulletDamage;
        private float _lastShotTime;

        [Inject]
        private void Construct(ObjectPool<PlayerBullet> bulletPool, GameEndTracker endTracker, GunConfig config, Player player)
        {
            _player = player;
            _gameEndTracker = endTracker;
            _bulletPool = bulletPool;
            _bulletsPerSecond = config.BulletsPerSecond;
            _bulletSpeed = config.BulletSpeed;
            _bulletDamage = config.BulletDamage;
            _attackDelay=1/_bulletsPerSecond;
        }
        
        public void TryShoot()
        {
            if (_player.IsInvincible || !(_lastShotTime + _attackDelay < Time.time) || _gameEndTracker.IsGameOver) return;
            
            PlayerBullet bullet = _bulletPool.GetObject();
            bullet.transform.position = _shootPoint.position;
            bullet.transform.rotation = transform.rotation;
            bullet.Launch((transform.rotation*_startLookDirection), _bulletSpeed, _bulletDamage);
            _lastShotTime = Time.time;
        }
    }
}