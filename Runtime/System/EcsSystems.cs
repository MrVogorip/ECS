using ECS.Common;
using ECS.Interfaces;
using System.Collections.Generic;

namespace ECS.System
{
    public class EcsSystems
    {
        private readonly EcsContext _context;
        private readonly List<IEcsSystem> _systems;
        private readonly List<IEcsUpdateSystem> _updates;

        public EcsSystems(EcsContext context)
        {
            _context = context;
            _systems = new List<IEcsSystem>(EcsConfig.SystemCapacity);
            _updates = new List<IEcsUpdateSystem>(EcsConfig.SystemCapacity);
        }

        public void Add(IEcsSystem system)
        {
            system.Awake(_context);
            _systems.Add(system);

            if (system is IEcsUpdateSystem updateSystem)
            {
                _updates.Add(updateSystem);
            }
        }

        public void Start()
        {
            for (var i = 0; i < _systems.Count; i++)
            {
                _systems[i].Start();
            }
        }

        public void Update()
        {
            for (var i = 0; i < _updates.Count; i++)
            {
                _updates[i].Update();
            }
        }

        public void Destroy()
        {
            for (var i = 0; i < _systems.Count; i++)
            {
                _systems[i].Destroy();
            }
        }
    }
}
