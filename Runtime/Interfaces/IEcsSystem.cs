namespace ECS.Interfaces
{
    public interface IEcsSystem
    {
        IEcsFilter Filter { get; }
        IEcsContext Context { get; }
        void Awake(IEcsContext context);
        void Start();
        void Destroy();
    }

    public interface IEcsUpdateSystem : IEcsSystem
    {
        void Update();
    }
}
