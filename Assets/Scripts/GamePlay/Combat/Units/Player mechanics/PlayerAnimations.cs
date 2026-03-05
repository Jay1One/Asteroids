using UnityEngine;
using Zenject;

namespace GamePlay.Combat.Units.Player_mechanics
{
    [RequireComponent(typeof(Player))]
    public class PlayerAnimations : MonoBehaviour
    { 
        [SerializeField] private ParticleSystem _shieldParticleSystem;
        [SerializeField] private ParticleSystem _gasParticleSystem;
        private PlayerState _playerState;
        private PlayerMovement _playerMovement;


        [Inject]
        private void Construct(PlayerState playerState, PlayerMovement playerMovement)
        {
            _playerState = playerState;
            _playerMovement = playerMovement;
        }
        private void Awake()
        {
            _playerState.InvincibilityChanged += OnInvincibilityChanged;
            _playerMovement.GasChanged += OnGasChanged;
        }

        private void OnDestroy()
        {
            _playerState.InvincibilityChanged -= OnInvincibilityChanged;
            _playerMovement.GasChanged -= OnGasChanged;
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