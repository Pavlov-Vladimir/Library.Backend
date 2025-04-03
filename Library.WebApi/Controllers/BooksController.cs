namespace Library.WebApi.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly ILibraryDataProvider _dataProvider;
    private readonly ILibraryService _libraryService;
    private readonly IMapper _mapper;
    private readonly IRedisCacheService _cache;

    public BooksController(ILibraryDataProvider dataProvider,
                           ILibraryService service,
                           IMapper mapper,
                           IRedisCacheService cache)
    {
        _dataProvider = dataProvider;
        _libraryService = service;
        _mapper = mapper;
        _cache = cache;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks([FromQuery] string? order)
    {
        var cancellationToken = HttpContext.RequestAborted;
        bool isOrdered = Enum.TryParse<OrderByProperty>(order, true, out OrderByProperty orderBy);
        var cacheKey = isOrdered && Enum.IsDefined(typeof(OrderByProperty), orderBy)
            ? $"books_ordered_by_{order!.ToLower()}"
            : "books";

        var bookDtos = await _cache.GetDataAsync<List<GetBookDto>>(cacheKey, cancellationToken);

        if (bookDtos is null)
        {
            var books = isOrdered
                ? await _dataProvider.GetBooksAsync(orderBy, cancellationToken)
                : await _dataProvider.GetBooksAsync(ct: cancellationToken);

            bookDtos = _mapper.Map<List<GetBookDto>>(books);

            SetDataCache(cacheKey, bookDtos, cancellationToken);
        }
        else if (isOrdered)
        {
            bookDtos = orderBy switch
            {
                OrderByProperty.Title => bookDtos.OrderBy(b => b.Title).ToList(),
                OrderByProperty.Author => bookDtos.OrderBy(b => b.Author).ToList(),
                _ => bookDtos
            };
        }

        return bookDtos is null ? StatusCode(500) : Ok(bookDtos);
    }

    [HttpGet]
    [Route("/api/recommended")]
    public async Task<IActionResult> GetRecommended([FromQuery] string? genre = null)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var cacheKey = genre is not null ? $"recommended_{genre.ToLower()}" : "recommended";

        var bookDtos = await _cache.GetDataAsync<List<GetBookDto>>(cacheKey, cancellationToken);

        if (bookDtos is null)
        {
            var books = await _dataProvider.GetRecommendedAsync(genre, cancellationToken);

            bookDtos = _mapper.Map<List<GetBookDto>>(books);

            SetDataCache(cacheKey, bookDtos, cancellationToken);
        }

        return bookDtos is null ? StatusCode(500) : Ok(bookDtos);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetBookById([FromRoute] int id)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var cacheKey = $"book_{id}";

        var bookDto = await _cache.GetDataAsync<DetailsBookDto>(cacheKey, cancellationToken);

        if (bookDto is null)
        {
            var book = await _dataProvider.GetBookByIdAsync(id, cancellationToken);

            if (book is null)
                return NotFound("The book is not found.");

            bookDto = _mapper.Map<DetailsBookDto>(book);

            SetDataCache(cacheKey, bookDto, cancellationToken);
        }

        return bookDto is null ? StatusCode(500) : Ok(bookDto);
    }


    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteBook([FromRoute] int id, [FromQuery] string key)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var config = HttpContext.RequestServices.GetService<IConfiguration>();
        var secretKey = config?["SecretKey"];
        if (key != secretKey)
            return BadRequest();

        var book = await _dataProvider.GetBookByIdAsync(id, cancellationToken);
        if (book is null)
            return NotFound("The book is not found.");

        await _libraryService.DeleteBookAsync(id, cancellationToken);

        await RemoveBooksCacheEntries(id, cancellationToken);

        return NoContent();
    }

    [HttpPost]
    [Route("save")]
    [ValidateModel<BookDto>]
    public async Task<IActionResult> SaveBook([FromBody] BookDto dto)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var book = _mapper.Map<Book>(dto);
        if (book == null)
            return StatusCode(500);

        if (book.Id == default)
        {
            var bookId = await _libraryService.AddBookAsync(book, cancellationToken);
            return Created("id", new { id = bookId });
        }

        await _libraryService.UpdateBookAsync(book, cancellationToken);

        await _cache.RemoveDataAsync($"book_{book.Id}", cancellationToken);

        return Ok(new { id = book.Id });
    }

    [HttpPut]
    [Route("{id:int}/review")]
    [ValidateModel<ReviewDto>]
    public async Task<IActionResult> SaveReview([FromRoute] int id, [FromBody] ReviewDto dto)
    {
        var cancellationToken = HttpContext.RequestAborted;

        var review = _mapper.Map<Review>(dto, opt => opt.Items["BookId"] = id);

        if (review == null)
            return StatusCode(500);

        var reviewId = await _libraryService.AddReviewAsync(review, cancellationToken);

        await _cache.RemoveDataAsync($"book_{id}", cancellationToken);

        return Ok(new { id = reviewId });
    }

    [HttpPut]
    [Route("{id:int}/rate")]
    [ValidateModel<RatingDto>]
    public async Task<IActionResult> AddRate([FromRoute] int id, [FromBody] RatingDto dto)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var rating = _mapper.Map<Rating>(dto, opt => opt.Items["BookId"] = id);
        if (rating == null)
            return StatusCode(500);

        await _libraryService.AddRatingAsync(rating, cancellationToken);

        await _cache.RemoveDataAsync($"book_{id}", cancellationToken);

        return Ok();
    }


    private void SetDataCache<T>(string cacheKey, T data, CancellationToken ct)
    {
        if (data is null) return;

        _ = Task.Run(async () =>
        {
            try
            {
                await _cache.SetDataAsync(cacheKey, data, ct);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cache set failed: {ex}");
            }
        }, ct);
    }

    private async Task RemoveBooksCacheEntries(int id, CancellationToken ct)
    {
        await _cache.RemoveDataAsync($"book_{id}", ct);
        await _cache.RemoveDataAsync($"books", ct);
        await _cache.RemoveDataAsync($"recommended", ct);

        foreach (var orderBy in Enum.GetNames<OrderByProperty>())
        {
            await _cache.RemoveDataAsync($"books_ordered_by_{orderBy.ToLower()}", ct);
        }
    }
}
