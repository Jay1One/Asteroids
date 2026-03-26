using UI.ViewModels;
using Zenject;

namespace Infrastructure.Installers
{
    public class ViewModelsInstaller :MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SpeedViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CoordinatesViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<RotationViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ScoreViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HealthViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LaserStateViewModel>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EndGameViewModel>().AsSingle().NonLazy();
        }
    }
}