using AutoMapper;

namespace DotLand.Extensions.Mapping.AutoMapper
{
    public abstract class MapFrom<TSource>
        where TSource : class, new()
    {
        public virtual void ConfigureMap(Profile profile) =>
            profile.CreateMap(typeof(TSource), GetType());
    }
}