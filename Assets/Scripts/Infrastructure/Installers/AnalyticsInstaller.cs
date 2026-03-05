using Analytics;
using Zenject;

namespace Infrastructure.Installers
{
    public class AnalyticsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<FirebaseAnalytics>().AsSingle();
        }
    }
}