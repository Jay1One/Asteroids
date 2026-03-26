using UnityEngine;

namespace GamePlay.Combat.Units
{
    public class PlayerAnimations : MonoBehaviour
    { 
        [SerializeField] private ParticleSystem _shieldParticleSystem;
        [SerializeField] private ParticleSystem _gasParticleSystem;
        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
            _player.InvincibilityChanged += OnInvincibilityChanged;
            _player.GasChanged += OnGasChanged;
        }

        private void OnDestroy()
        {
            _player.InvincibilityChanged -= OnInvincibilityChanged;
            _player.GasChanged -= OnGasChanged;
        }

        private void OnInvincibilityChanged(bool isInvincible)
        {
            if (isInvincible)
            {
                _shieldParticleSystem.Play();
            }
            else
            {
                _shieldParticleSystem.Stop();
                _shieldParticleSystem.Clear();
            }
        }

        private void OnGasChanged(bool isGasPressed)
        {
            if (isGasPressed)
            {
                _gasParticleSystem.Play();
            }
            else
            {
                _gasParticleSystem.Stop();
                _gasParticleSystem.Clear();
            }
        }
    }
}