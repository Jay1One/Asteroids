using GamePlay.Combat.Bullets;
using GamePlay.Combat.Systems;
using GamePlay.Combat.Units.Enemies;
using GamePlay.Combat.Units.Player_mechanics;
using GamePlay.Combat.Weapons;
using GamePlay.Factories;
using GamePlay.Physics;
using GamePlay.Pooling;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class BattleInstaller : MonoInstaller
    { 
        [SerializeField] private Player player;
        [SerializeField] private Transform _gunShootPoint;
        [SerializeField] private LaserBeam _laserBeam;
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private Ufo _ufoPrefab;
        [SerializeField] private AsteroidShard _asteroidShardPrefab;
        [SerializeField] private GameFieldMonoBehaviour _gameFieldMonoBehaviour;
        [SerializeField] private PlayerBullet _playerBulletPrefab;
        
        [SerializeField] private int _asteroidPoolSize;
        [SerializeField] private int _bulletPoolSize;
        [SerializeField] private int _asteroidShardPoolSize;
        [SerializeField] private int _ufoPoolSize;
    
        public override void InstallBindings()
        {
            BindPools();
            BindPlayer();
            BindSystems();
        }

        private void BindPools()
        {
            Container.Bind<ObjectFactory<Asteroid>>().AsSingle().WithArguments(_asteroidPrefab, Container);
            Container.Bind<ObjectFactory<AsteroidShard>>().AsSingle().WithArguments(_asteroidShardPrefab, Container);
            Container.Bind<ObjectFactory<Ufo>>().AsSingle().WithArguments(_ufoPrefab, Container);
            Container.Bind<ObjectFactory<PlayerBullet>>().AsSingle().WithArguments(_playerBulletPrefab, Container);
           
            Container.BindInterfacesAndSelfTo<ObjectPool<Asteroid>>().AsSingle().WithArguments(_asteroidPoolSize);
            Container.BindInterfacesAndSelfTo<ObjectPool<AsteroidShard>>().AsSingle().WithArguments(_asteroidShardPoolSize);
            Container.BindInterfacesAndSelfTo<ObjectPool<Ufo>>().AsSingle().WithArguments(_ufoPoolSize);
            Container.BindInterfacesAndSelfTo<ObjectPool<PlayerBullet>>().AsSingle().WithArguments(_bulletPoolSize);
        }
        
        private void BindPlayer()
        {
            Container.Bind<Player>().FromInstance(player).AsSingle();
            Container.Bind<LaserBeam>().FromInstance(_laserBeam).AsSingle();
            Container.Bind<Gun>().AsSingle().WithArguments(_gunShootPoint).NonLazy();
            Container.BindInterfacesAndSelfTo<Laser>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMovement>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerState>().AsSingle();
            Container.Bind<PhysicsBody>().FromComponentOn(player.gameObject).AsSingle().WhenInjectedInto<PlayerMovement>();
            Container.Bind<Transform>().FromComponentOn(player.gameObject).WhenInjectedInto<PlayerState>();
            Container.Bind<Transform>().FromComponentOn(player.gameObject).WhenInjectedInto<PlayerMovement>();
            Container.Bind<PlayerHealthService>().AsSingle();
        }
        
        private void BindSystems()
        {
            Container.BindInterfacesAndSelfTo<Spawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameField>().AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreCalculator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameEndTracker>().AsSingle();
            Container.Bind<ShardSpawner>().AsSingle();
        }
    }
}