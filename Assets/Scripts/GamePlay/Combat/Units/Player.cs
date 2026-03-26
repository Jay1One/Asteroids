using System;
using System.Threading;
using Core.Configs;
using Core.Signals;
using Cysharp.Threading.Tasks;
using GamePlay.Physics;
using UnityEngine;
using Zenject;

namespace GamePlay.Combat.Units
{
    public class Player : Unit
    {
        private SignalBus _signalBus; 
        private readonly Vector2 _startLookDirection = Vector2.up;
        private float _acceleration;
        private float _rotationSpeed;
        private float _invincibilityDuration;
        private float _bounceSpeed;
    
        private float _rotationDegrees;
        private Vector2 _lookDirection = Vector2.up;
        private CancellationToken _cancellationToken;
        private bool _isGasPressed;

        public bool IsInvincible { get; private set; }

        public event Action<float> SpeedChanged; 
        public event Action<Vector2> CoordinatesChanged; 
        public event Action<float> RotationChanged;
        public event Action<int, int> HealthChanged;
        public event Action<bool> InvincibilityChanged;
        public event Action<bool> GasChanged;

        [Inject]
        private void Construct(PlayerConfig playerConfig, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _rotationSpeed = playerConfig.RotationSpeed;
            _acceleration = playerConfig.MoveAcceleration;
            _invincibilityDuration = playerConfig.InvincibilityDuration;
            _bounceSpeed = playerConfig.BounceSpeed;
                
            PhysicsBody = GetComponent<PhysicsBody>();
            PhysicsBody.Initialize(playerConfig.MaxMoveSpeed, playerConfig.MoveDeceleration);
            
            Health = new Health(playerConfig.MaxHealth);
        }
    
        protected void Awake()
        {
            PhysicsBody.SpeedChanged += OnSpeedChanged;
        }

        private void Start()
        {
            _cancellationToken = this.GetCancellationTokenOnDestroy();
            
            RotationChanged?.Invoke(_rotationDegrees);
            HealthChanged?.Invoke(Health.CurrentHealth, Health.MaxHealth);
        }

        private void OnDestroy()
        {
            if (PhysicsBody != null)
            {
                PhysicsBody.SpeedChanged -= OnSpeedChanged;
            }
        }

        private void Update()
        {
            CoordinatesChanged?.Invoke(transform.position);
        }

        public void TryThrustForward()
        {
            if (!IsInvincible)
            {
                PhysicsBody.AddForce(_lookDirection * (_acceleration * Time.deltaTime));
                
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

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            HealthChanged?.Invoke(Health.CurrentHealth, Health.MaxHealth);
            if (Health.CurrentHealth > 0)
            {
                _ = GainInvincibilityAsync(_cancellationToken);
            }
        }
        
        public void TryRotate(Vector2 direction)
        {
            if (IsInvincible)
            {
                return;
            }

            if (direction!=_lookDirection)
            {
                float targetAdditionalRotation = Vector2.SignedAngle(direction, _lookDirection);
                float maxRotationThisFrame =_rotationSpeed * Time.deltaTime;
                
                if (Mathf.Abs(targetAdditionalRotation) > maxRotationThisFrame)
                {
                    Rotate(targetAdditionalRotation>0? -maxRotationThisFrame : maxRotationThisFrame);
                }
                else
                {
                    Rotate(targetAdditionalRotation);
                }
            }
        }
        
        public void TryRotate(int direction)
        {
            if (IsInvincible)
            {
                return;
            }
            
            Rotate(direction * _rotationSpeed * Time.deltaTime);
        }

        protected override void Die()
        {
            gameObject.layer = LayerMask.NameToLayer("Invincible objects");
            _signalBus.Fire<PlayerDiedSignal>();
        }
        
        private void Rotate(float degrees)
        {
            _rotationDegrees += degrees;
            _rotationDegrees %= 360;
            transform.rotation = Quaternion.Euler(new Vector3(0,0,_rotationDegrees));
            
            _lookDirection = Quaternion.AngleAxis(_rotationDegrees, Vector3.forward) 
                             * _startLookDirection;
            
            RotationChanged?.Invoke(_rotationDegrees);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Enemy>())
            {
                Vector2 direction = other.contacts[0].normal;
                PhysicsBody.SetVelocity(direction * _bounceSpeed);
            }
        }

        private async UniTask GainInvincibilityAsync(CancellationToken cancellationToken)
        {
            IsInvincible = true;
            InvincibilityChanged?.Invoke(IsInvincible);
            _isGasPressed = false;
            GasChanged?.Invoke(_isGasPressed);
            
            gameObject.layer = LayerMask.NameToLayer("Invincible objects");
            int invincibilityMilliseconds = (int)(_invincibilityDuration * 1000);
            
            await UniTask.Delay(invincibilityMilliseconds, cancellationToken: cancellationToken);
            
            gameObject.layer = LayerMask.NameToLayer("Player");
            IsInvincible = false;
            InvincibilityChanged?.Invoke(IsInvincible);
        }
        
        private void OnSpeedChanged(float speed)
        {
            SpeedChanged?.Invoke(speed);
        }
    }
}
