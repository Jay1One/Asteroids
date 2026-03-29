using System;
using GamePlay.Combat.Units.Player_mechanics;
using MVVM;
using UniRx;
using Zenject;

namespace UI.ViewModels
{
    public class SpeedViewModel :IInitializable, IDisposable
    {
        private readonly PlayerMovement _playerMovement;
        
        [Data ("Speed")]
        public ReactiveProperty<string> Speed = new ReactiveProperty<string>();
        
        public SpeedViewModel(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
        }

        public void Dispose()
        {
            _playerMovement.SpeedChanged -= OnSpeedChanged;
        }

        public void Initialize()
        {
            _playerMovement.SpeedChanged += OnSpeedChanged;
        }

        private void OnSpeedChanged(float speed)
        {
            Speed.Value = speed.ToString("0.00");
        }
    }
}