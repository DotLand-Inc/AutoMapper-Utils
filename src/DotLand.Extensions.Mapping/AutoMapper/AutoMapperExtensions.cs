using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using AutoMapper;

namespace DotLand.Extensions.Mapping.AutoMapper
{
    public static class MappingExtensions
    {
        private static readonly ICollection<Type> _mappingConfiguration = new List<Type>()
        {
            typeof(MapFrom<>),
            typeof(MapFromWithReverse<>),
            typeof(SafeMapFrom<>),
            typeof(SafeMapFromWithReverse<>),
        };

        private const string MethodName = "ConfigureMap";

        public static void ConfigureMapping(this Profile profile, params Assembly?[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                if (assembly != null)
                {
                    profile.ConfigureMapping(assembly);
                }
            }
        }

        public static void ConfigureMapping(this Profile profile, Assembly assembly)
        {
            foreach (var type in _mappingConfiguration)
            {
                assembly.GetTypes().Where(
                        t => t.IsClass &&
                             !t.IsAbstract &&
                             t.IsSubclassOf(type) &&
                             t.GetConstructors(BindingFlags.Instance).Length == 1)
                    .InvokeAll(profile, MethodName);
            }
        }

        public static IMappingExpression IgnoreAllNonExistent(this IMappingExpression expression, Type sourceType, Type destinationType)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var destinationProperties = destinationType.GetProperties(flags);
            foreach (var property in destinationProperties.Where(property => sourceType.GetProperty(property.Name, flags) == null))
            {
                expression.ForMember(property.Name, opt => opt.Ignore());
            }

            return expression;
        }

        private static void InvokeAll(this IEnumerable<Type> types, Profile profile, string methodName)
        {
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod(methodName);
                methodInfo?.Invoke(instance, new object[] { profile });
            }
        }
    }
}