using ECS.Extensions;
using ECS.Interfaces;

namespace ECS.Component
{
    public static class EcsComponent<T> where T : struct, IEcsComponent
    {
        public static int Id { get; }

        static EcsComponent()
        {
            Id = typeof(T).Name.GetComponentId();
        }
    }
}
