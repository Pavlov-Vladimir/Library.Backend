namespace Library.WebApi.Mapping.Profiles;

public class ReviewMapperProfile : Profile
{
    public ReviewMapperProfile()
    {
        CreateMap<ReviewDto, Review>()
            .ConvertUsing<ReviewResolver>();

        CreateMap<Review, DetailsReviewDto>();
    }
}
