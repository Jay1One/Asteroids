using System;
using System.Threading;
using Core.Configs;
using Cysharp.Threading.Tasks;
using GamePlay.Combat.Bullets;
using GamePlay.Combat.Systems;
using GamePlay.Combat.Units.Player_mechanics;
using UnityEngine;
using Zenject;

namespace GamePlay.Combat.Weapons
{
    public class Laser : IInitializable, IDisposable
    {
        private readonly LaserBeam _laserBeam;
        private readonly PlayerState _playerState;
        private CancellationTokenSource _cts;
        private readonly GameEndTracker _gameEndTracker;
        private readonly float _shootTime;
        private readonly float _cooldown;
        private readonly int _maxCharges;
        
        private int _currentCharges;
        private float _timeForNextCharge;
        private bool _isShooting;
        
        public event Action<int> ChargesChanged;
        public event Action<float> CooldownPercentageChanged;
        
        [Inject]
        public Laser(LaserConfig config, GameEndTracker endTracker, PlayerState playerState, LaserBeam laserBeam)
        {
            _laserBeam = laserBeam;
            _playerState = playerState;
            _gameEndTracker = endTracker;
            _cooldown = config.ChargeTime;
            _maxCharges = config.MaxCharges;
            _currentCharges = _maxCharges;
            _shootTime = config.ShootTime;
        }
        
        public void Initialize()
        {
            _laserBeam.gameObject.SetActive(false);
            _cts = new CancellationTokenSource();
            ChargesChanged?.Invoke(_currentCharges);
            _ = TrackCharges(_cts.Token);
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        public void TryShoot()
        {
            if (_playerState.IsInvincible || _currentCharges <= 0 || _isShooting || _gameEndTracker.IsGameOver)
            {
                return;
            }
            
            _ = Fire(_cts.Token);
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
                        _timeForNextCharge = _cooldown;
                        _currentCharges++;
                        ChargesChanged?.Invoke(_currentCharges);
                    }
                    
                    CooldownPercentageChanged?.Invoke(_timeForNextCharge / _cooldown);
                }
                
                await UniTask.Yield(token);
            }
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