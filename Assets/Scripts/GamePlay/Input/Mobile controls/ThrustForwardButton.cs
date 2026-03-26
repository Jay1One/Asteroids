using GamePlay.Combat.Systems;
using GamePlay.Combat.Units;
using UnityEngine;
using Zenject;

namespace GamePlay.Input.Mobile_controls
{
    public class ThrustForwardButton : MonoBehaviour
    {
        private GameEndTracker _gameEndTracker;
        private Player _player;

        [Inject]
        public void Construct(GameEndTracker gameEndTracker, Player player)
        {
            _gameEndTracker = gameEndTracker;
            _player = player;
        }
        
        public void OnButtonHold()
        {
            if (!_gameEndTracker.IsGameOver)
            {
                _player.TryThrustForward();
            }
        }

        public void OnButtonRelease()
        {
            _player.StopGas();
        }
    }
}