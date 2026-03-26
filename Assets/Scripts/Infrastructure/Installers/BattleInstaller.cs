using GamePlay.Combat.Bullets;
using GamePlay.Combat.Systems;
using GamePlay.Combat.Units;
using GamePlay.Combat.Weapons;
using GamePlay.Factories;
using GamePlay.Pooling;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class BattleInstaller : MonoInstaller
    { 
        [SerializeField] private Player _player;
        [SerializeField] private Gun _gun;
        [SerializeField] private Laser _laser;
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private Ufo _ufoPrefab;
        [SerializeField] private AsteroidShard _asteroidShardPrefab;
        [SerializeField] private GameField _gameField;
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
            Container.Bind<Player>().FromInstance(_player).AsSingle();
            Container.Bind<Gun>().FromInstance(_gun).AsSingle();
            Container.Bind<Laser>().FromInstance(_laser).AsSingle();
        }
        
        private void BindSystems()
        {
            Container.BindInterfacesAndSelfTo<Spawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameField>().FromInstance(_gameField).AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreCalculator>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameEndTracker>().AsSingle();
        }
    }
}