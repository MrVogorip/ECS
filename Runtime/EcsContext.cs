using ECS.Component;
using ECS.Entity;
using ECS.Filter;
using ECS.Interfaces;
using ECS.System;

namespace ECS
{
    public class EcsContext : IEcsContext
    {
        public EcsFilters Filters { get; }
        public EcsEntities Entities { get; }
        public EcsComponents Components { get; }
        public EcsSystems Systems { get; }

        public EcsContext()
        {
            Filters = new EcsFilters(this);
            Entities = new EcsEntities(this);
            Components = new EcsComponents(this);
            Systems = new EcsSystems(this);
        }

        public void Start()
        {
            Systems.Start();
        }

        public void Update()
        {
            Systems.Update();
            Filters.Update();
        }

        public void Destroy()
        {
            Systems.Destroy();
            Components.Destroy();
            Entities.Destroy();
            Filters.Destroy();
        }

        public EcsContext AddComponents<T>() where T : struct, IEcsComponent
        {
            Components.Add(new EcsCollectionComponents<T>(this));

            return this;
        }

        public EcsContext AddSystem(IEcsSystem system)
        {
            Systems.Add(system);

            return this;
        }

        public IEcsFilter MakeFilter()
        {
            return Filters.Make();
        }

        public IEcsEntity CreateEntity()
        {
            return Entities.Create();
        }
    }
}
