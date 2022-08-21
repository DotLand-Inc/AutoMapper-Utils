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

        /// <summary>
        /// Configure profile by adding mapping configuration
        /// </summary>
        /// <param name="profile">Profile to configure</param>
        /// <param name="assemblies">Assemblies where to look for models</param>
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

        /// <summary>
        /// Configure profile by adding mapping configuration
        /// </summary>
        /// <param name="profile">Profile to configure</param>
        /// <param name="assembly">Assembly where to look for models</param>
        public static void ConfigureMapping(this Profile profile, Assembly assembly)
        {
            foreach (var type in _mappingConfiguration)
            {
                assembly.GetTypes().Where(
                        t => t.IsClass &&
                             !t.IsAbstract &&
                             t.BaseType?.Name == type.Name)
                    .InvokeAll(profile, MethodName);
            }
        }

        /// <summary>
        /// Used to ignore in-existent properties mapping
        /// </summary>
        /// <param name="expression"><see cref="IMappingExpression"/></param>
        /// <param name="sourceType">Source Type</param>
        /// <param name="destinationType">Destination Type</param>
        /// <returns><see cref="IMappingExpression"/></returns>
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