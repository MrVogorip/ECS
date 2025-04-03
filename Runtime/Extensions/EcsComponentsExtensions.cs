using ECS.Common;
using System;
using System.Collections.Generic;

namespace ECS.Extensions
{
    public static class EcsComponentsExtensions
    {
        private static Dictionary<string, int> _enumComponents;

        public static void InitializeComponents(this EcsContext context, Type enumComponentType)
        {
            _enumComponents = new Dictionary<string, int>(EcsConfig.ComponentCapacity);

            var componentsNames = Enum.GetNames(enumComponentType);
            var componentsValues = Enum.GetValues(enumComponentType);

            for (var i = 0; i < componentsNames.Length; i++)
            {
                _enumComponents[componentsNames[i]] = (int)componentsValues.GetValue(i);
            }
        }

        public static int GetComponentId(this string componentName)
        {
            return _enumComponents[componentName];
        }
    }
}
