namespace Gwent.Pooling
{
    public interface IPoolable
    {
        bool IsActive { get; }

        void OnSpawnFromPool();
        void ReturnToPool();
    }
}
