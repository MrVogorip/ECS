using ECS.Interfaces;

namespace ECS.Component
{
    public interface IEcsCollectionComponents
    {
        void AddFilter(int filterId);
        bool Exists(IEcsEntity entity);
        void Remove(IEcsEntity entity);
        void Clear();
    }
}
