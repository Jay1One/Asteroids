using System;
using UnityEngine;

namespace GamePlay.Physics
{
    public class PhysicsBody : MonoBehaviour
    { 
        private float _maxSpeed; 
        private float _deceleration;
        private Vector2 _velocity;
        private float _speed;
        
        public event Action<float> SpeedChanged;
        public void Initialize(float maxSpeed, float deceleration)
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
        
        private void Update()
        {
            transform.position = (Vector2)transform.position + _velocity * Time.deltaTime;
            SpeedChanged?.Invoke(_velocity.magnitude);
            _velocity -= _velocity *(_deceleration * Time.deltaTime);
        }
    }
}