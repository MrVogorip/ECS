namespace ECS.Interfaces
{
    public interface IEcsEntity
    {
        int Id { get; }
        IEcsEntity AddComponent<T>(T component) where T : struct, IEcsComponent;
        ref T GetComponent<T>() where T : struct, IEcsComponent;
        bool HasComponent<T>() where T : struct, IEcsComponent;
        void DelComponent<T>() where T : struct, IEcsComponent;
        void Destroy();
    }
}
