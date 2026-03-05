using GamePlay.Combat.Systems;
using GamePlay.Combat.Weapons;
using UnityEngine;
using Zenject;

namespace GamePlay.Input.Mobile_controls
{
    public class GunButton : MonoBehaviour
    {
        private GameEndTracker _gameEndTracker;
        private Gun _gun;

        [Inject]
        public void Construct(GameEndTracker gameEndTracker, Gun gun)
        {
            _gameEndTracker = gameEndTracker;
            _gun = gun;
        }
        
        public void OnButtonHold()
        {
            if (!_gameEndTracker.IsGameOver)
            {
                _gun.TryShoot();
            }
        } 
    }
}