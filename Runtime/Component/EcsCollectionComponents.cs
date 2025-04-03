using ECS.Common;
using ECS.Interfaces;
using System.Collections.Generic;

namespace ECS.Component
{
    public class EcsCollectionComponents<T> : IEcsCollectionComponents where T : struct, IEcsComponent
    {
        private readonly EcsContext _context;
        private readonly HashSet<int> _filters;
        private readonly EcsArrayPool<T> _components;
        private readonly Dictionary<int, int> _indexes;

        public EcsCollectionComponents(EcsContext context)
        {
            _context = context;
            _filters = new HashSet<int>(EcsConfig.ComponentCapacity);
            _components = new EcsArrayPool<T>(EcsConfig.ComponentCapacity);
            _indexes = new Dictionary<int, int>(EcsConfig.ComponentCapacity);
        }

        public void AddFilter(int filterId)
        {
            _filters.Add(filterId);
        }

        public void Insert(IEcsEntity entity, T component)
        {
            var index = _components.Take();
            _indexes[entity.Id] = index;
            _components.Drop(component, index);
            _context.Filters.Enqueue(_filters);
        }

        public bool Exists(IEcsEntity entity)
        {
            return _indexes.ContainsKey(entity.Id);
        }

        public ref T Obtain(IEcsEntity entity)
        {
            return ref _components.Find(_indexes[entity.Id]);
        }

        public void Remove(IEcsEntity entity)
        {
            if (!_indexes.TryGetValue(entity.Id, out int index))
            {
                return;
            }

            _components.Free(index);
            _indexes.Remove(entity.Id);
            _context.Filters.Enqueue(_filters);
        }

        public void Clear()
        {
            _components.Clear();
            _indexes.Clear();
        }
    }
}
