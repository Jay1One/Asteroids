using System;
using System.Collections.Generic;
using Core.Signals;
using GamePlay.Combat.Units.Enemies;
using Zenject;

namespace GamePlay.Combat.Systems
{
    public class ScoreCalculator :IInitializable, IDisposable
    {
        private int _score;
        private readonly SignalBus _signalBus;
        public int Score => _score;

        private readonly Dictionary<Type, int> _rewards = new Dictionary<Type, int>
        {
            { typeof(Asteroid), 5 },
            { typeof(AsteroidShard), 1 },
            { typeof(Ufo), 10 },
        };
        
        public event Action<int> ScoreChanged;

        public ScoreCalculator(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        private void OnEnemyDied(EnemyDiedSignal signal)
        {
            if (_rewards.TryGetValue(signal.Enemy.GetType(), out var value))
            {
                _score += _rewards[signal.Enemy.GetType()];
                ScoreChanged?.Invoke(_score);
            }
            else
            {
                throw new KeyNotFoundException("No such enemy type in rewards");
            }
        }

        public void Initialize()
        {
            _signalBus.Subscribe<EnemyDiedSignal>(OnEnemyDied);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EnemyDiedSignal>(OnEnemyDied);
        }
    }
}