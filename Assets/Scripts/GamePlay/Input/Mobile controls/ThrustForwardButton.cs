using GamePlay.Combat.Systems;
using GamePlay.Combat.Units.Player_mechanics;
using UnityEngine;
using Zenject;

namespace GamePlay.Input.Mobile_controls
{
    public class ThrustForwardButton : MonoBehaviour
    {
        private GameEndTracker _gameEndTracker;
        private PlayerMovement _playerMovement;

        [Inject]
        public void Construct(GameEndTracker gameEndTracker, PlayerMovement playerMovement)
        {
            _gameEndTracker = gameEndTracker;
            _playerMovement = playerMovement;
        }
        
        public void OnButtonHold()
        {
            if (!_gameEndTracker.IsGameOver)
            {
                _playerMovement.TryThrustForward();
            }
        }

        public void OnButtonRelease()
        {
            _playerMovement.StopGas();
        }
    }
}