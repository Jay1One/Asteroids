using System;
using Core.Signals;
using GamePlay;
using MVVM;
using UniRx;
using Zenject;

namespace UI.ViewModels
{
    public class EndGameViewModel : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly SceneLoader _sceneLoader;

        public EndGameViewModel(SceneLoader sceneLoader,SignalBus signalBus)
        {
           _signalBus = signalBus;
           _sceneLoader = sceneLoader;
        }
        
        [Method("RestartButton")]
        public void OnRestartButton()
        {
            _sceneLoader.LoadBattleScene();
        }
        
       [Data("IsVisible")]
       public ReactiveProperty<bool> IsVisible = new ReactiveProperty<bool>();

        public void Initialize()
        {
            _signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDiedSignal);
            IsVisible.Value = false;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerDiedSignal>(OnPlayerDiedSignal);
        }

        private void OnPlayerDiedSignal()
        {
            IsVisible.Value = true;
        }
    }
}