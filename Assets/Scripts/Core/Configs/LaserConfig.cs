using System;

namespace Core.Configs
{
    [Serializable]
    public struct LaserConfig
    {
        public int MaxCharges;
        public float ChargeTime;
        public float ShootTime;
    }
}