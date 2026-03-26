using Core.Signals;
using GamePlay;
using Zenject;

namespace Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            DeclareSignals();
            
            Container.Bind<SceneLoader>().AsSingle();
        }

        private void DeclareSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<PlayerDiedSignal>();
            Container.DeclareSignal<EnemyDiedSignal>();
            Container.DeclareSignal<SceneLoadedSignal>();
        }
    }
}