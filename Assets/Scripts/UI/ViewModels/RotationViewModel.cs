using System;
using GamePlay.Combat.Units;
using MVVM;
using UniRx;
using Zenject;

namespace UI.ViewModels
{
    public class RotationViewModel : IInitializable, IDisposable
    {
        private readonly Player _player;
        
        [Data("Rotation")]
        public ReactiveProperty<string> Rotation = new ReactiveProperty<string>();

        public RotationViewModel(Player player)
        {
            _player = player;
        }

        private void OnRotationChanged(float rotation)
        {
            Rotation.Value = rotation.ToString("F0") +" \u00b0";
        }

        public void Initialize()
        {
            _player.RotationChanged += OnRotationChanged;
        }

        public void Dispose()
        {
            _player.RotationChanged -= OnRotationChanged;
        }
    }
}