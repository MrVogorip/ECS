using ECS.Common;
using ECS.Interfaces;

namespace ECS.Entity
{
    public class EcsEntities
    {
        public int Count => _entities.Count;

        private readonly EcsContext _context;
        private readonly EcsArrayPool<EcsEntity> _entities;

        public EcsEntities(EcsContext context)
        {
            _context = context;
            _entities = new EcsArrayPool<EcsEntity>(EcsConfig.EntityCapacity);
        }

        public IEcsEntity Create()
        {
            var index = _entities.Take();
            var entity = new EcsEntity(index, _context);
            _entities.Drop(entity, index);

            return entity;
        }

        public EcsEntity this[int index]
        {
            get => _entities.Find(index);
        }

        public void Delete(EcsEntity entity)
        {
            _entities.Free(entity.Id);
        }

        public void Destroy()
        {
            _entities.Clear();
        }
    }
}
