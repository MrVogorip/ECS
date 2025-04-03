using ECS.Common;
using ECS.Component;
using ECS.Interfaces;
using System.Collections.Generic;

namespace ECS.Entity
{
    public class EcsEntity : IEcsEntity
    {
        public int Id { get; }

        private readonly EcsContext _context;
        private readonly HashSet<int> _components;

        public EcsEntity(int id, EcsContext context)
        {
            Id = id;
            _context = context;
            _components = new HashSet<int>(EcsConfig.ComponentCapacity);
        }

        public IEcsEntity AddComponent<T>(T component) where T : struct, IEcsComponent
        {
            _context.Components.Get<T>().Insert(this, component);
            _components.Add(EcsComponent<T>.Id);

            return this;
        }

        public ref T GetComponent<T>() where T : struct, IEcsComponent
        {
            return ref _context.Components.Get<T>().Obtain(this);
        }

        public bool HasComponent(int componentId)
        {
            return _components.Contains(componentId);
        }

        public bool HasComponent<T>() where T : struct, IEcsComponent
        {
            return _components.Contains(EcsComponent<T>.Id);
        }

        public void DelComponent<T>() where T : struct, IEcsComponent
        {
            var componentId = EcsComponent<T>.Id;
            _context.Components[componentId].Remove(this);
            _components.Remove(componentId);
        }

        public void Destroy()
        {
            foreach (int componentId in _components)
            {
                _context.Components[componentId].Remove(this);
            }

            _components.Clear();
            _context.Entities.Delete(this);
        }
    }
}
