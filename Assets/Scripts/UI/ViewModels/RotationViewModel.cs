using System;
using GamePlay.Combat.Units.Player_mechanics;
using MVVM;
using UniRx;
using Zenject;

namespace UI.ViewModels
{
    public class RotationViewModel : IInitializable, IDisposable
    {
        private readonly PlayerState _playerState;
        
        [Data("Rotation")]
        public ReactiveProperty<string> Rotation = new ReactiveProperty<string>();

        public RotationViewModel(PlayerState playerState)
        {
            _playerState = playerState;
        }

        private void OnRotationChanged(float rotation)
        {
            Rotation.Value = rotation.ToString("F0") +" \u00b0";
        }

        public void Initialize()
        {
            _playerState.RotationChanged += OnRotationChanged;
        }

        public void Dispose()
        {
            _playerState.RotationChanged -= OnRotationChanged;
        }
    }
}