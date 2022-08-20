using AutoMapper;

namespace DotLand.Extensions.Mapping.AutoMapper
{
    public abstract class SafeMapFromWithReverse<TSource>
    {
        public virtual void ConfigureMap(Profile profile) =>
            profile.CreateMap(typeof(TSource), GetType())
                .IgnoreAllNonExistent(typeof(TSource), GetType())
                .ReverseMap()
                .IgnoreAllNonExistent(GetType(), typeof(TSource));
    }
}