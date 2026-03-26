using GamePlay.Combat.Units;
using UnityEngine;

namespace GamePlay.Combat.Bullets
{
    public class LaserBeam : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(int.MaxValue);
            }
        }
    }
}