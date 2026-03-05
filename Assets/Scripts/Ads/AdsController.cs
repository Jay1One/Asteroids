using System;
using Core.Signals;
using UnityEngine;
using Zenject;

namespace Ads
{
    public class AdsController :IInitializable, IDisposable
    {
        private readonly IAdsProvider _adsProvider;
        private readonly SignalBus _signalBus;
        private readonly float _interstitialCooldown = 180f;
        
        private float _lastInterstitialTime;
        private int _interstitialsShown;

        public AdsController(IAdsProvider adsProvider, SignalBus signalBus)
        {
            _adsProvider = adsProvider;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<SceneLoadedSignal>(OnSceneloaded);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<SceneLoadedSignal>(OnSceneloaded);
        }

        private void OnSceneloaded()
        {
            if (_interstitialsShown == 0)
            {
                TryShowInterstitial();
            }
            else
            {
                if (_lastInterstitialTime + _interstitialCooldown > Time.time)
                {
                    TryShowInterstitial();
                }
            }
        }

        private void TryShowInterstitial()
        {
            if (!_adsProvider.IsInterstitialLoaded()) return;
            
            _adsProvider.ShowInterstitialAd();
            _interstitialsShown++;
            _lastInterstitialTime = Time.time;
        }
    }
}