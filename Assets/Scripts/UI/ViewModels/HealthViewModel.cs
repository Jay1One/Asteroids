using System;
using GamePlay.Combat.Units;
using MVVM;
using UniRx;
using Zenject;

namespace UI.ViewModels
{
    public class HealthViewModel : IInitializable, IDisposable
    {
        private Player _player;
        
        [Data("MaxHealth")]
        public ReactiveProperty<int> MaxHealth = new ReactiveProperty<int>();
        
        [Data("CurrentHealth")]
        public ReactiveProperty<int> CurrentHealth = new ReactiveProperty<int>();

        public HealthViewModel(Player player)
        {
            _player = player;
        }

        private void OnHealthChanged(int currentHealth, int maxHealth)
        {
            CurrentHealth.Value = currentHealth;
            MaxHealth.Value = maxHealth;
        }

        public void Initialize()
        {
            _player.HealthChanged += OnHealthChanged;
        }

        public void Dispose()
        {
            _player.HealthChanged += OnHealthChanged;
        }
    }
}