using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace SmartHub.Application.Common.Mappings
{
    /// <summary>
    /// This class scans the Assembly and creates each Profile
    /// (It will be created during startUp - DI)
    /// </summary>
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            ApplyMappingFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(x => x.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping") ?? type.GetInterface("IMapFrom`1")?.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}