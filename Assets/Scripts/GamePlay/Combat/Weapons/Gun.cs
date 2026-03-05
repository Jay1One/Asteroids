using Core.Configs;
using GamePlay.Combat.Bullets;
using GamePlay.Combat.Systems;
using GamePlay.Combat.Units.Player_mechanics;
using GamePlay.Pooling;
using UnityEngine;
using Zenject;

namespace GamePlay.Combat.Weapons
{
    public class Gun
    { 
        private readonly ObjectPool<PlayerBullet> _bulletPool;
        private readonly PlayerState _playerState;
        private readonly GameEndTracker _gameEndTracker;
        private readonly Transform _shootPoint;
        private readonly Vector2 _startLookDirection=Vector2.up;
        private readonly float _attackDelay;
        private readonly float _bulletSpeed;
        private readonly int _bulletDamage;
        private float _lastShotTime;

        [Inject]
        public Gun(ObjectPool<PlayerBullet> bulletPool, GameEndTracker endTracker, GunConfig config,
            PlayerState playerState, Transform shootPoint)
        {
            _shootPoint = shootPoint;
            _playerState = playerState;
            _gameEndTracker = endTracker;
            _bulletPool = bulletPool;
            _bulletSpeed = config.BulletSpeed;
            _bulletDamage = config.BulletDamage;
            _attackDelay=1/config.BulletsPerSecond;
        }
        
        public void TryShoot()
        {
            if (_playerState.IsInvincible || !(_lastShotTime + _attackDelay < Time.time) || _gameEndTracker.IsGameOver) return;
            
            PlayerBullet bullet = _bulletPool.GetObject(_shootPoint.position);
            bullet.transform.rotation = _shootPoint.rotation;
            bullet.Launch((_shootPoint.rotation*_startLookDirection), _bulletSpeed, _bulletDamage);
            _lastShotTime = Time.time;
        }
    }
}