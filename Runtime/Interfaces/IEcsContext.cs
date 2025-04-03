namespace ECS.Interfaces
{
    public interface IEcsContext
    {
        IEcsFilter MakeFilter();
        IEcsEntity CreateEntity();
    }
}
