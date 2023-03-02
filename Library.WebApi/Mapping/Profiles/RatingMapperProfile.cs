namespace Library.WebApi.Mapping.Profiles;

public class RatingMapperProfile : Profile
{
    public RatingMapperProfile()
    {
        CreateMap<RatingDto, Rating>()
            .ConvertUsing<RatingResolver>();
    }
}
