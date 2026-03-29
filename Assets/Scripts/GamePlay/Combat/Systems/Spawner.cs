using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using GamePlay.Combat.Units.Enemies;
using GamePlay.Pooling;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace GamePlay.Combat.Systems
{
    public class Spawner :IInitializable, IDisposable
    { 
        private readonly ObjectPool<Asteroid> _asteroidPool;
        private readonly ObjectPool<Ufo> _ufoPool;
        private readonly GameField _gameField;
        private readonly GameEndTracker _gameEndTracker;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        
        private readonly float _timeForMaxDifficulty = 400f;
        
        private readonly float _maxAsteroidSpawnInterval=5f;
        private readonly float _maxUfoSpawnInterval=15f;
        
        private readonly float _minAsteroidSpawnInterval=1f;
        private readonly float _minUfoSpawnInterval=4f;
        
        private float _timeSinceLastAsteroid;
        private float _timeSinceLastUfo;
        
        
        private float _ufoSpawnInterval;
        private float _asteroidSpawnInterval;
        private bool _maxDifficultyReached;
        private float _startTime;
        
        
        public Spawner(ObjectPool<Asteroid> asteroidPool, 
            ObjectPool<Ufo> ufoPool, GameField gameField, GameEndTracker endTracker)
        {
            _ufoPool = ufoPool;
            _gameEndTracker = endTracker;
            _asteroidPool = asteroidPool;
            _gameField = gameField;
        }

        public void Initialize()
        {
            _asteroidSpawnInterval = _maxAsteroidSpawnInterval;
            _ufoSpawnInterval = _maxUfoSpawnInterval;
            _startTime = Time.time;
            _ = SpawnProcessAsync(_cts.Token);
        }

        public void Dispose()
        {
            _cts?.Cancel();
        }

        private async UniTask SpawnProcessAsync(CancellationToken token)
        {
            while (!_gameEndTracker.IsGameOver)
            {
                _timeSinceLastAsteroid += Time.deltaTime;
                _timeSinceLastUfo += Time.deltaTime;

                if (_timeSinceLastAsteroid >= _asteroidSpawnInterval)
                {
                    _timeSinceLastAsteroid -= _asteroidSpawnInterval;
                    SpawnAsteroid();
                }
                
                if (_timeSinceLastUfo >= _ufoSpawnInterval)
                {
                    _timeSinceLastUfo -= _ufoSpawnInterval;
                    SpawnUfo();
                }
                
                await UniTask.Yield(token);
                
                UpdateDifficulty();
            }
        }

        private void UpdateDifficulty()
        {
            if (_maxDifficultyReached)
            {
                return;
            }

            if (Time.time - _startTime > _timeForMaxDifficulty)
            {
                _asteroidSpawnInterval = _minAsteroidSpawnInterval;
                _ufoSpawnInterval = _minUfoSpawnInterval;
                _maxDifficultyReached = true;
            }
            else
            {
                _asteroidSpawnInterval = Mathf.Lerp(_maxAsteroidSpawnInterval, _minAsteroidSpawnInterval,
                    (Time.time -_startTime)/_timeForMaxDifficulty);
                
                _ufoSpawnInterval = Mathf.Lerp(_maxUfoSpawnInterval, _minUfoSpawnInterval,
                    (Time.time - _startTime) / _timeForMaxDifficulty);
            }
        }

        private void SpawnAsteroid()
        {
            Vector2 position = GetRandomPointOnGameFieldEdge();
            _asteroidPool.GetObject(position);
        }

        private void SpawnUfo()
        {
            Vector2 position = GetRandomPointOnGameFieldEdge();
            _ufoPool.GetObject(position);
        }
        
        private Vector2 GetRandomPointOnGameFieldEdge()
        {
            int randomEdge = Random.Range(0, 4);
            Vector2 result = Vector2.zero;
            
            switch (randomEdge)
            {
                case 0:
                    result = new Vector2(Random.Range(-1f,1f) * _gameField.Width/2, -_gameField.Height/2);
                    break;
                
                case 1:
                    result = new Vector2(Random.Range(-1f,1f) * _gameField.Width/2, _gameField.Height/2);
                    break;
                
                case 2:
                    result = new Vector2(-_gameField.Width/2, Random.Range(-1f,1f) * _gameField.Height/2);
                    break;
                
                case 3:
                    result = new Vector2(_gameField.Width/2, Random.Range(-1f,1f) * _gameField.Height/2);
                    break;
            }

            return result;
        }
    }
}
