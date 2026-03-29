using Core.Configs;
using GamePlay.Physics;
using UnityEngine;
using Zenject;

namespace GamePlay.Combat.Units.Player_mechanics
{
    [RequireComponent(typeof(PhysicsBody))]
    public class Player : MonoBehaviour
    {
        private PlayerState _playerState;
        private PlayerMovement _playerMovement;
        private PlayerHealthService _playerHealthService;

        [Inject]
        private void Construct(PlayerConfig playerConfig, PlayerState playerState, 
            PlayerMovement playerMovement, PlayerHealthService playerHealthService)
        {
            _playerHealthService = playerHealthService;
            _playerState = playerState;
            _playerMovement = playerMovement;
                
            PhysicsBody physicsBody = GetComponent<PhysicsBody>();
            physicsBody.Initialize(playerConfig.MaxMoveSpeed, playerConfig.MoveDeceleration);
        }

        private void Start()
        {
            _playerHealthService.Initialize();
        }

        public void TakeDamage(int damage)
        {
            if (_playerState.IsInvincible)
            {
                return;
            }
            
            _playerHealthService.TakeDamage(damage);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _playerMovement.ProcessCollision(other);
        }
    }
}
