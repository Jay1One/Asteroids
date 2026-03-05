using AppodealStack.Monetization.Common;
using Zenject;

namespace Ads.Appodeal
{
    public class AppodealAds: IAdsProvider, IInitializable
    {
        public void Initialize()
        {
            int adTypes = AppodealAdType.Interstitial;
            string appKey = "YOUR_APPODEAL_APP_KEY";
            AppodealStack.Monetization.Api.Appodeal.SetTesting(true);
            AppodealStack.Monetization.Api.Appodeal.Initialize(appKey, adTypes);
        }

        public void ShowInterstitialAd()
        {
            AppodealStack.Monetization.Api.Appodeal.Show(AppodealShowStyle.Interstitial);
        }

        public bool IsInterstitialLoaded()
        {
            return AppodealStack.Monetization.Api.Appodeal.IsLoaded(AppodealAdType.Interstitial);
        }
    }
}