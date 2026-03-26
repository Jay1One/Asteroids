using System;
using System.Threading;
using Core.Configs;
using Cysharp.Threading.Tasks;
using GamePlay.Combat.Bullets;
using GamePlay.Combat.Systems;
using GamePlay.Combat.Units;
using UnityEngine;
using Zenject;

namespace GamePlay.Combat.Weapons
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private LaserBeam _laserBeam;
        private Player _player;
        private CancellationToken _cancellationToken;
        private GameEndTracker _gameEndTracker;
        private float _shootTime;
        private float _cooldown;
        private int _maxCharges;
        private int _currentCharges;
        private float _timeForNextCharge;
        
        public event Action<int> ChargesChanged;
        public event Action<float> CooldownPercentageChanged;
        private bool _isShooting;
        
        [Inject]
        public void Construct(LaserConfig config, GameEndTracker endTracker, Player player)
        {
            _player = player;
            _gameEndTracker = endTracker;
            _cooldown = config.ChargeTime;
            _maxCharges = config.MaxCharges;
            _currentCharges = _maxCharges;
            _shootTime = config.ShootTime;
        }

        private void Start()
        {
            _cancellationToken = this.GetCancellationTokenOnDestroy();
            ChargesChanged?.Invoke(_currentCharges);
            _ = TrackCharges(_cancellationToken);
        }

        private async UniTask TrackCharges(CancellationToken token)
        {
            while (!_gameEndTracker.IsGameOver)
            {
                if (_currentCharges < _maxCharges)
                {
                    _timeForNextCharge -= Time.deltaTime;
                
                    if (_timeForNextCharge <= 0)
                    {
                        _currentCharges++;
                        ChargesChanged?.Invoke(_currentCharges);
                    }
                    
                    CooldownPercentageChanged?.Invoke(_timeForNextCharge / _cooldown);
                }
                
                await UniTask.Yield(token);
            }
        }

        public void TryShoot()
        {
            if (_player.IsInvincible || _currentCharges <= 0 || _isShooting || _gameEndTracker.IsGameOver)
            {
                return;
            }
            
            _ = Fire(_cancellationToken);
        }

        private async UniTask Fire(CancellationToken token)
        {
            _isShooting = true;
            _currentCharges--;
            ChargesChanged?.Invoke(_currentCharges);
            _laserBeam.gameObject.SetActive(true);
            _timeForNextCharge = _cooldown;
            
            await UniTask.Delay((int)(_shootTime * 1000), cancellationToken: token);
            
            _laserBeam.gameObject.SetActive(false);
            _isShooting = false;
        }
    }
}