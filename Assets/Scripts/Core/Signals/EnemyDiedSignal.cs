using Core.Interfaces;

namespace Core.Signals
{ 
    public class EnemyDiedSignal
    {
        public IEnemy Enemy { get; private set; }

        public EnemyDiedSignal(IEnemy enemy)
        {
            Enemy = enemy;
        }
    }
}