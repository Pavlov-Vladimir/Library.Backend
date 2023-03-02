using System.Linq;

namespace Library.WebApi.Mapping.Profiles;

public class BookMapperProfile : Profile
{
    public BookMapperProfile()
    {
        CreateMap<BookDto, Book>()
            .ForMember(book => book.Id, opt => opt.Condition(dto => dto.Id != null));

        CreateMap<Book, DetailsBookDto>()
            .ForMember(dto => dto.Rating, opt => opt
                .MapFrom(book => book.Ratings.Average(r => r.Score)));
    }
}
