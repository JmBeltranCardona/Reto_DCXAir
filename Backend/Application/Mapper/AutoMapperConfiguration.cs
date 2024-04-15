using AutoMapper;

namespace Application.Mapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg => {
                cfg.AddProfile(new ViewModelToDomainProfile());
                cfg.AddProfile(new DomainToViewModelProfile());
            });
        }
    }
}
