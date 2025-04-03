using ECS.Common;
using ECS.Entity;
using ECS.Interfaces;
using System;
using System.Collections.Generic;

namespace ECS.Filter
{
    public class EcsFilter : List<IEcsEntity>, IEcsFilter
    {
        public int Id { get; private set; }
        public IReadOnlyList<IEcsEntity> Entities => this;

        private int[] _includeComponents;
        private int[] _excludeComponents;
        private readonly EcsContext _context;

        public EcsFilter(EcsContext context) : base(EcsConfig.FilterCapacity)
        {
            _context = context;
            _includeComponents = new int[0];
            _excludeComponents = new int[0];
        }

        public IEcsFilter Include(params Enum[] components)
        {
            _includeComponents = new int[components.Length];

            for (var i = 0; i < components.Length; i++)
            {
                _includeComponents[i] = components[i].GetHashCode();
            }

            return this;
        }

        public IEcsFilter Exclude(params Enum[] components)
        {
            _excludeComponents = new int[components.Length];

            for (var i = 0; i < components.Length; i++)
            {
                _excludeComponents[i] = components[i].GetHashCode();
            }

            return this;
        }

        public IEcsFilter Apply()
        {
            Id = GetId();
            _context.Filters.Insert(this);

            for (var i = 0; i < _includeComponents.Length; i++)
            {
                _context.Components[_includeComponents[i]].AddFilter(Id);
            }

            for (var i = 0; i < _excludeComponents.Length; i++)
            {
                _context.Components[_excludeComponents[i]].AddFilter(Id);
            }

            return this;
        }

        public void Update()
        {
            Clear();

            for (var i = 0; i < _context.Entities.Count; i++)
            {
                var entity = _context.Entities[i];

                if (entity != null)
                {
                    if (IsInclude(entity) && WithExclude(entity))
                    {
                        Add(entity);
                    }
                }
            }
        }

        private bool IsInclude(EcsEntity entity)
        {
            for (var i = 0; i < _includeComponents.Length; i++)
            {
                if (!entity.HasComponent(_includeComponents[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private bool WithExclude(EcsEntity entity)
        {
            for (var i = 0; i < _excludeComponents.Length; i++)
            {
                if (entity.HasComponent(_excludeComponents[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private int GetId()
        {
            unchecked
            {
                var hash = 17;

                for (var i = 0; i < _includeComponents.Length; i++)
                {
                    hash = hash * 23 + _includeComponents[i].GetHashCode();
                }

                hash = hash * 23 + 867_5309;

                for (var i = 0; i < _excludeComponents.Length; i++)
                {
                    hash = hash * 23 + _excludeComponents[i].GetHashCode();
                }

                return hash;
            }
        }
    }
}
