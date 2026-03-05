namespace Ads
{
    public interface IAdsProvider
    {
        public void ShowInterstitialAd();
        public bool IsInterstitialLoaded();
    }
}