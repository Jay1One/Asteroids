using Core.Configs;
using GamePlay.Combat.Units.Enemies;
using GamePlay.Pooling;
using UnityEngine;

namespace GamePlay.Combat.Systems
{
    public class ShardSpawner
    {
        private readonly ObjectPool<AsteroidShard> _shardPool;
        private readonly int _shardsSpawned;

        public ShardSpawner(ObjectPool<AsteroidShard> shardPool, AsteroidConfig asteroidConfig) 
        {
            _shardPool = shardPool;
            _shardsSpawned = asteroidConfig.ShardsSpawned;
        }

        public void SpawnShards(Vector2 position)
        {
            for (int i = 0; i < _shardsSpawned; i++)
            {
                _shardPool.GetObject(position);
            }
        }
    }
}