using Library.Domain.Contracts.DataProviders;

namespace Library.WebApi.Mapping.Resolvers;

internal class RatingResolver : ITypeConverter<RatingDto, Rating>
{
    private readonly ILibraryDataProvider _dataProvider;

    public RatingResolver(ILibraryDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public Rating Convert(RatingDto dto, Rating rating, ResolutionContext context)
    {
        var bookId = (int)context.Items["BookId"];
        var book = _dataProvider.GetBookByIdAsync(bookId).Result;

        if (book is not null)
        {
            rating ??= new Rating();
            rating.Book = book;
            rating.Score = dto.Score; 
        }

        return rating;
    }
}
