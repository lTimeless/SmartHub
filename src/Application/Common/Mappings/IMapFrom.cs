using AutoMapper;

namespace SmartHub.Application.Common.Mappings
{
    /// <summary>
    /// Add this to an Dto and it will be mapable to en Entity
    /// </summary>
    /// <typeparam name="T">The Entity to the Dto</typeparam>
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType())
                .ReverseMap();
        }
    }
}