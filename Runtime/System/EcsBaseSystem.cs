using ECS.Interfaces;
using System.Collections.Generic;

namespace ECS.System
{
    public abstract class EcsBaseSystem : IEcsSystem, IEcsUpdateSystem
    {
        public IEcsFilter Filter { get; protected set; }
        public IEcsContext Context { get; private set; }
        public IReadOnlyList<IEcsEntity> Entities => Filter.Entities;
        public virtual void Awake(IEcsContext context) => Context = context;
        public virtual void Destroy() { }
        public abstract void Start();
        public abstract void Update();
    }
}
