using System;
using GamePlay.Combat.Units.Player_mechanics;
using MVVM;
using UniRx;
using Zenject;

namespace UI.ViewModels
{
    public class HealthViewModel : IInitializable, IDisposable
    {
        private readonly PlayerHealthService _playerHealthService;
        
        [Data("MaxHealth")]
        public ReactiveProperty<int> MaxHealth = new ReactiveProperty<int>();
        
        [Data("CurrentHealth")]
        public ReactiveProperty<int> CurrentHealth = new ReactiveProperty<int>();

        public HealthViewModel(PlayerHealthService playerHealthService)
        {
            _playerHealthService = playerHealthService;
        }

        private void OnHealthChanged(int currentHealth, int maxHealth)
        {
            CurrentHealth.Value = currentHealth;
            MaxHealth.Value = maxHealth;
        }

        public void Initialize()
        {
            _playerHealthService.HealthChanged += OnHealthChanged;
        }

        public void Dispose()
        {
            _playerHealthService.HealthChanged += OnHealthChanged;
        }
    }
}