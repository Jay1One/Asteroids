namespace GamePlay.Pooling
{
    public interface IPoolableObject
    {
        public void Deactivate();
        public void Activate();
    }
}