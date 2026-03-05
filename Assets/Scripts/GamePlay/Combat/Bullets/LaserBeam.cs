using GamePlay.Combat.Systems;
using GamePlay.Combat.Units.Enemies;
using UnityEngine;
using Zenject;

namespace GamePlay.Combat.Bullets
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class LaserBeam : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        [Inject]
        public void Construct(GameField gameField)
        {
            float maxLength = Mathf.Sqrt(gameField.Width* gameField.Width + gameField.Height * gameField.Height);
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            collider.size = new Vector2(collider.size.x, maxLength);
            collider.offset = new Vector2(collider.offset.x, maxLength/2);
            _spriteRenderer.size = new Vector2(_spriteRenderer.size.x, maxLength);
            
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.TakeDamage(int.MaxValue);
            }
        }
    }
}