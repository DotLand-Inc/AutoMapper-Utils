using AutoMapper;

namespace DotLand.Extensions.Mapping.AutoMapper
{
    /// <summary>
    /// Used to map current class properties with from/to TSource Model ignoring in-existent
    /// properties
    /// </summary>
    /// <typeparam name="TSource">
    /// Model properties source
    /// </typeparam>
    public abstract class SafeMapFromWithReverse<TSource>
    {
        /// <summary>
        /// Will be executed when project start to configure mapping
        /// </summary>
        /// <param name="profile">Current used profile</param>
        /// <remarks>You can override this function to customise mapping</remarks>
        public virtual void ConfigureMap(Profile profile) =>
            profile.CreateMap(typeof(TSource), GetType())
                .IgnoreAllNonExistent(typeof(TSource), GetType())
                .ReverseMap()
                .IgnoreAllNonExistent(GetType(), typeof(TSource));
    }
}