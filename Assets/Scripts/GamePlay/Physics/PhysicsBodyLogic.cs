
using System;
using UnityEngine;

namespace GamePlay.Physics
{
    public class PhysicsBodyLogic
    {
        private readonly float _maxSpeed; 
        private readonly float _deceleration;
        private Vector2 _velocity;
        private float _speed;
        
        public event Action<float> SpeedChanged;
        public PhysicsBodyLogic(float maxSpeed, float deceleration)
        {
            _maxSpeed = maxSpeed;
            _deceleration = deceleration;
        }
        public void AddForce(Vector2 force)
        {
            _velocity += force;
            _velocity = Vector2.ClampMagnitude(_velocity, _maxSpeed);
        }

        public void SetVelocity(Vector2 velocity)
        {
            _velocity = Vector2.ClampMagnitude(velocity, _maxSpeed);
        }

        public void UpdatePosition(Transform transform)
        {
            transform.position = (Vector2)transform.position + _velocity * Time.deltaTime;
            SpeedChanged?.Invoke(_velocity.magnitude);
            _velocity -= _velocity *(_deceleration * Time.deltaTime);
        }
    }
}