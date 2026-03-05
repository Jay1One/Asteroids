using Ads;
using Ads.Appodeal;
using Zenject;

namespace Infrastructure.Installers
{
    public class AdsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AppodealAds>().AsSingle();
            Container.BindInterfacesAndSelfTo<AdsController>().AsSingle();
        }
    }
}