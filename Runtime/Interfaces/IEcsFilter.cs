using System;
using System.Collections.Generic;

namespace ECS.Interfaces
{
    public interface IEcsFilter
    {
        int Id { get; }
        IReadOnlyList<IEcsEntity> Entities { get; }
        IEcsFilter Include(params Enum[] components);
        IEcsFilter Exclude(params Enum[] components);
        IEcsFilter Apply();
    }
}
