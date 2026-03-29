using GamePlay.Combat.Bullets;
using GamePlay.Combat.Units.Enemies;
using GamePlay.Combat.Units.Player_mechanics;
using UnityEngine;
using Zenject;

namespace GamePlay.Combat.Systems
{
    public class GameFieldMonoBehaviour :MonoBehaviour
    {
        private GameField _gameField;

        [Inject]
        public void Construct(GameField gameField)
        {
            _gameField = gameField;
        }

        private void Start()
        {
            transform.localScale = new Vector3(_gameField.Width,_gameField.Height,1);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.activeInHierarchy)
            {
                return;
            }
            
            if (other.gameObject.TryGetComponent<Player>(out var player))
            {
                _gameField.TeleportPlayer(player);
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
    }
}