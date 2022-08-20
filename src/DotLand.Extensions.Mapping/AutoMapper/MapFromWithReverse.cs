using AutoMapper;

namespace DotLand.Extensions.Mapping.AutoMapper
{
    public abstract class MapFromWithReverse<TSource>
        where TSource : class, new()
    {
        public virtual void ConfigureMap(Profile profile) =>
            profile.CreateMap(typeof(TSource), GetType())
                .ReverseMap();
    }
}