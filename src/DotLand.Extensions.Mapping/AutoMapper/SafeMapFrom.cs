using AutoMapper;

namespace DotLand.Extensions.Mapping.AutoMapper
{
    public abstract class SafeMapFrom<TSource>
        where TSource : class, new()
    {
        public virtual void ConfigureMap(Profile profile) =>
            profile.CreateMap(typeof(TSource), GetType())
                .IgnoreAllNonExistent(GetType(), typeof(TSource));
    }
}