using Core.Configs;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class ConfigInstaller : MonoInstaller
    {
        [SerializeField] private TextAsset _playerSettingsJson;
        [SerializeField] private TextAsset _laserConfigJson;
        [SerializeField] private TextAsset _gunConfigJson;
        
        [SerializeField] private TextAsset _gameFieldSettingsJson;
        [SerializeField] private TextAsset _AsteroidSettingsJson;
        [SerializeField] private TextAsset _AsteroidShardSettingsJson;
        [SerializeField] private TextAsset _UFOSettingsJson;
        

        public override void InstallBindings()
        {
            Container.Bind<PlayerConfig>().FromMethod(ctx => 
                JsonConvert.DeserializeObject<PlayerConfig>(_playerSettingsJson.text)).AsSingle();
            
            Container.Bind<GunConfig>().FromMethod(ctx => 
                JsonConvert.DeserializeObject<GunConfig>(_gunConfigJson.text)).AsSingle();
            
            Container.Bind<LaserConfig>().FromMethod(ctx => 
                JsonConvert.DeserializeObject<LaserConfig>(_laserConfigJson.text)).AsSingle();
            
            Container.Bind<GameFieldConfig>().FromMethod(ctx => 
                JsonConvert.DeserializeObject<GameFieldConfig>(_gameFieldSettingsJson.text)).AsSingle();
            
            Container.Bind<AsteroidConfig>().FromMethod(ctx => 
                JsonConvert.DeserializeObject<AsteroidConfig>(_AsteroidSettingsJson.text)).AsSingle();
            
            Container.Bind<AsteroidShardConfig>().FromMethod(ctx => 
                JsonConvert.DeserializeObject<AsteroidShardConfig>(_AsteroidShardSettingsJson.text)).AsSingle();
            
            Container.Bind<UfoConfig>().FromMethod(ctx => 
                JsonConvert.DeserializeObject<UfoConfig>(_UFOSettingsJson.text)).AsSingle();
        }
    }
}