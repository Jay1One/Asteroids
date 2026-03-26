using GamePlay.Combat.Systems;
using GamePlay.Combat.Weapons;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GamePlay.Input.Mobile_controls
{
    public class LaserButton :MonoBehaviour
    {
        [SerializeField] private Button _laserButton;
        
        private Laser _laser;
        private Gun _gun;
        private GameEndTracker _gameEndTracker;

        [Inject]
        public void Construct(Laser laser, GameEndTracker gameEndTracker)
        {
            _laser = laser;
            _gameEndTracker = gameEndTracker;
        }

        private void Awake()
        {
            _laserButton.onClick.AddListener(OnLaserButton);
        }
        
        private void OnLaserButton()
        {
            if (!_gameEndTracker.IsGameOver)
            {
                _laser.TryShoot();
            }
        }
    }
}