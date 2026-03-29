using System;
using System.Threading;
using Core.Configs;
using Core.Signals;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace GamePlay.Combat.Units.Player_mechanics
{
    public class PlayerState : IInitializable, IDisposable
    {
        private float _rotationDegrees;
        private readonly Transform _transform;
        private readonly float _invincibilityDuration;
        private readonly float _rotationSpeed;
        private readonly SignalBus _signalBus;
        private readonly Vector2 _startLookDirection = Vector2.up;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        
        public event Action<bool> InvincibilityChanged;
        public event Action<float> RotationChanged;
        
        
        public bool IsInvincible { get; private set; }
        public Vector2 LookDirection { get; private set; }
        public void Initialize()
        {
            _signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDied);
            LookDirection = Vector2.up;
            RotationChanged?.Invoke(_rotationDegrees);
        }

        public PlayerState(Transform transform, PlayerConfig playerConfig, SignalBus signalBus)
        {
            _transform = transform;
            _rotationSpeed = playerConfig.RotationSpeed;
            _invincibilityDuration = playerConfig.InvincibilityDuration;
            _signalBus = signalBus;
        }

        private void OnPlayerDied()
        {
            _transform.gameObject.layer = LayerMask.NameToLayer("Invincible objects");
        }

        public void TryRotate(int direction)
        {
            if (IsInvincible)
            {
                return;
            }
            
            Rotate(direction * _rotationSpeed * Time.deltaTime);
        }
        
        public void TryRotate(Vector2 direction)
        {
            if (IsInvincible)
            {
                return;
            }

            if (direction!=LookDirection)
            {
                float targetAdditionalRotation = Vector2.SignedAngle(direction, LookDirection);
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
        
        private void Rotate(float degrees)
        {
            _rotationDegrees += degrees;
            _rotationDegrees %= 360;
            _transform.rotation = Quaternion.Euler(new Vector3(0,0,_rotationDegrees));
            
            LookDirection = Quaternion.AngleAxis(_rotationDegrees, Vector3.forward) 
                             * _startLookDirection;
            
            RotationChanged?.Invoke(_rotationDegrees);
        }
        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerDiedSignal>(OnPlayerDied);
            _cts?.Cancel();
            _cts?.Dispose();
        }
        
        private async UniTask GainInvincibilityAsync(CancellationToken cancellationToken)
        {
            IsInvincible = true;
            InvincibilityChanged?.Invoke(IsInvincible);
            
            _transform.gameObject.layer = LayerMask.NameToLayer("Invincible objects");
            int invincibilityMilliseconds = (int)(_invincibilityDuration * 1000);
            
            await UniTask.Delay(invincibilityMilliseconds, cancellationToken: cancellationToken);
            
            _transform.gameObject.layer = LayerMask.NameToLayer("Player");
            IsInvincible = false;
            InvincibilityChanged?.Invoke(IsInvincible);
        }

        public void GainInvincibility()
        {
            _ = GainInvincibilityAsync(_cts.Token);
        }
    }
}