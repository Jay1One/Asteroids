using Core.Configs;
using GamePlay.Combat.Bullets;
using GamePlay.Combat.Units;
using UnityEngine;
using Zenject;

namespace GamePlay.Combat.Systems
{
    public class GameField : MonoBehaviour, IInitializable
    {
        public float Width { get; private set; }
        public float Height { get; private set; }

        [Inject]
        public void Construct(GameFieldConfig config)
        {
            Width = config.Width;
            Height = config.Height;
        }
        public void Initialize()
        {
            transform.localScale = new Vector3(Width,Height,1);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent<Player>(out var player))
            {
                TeleportPlayer(player);
            }

            if (other.gameObject.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.Deactivate();
            }
            
            if (other.gameObject.TryGetComponent<PlayerBullet>(out var bullet))
            {
                bullet.Deactivate();
            }
        }

        private void TeleportPlayer(Player player)
        {
            float newX = player.transform.position.x;
            float newY = player.transform.position.y;
            
            if (player.transform.position.x > Width/2)
            {
                newX = -Width + player.transform.position.x;
            }
            else if (player.transform.position.x < -Width/2)
            {
                newX = Width + player.transform.position.x;
            }
            
            if (player.transform.position.y > Height/2)
            {
                newY = -Height + player.transform.position.y;
            }
            else if (player.transform.position.y < -Height/2)
            {
                newY = Height + player.transform.position.y;
            }
            
            player.transform.position = new Vector2(newX, newY);
        }
    }
}