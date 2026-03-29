using System;
using UnityEngine;

namespace GamePlay.Physics
{
    public class PhysicsBody : MonoBehaviour
    { 
        private PhysicsBodyLogic _physicsBodyLogic;
        public event Action<float> SpeedChanged;
        
        public void Initialize(float maxSpeed, float deceleration)
        {
            _physicsBodyLogic = new PhysicsBodyLogic(maxSpeed, deceleration);
            _physicsBodyLogic.SpeedChanged += OnSpeedChanged;
        }

        public void AddForce(Vector2 force)
        {
            _physicsBodyLogic.AddForce(force);
        }

        public void SetVelocity(Vector2 velocity)
        {
            _physicsBodyLogic.SetVelocity(velocity);
        }
        
        private void Update()
        {
            _physicsBodyLogic?.UpdatePosition(transform);
        }

        private void OnDestroy()
        {
            _physicsBodyLogic.SpeedChanged -= OnSpeedChanged;
        }

        private void OnSpeedChanged(float speed)
        {
            SpeedChanged?.Invoke(speed);
        }
    }
}