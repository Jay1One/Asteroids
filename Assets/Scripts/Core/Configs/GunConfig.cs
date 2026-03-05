using System;

namespace Core.Configs
{
    [Serializable]
    public struct GunConfig
    {
        public float BulletsPerSecond;
        public float BulletSpeed;
        public int BulletDamage;
    }
}