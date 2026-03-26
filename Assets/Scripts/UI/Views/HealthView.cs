using MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image _healthPrefab;
        
        private Image[] _healthImages;
        private int _currentHealth;
        private int _maxHealth;

        [Setter("CurrentHealth")]
        public int CurrentHealth
        {
            set
            {
                _currentHealth = value;
                UpdateHealth();
            }
        }

        
        [Setter("MaxHealth")]
        public int MaxHealth
        {
            set
            {
                _maxHealth = value;
                UpdateHealth();
            }
        }

        private void UpdateHealth()
        {
            if (_healthImages == null || _healthImages.Length!=_maxHealth)
            {
                _healthImages = new Image[_maxHealth];

                for (int i = 0; i < _healthImages.Length; i++)
                {
                    _healthImages[i] = Instantiate(_healthPrefab, transform);
                }
            }
            
            for (int i = 0; i < _healthImages.Length; i++)
            {
                if (i<_currentHealth)
                {
                    _healthImages[i].color = Color.white;
                }
                else
                {
                    _healthImages[i].color = Color.clear;
                }
            }
        }
    }
}