using System;
using Core.Signals;
using UnityEngine;
using Zenject;

namespace GamePlay.Combat.Systems
{
    public class GameEndTracker : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        
        public bool IsGameOver { get; private set; }

        public GameEndTracker(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            Time.timeScale = 1;
            _signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDied);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerDiedSignal>(OnPlayerDied);
        }

        private void OnPlayerDied(PlayerDiedSignal signal)
        {
            Time.timeScale = 0;
            IsGameOver = true;
        }
    }
}