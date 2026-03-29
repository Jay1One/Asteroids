using System;

namespace GamePlay.Combat.Units
{
    public class Health
    {
        private readonly int _maxHealth;
        private int _currentHealth;
        
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;
        
        public event Action Died;

        public Health(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            
            if (_currentHealth<=0)
            {
                _currentHealth = 0;
                Died?.Invoke();
            }
        }

        public void ResetHealth()
        {
            _currentHealth = _maxHealth;
        }
    }
}