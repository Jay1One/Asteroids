using System;
using GamePlay.Combat.Units;
using MVVM;
using UniRx;
using Zenject;

namespace UI.ViewModels
{
    public class SpeedViewModel :IInitializable, IDisposable
    {
        private readonly Player _player;
        
        [Data ("Speed")]
        public ReactiveProperty<string> Speed = new ReactiveProperty<string>();
        
        public SpeedViewModel(Player player)
        {
            _player = player;
        }

        public void Dispose()
        {
            _player.SpeedChanged -= OnSpeedChanged;
        }

        public void Initialize()
        {
            _player.SpeedChanged += OnSpeedChanged;
        }

        private void OnSpeedChanged(float speed)
        {
            Speed.Value = speed.ToString("0.00");
        }
    }
}