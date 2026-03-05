using System;
using Core.Configs;
using GamePlay.Combat.Units.Enemies;
using GamePlay.Physics;
using UnityEngine;
using Zenject;

namespace GamePlay.Combat.Units.Player_mechanics
{
    public class PlayerMovement :ITickable, IInitializable, IDisposable
    {
        private readonly Transform _transform;
        private readonly PhysicsBody _physicsBody;
        private readonly float _acceleration;
        private readonly float _bounceSpeed;
        private bool _isGasPressed;

        public event Action<float> SpeedChanged; 
        public event Action<Vector2> CoordinatesChanged; 
        public event Action<bool> GasChanged;


        private readonly PlayerState _playerState;

        public PlayerMovement(PlayerState playerState, PlayerConfig playerConfig, PhysicsBody physicsBody, Transform transform)
        {
            _transform = transform;
            _physicsBody = physicsBody;
            _playerState = playerState;
            _acceleration = playerConfig.MoveAcceleration;
            _bounceSpeed = playerConfig.BounceSpeed;
        }
        
        public void ProcessCollision(Collision2D other)
        {
            if (other.gameObject.GetComponent<Enemy>())
            {
                Vector2 direction = other.contacts[0].normal;
                _physicsBody.SetVelocity(direction * _bounceSpeed);
            }
        }
        
        public void TryThrustForward()
        {
            if (!_playerState.IsInvincible)
            {
                _physicsBody.AddForce(_playerState.LookDirection * (_acceleration * Time.deltaTime));
                
                if (!_isGasPressed)
                {
                    _isGasPressed = true;
                    GasChanged?.Invoke(_isGasPressed);
                }
            }
        }
        
        public void StopGas()
        {
            _isGasPressed = false;
            GasChanged?.Invoke(_isGasPressed);
        }

        public void Tick()
        {
            CoordinatesChanged?.Invoke(_transform.position);
        }

        public void Initialize()
        {
            _playerState.InvincibilityChanged += OnPlayerInvincibilityChanged;
            _physicsBody.SpeedChanged += OnSpeedChanged;
        }
        
        public void Dispose()
        {
            _playerState.InvincibilityChanged += OnPlayerInvincibilityChanged;
            
            if (_physicsBody != null)
            {
                _physicsBody.SpeedChanged -= OnSpeedChanged;
            }
        }
        
        private void OnSpeedChanged(float speed)
        {
            SpeedChanged?.Invoke(speed);
        }
        
        private void OnPlayerInvincibilityChanged(bool isInvincible)
        {
            if (isInvincible)
            {
                _isGasPressed = false;
            }
        }
    }
}