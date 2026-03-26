using System;

namespace Core.Configs
{
    [Serializable]
    public struct PlayerConfig
    { 
        public int MaxHealth;
        public float RotationSpeed;
        public float MaxMoveSpeed;
        public float MoveAcceleration;
        public float MoveDeceleration;
        public float InvincibilityDuration;
        public float BounceSpeed;
    }
}