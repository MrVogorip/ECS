using ECS.Common;
using ECS.Interfaces;
using System.Collections.Generic;

namespace ECS.Component
{
    public class EcsComponents : Dictionary<int, IEcsCollectionComponents>
    {
        public EcsComponents(EcsContext context) : base(EcsConfig.ComponentCapacity) { }

        public void Add<T>(EcsCollectionComponents<T> components) where T : struct, IEcsComponent
        {
            this[EcsComponent<T>.Id] = components;
        }

        public EcsCollectionComponents<T> Get<T>() where T : struct, IEcsComponent
        {
            return (EcsCollectionComponents<T>)this[EcsComponent<T>.Id];
        }

        public void Destroy()
        {
            foreach (var components in Values)
            {
                components.Clear();
            }
        }
    }
}
