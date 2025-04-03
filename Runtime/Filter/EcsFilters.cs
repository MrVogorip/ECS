using ECS.Common;
using ECS.Interfaces;
using System.Collections.Generic;

namespace ECS.Filter
{
    public class EcsFilters : Dictionary<int, EcsFilter>
    {
        private readonly EcsContext _context;
        private readonly HashSet<int> _updates;

        public EcsFilters(EcsContext context) : base(EcsConfig.FilterCapacity)
        {
            _context = context;
            _updates = new HashSet<int>(EcsConfig.FilterCapacity);
        }

        public IEcsFilter Make()
        {
            return new EcsFilter(_context);
        }

        public void Insert(EcsFilter filter)
        {
            this[filter.Id] = filter;
        }

        public void Enqueue(ISet<int> filtres)
        {
            _updates.UnionWith(filtres);
        }

        public void Update()
        {
            if (_updates.Count == 0)
            {
                return;
            }

            foreach (var filterId in _updates)
            {
                this[filterId].Update();
            }

            _updates.Clear();
        }

        public void Destroy()
        {
            foreach (var filter in Values)
            {
                filter.Clear();
            }
        }
    }
}
