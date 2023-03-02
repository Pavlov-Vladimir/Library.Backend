using Library.Domain.Contracts.DataProviders;

namespace Library.WebApi.Mapping.Resolvers;

internal class ReviewResolver : ITypeConverter<ReviewDto, Review>
{
    private readonly ILibraryDataProvider _dataProvider;

    public ReviewResolver(ILibraryDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }
    public Review Convert(ReviewDto dto, Review review, ResolutionContext context)
    {
        var bookId = (int)context.Items["BookId"];
        var book = _dataProvider.GetBookByIdAsync(bookId).Result;
        if (book is not null)
        {
            review ??= new Review();
            review.Message = dto.Message;
            review.Reviewer = dto.Reviewer;
            review.Book = book;
        }
        return review;
    }
}