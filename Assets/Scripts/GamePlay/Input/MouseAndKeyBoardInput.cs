using GamePlay.Combat.Systems;
using GamePlay.Combat.Units.Player_mechanics;
using GamePlay.Combat.Weapons;
using UnityEngine;
using Zenject;

namespace GamePlay.Input
{
    public class MouseAndKeyBoardInput : ITickable
    {
        private readonly KeyCode _upKey = KeyCode.W;
        private readonly KeyCode _leftKey = KeyCode.A;
        private readonly KeyCode _rightKey = KeyCode.D;
        
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerState _playerState;
        private readonly Laser _laser;
        private readonly Gun _gun;
        private readonly GameEndTracker _gameEndTracker;
        
        public MouseAndKeyBoardInput(PlayerState playerState, PlayerMovement playerMovement, Laser laser, Gun gun, GameEndTracker gameEndTracker)
        {
            _playerState = playerState;
            _playerMovement = playerMovement;
            _laser = laser;
            _gun = gun;
            _gameEndTracker = gameEndTracker;
        }

        public void Tick()
        {
            if (_gameEndTracker.IsGameOver)
            {
                return;
            }
            
            if (UnityEngine.Input.GetKey(_upKey))
            {
                _playerMovement.TryThrustForward();
            }
            if (UnityEngine.Input.GetKeyUp(_upKey))
            {
                _playerMovement.StopGas();
            }
            
            if (UnityEngine.Input.GetKey(_leftKey))
            {
                _playerState.TryRotate(1);
            }
            
            if (UnityEngine.Input.GetKey(_rightKey))
            {
                _playerState.TryRotate(-1);
            }

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _gun.TryShoot();
            }
            
            if (UnityEngine.Input.GetMouseButtonDown(1))
            {
                _laser.TryShoot();
            }
        }
    }
}
