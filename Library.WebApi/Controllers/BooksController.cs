namespace Library.WebApi.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly ILibraryDataProvider _dataProvider;
    private readonly ILibraryService _libraryService;
    private readonly IMapper _mapper;

    public BooksController(ILibraryDataProvider dataProvider,
                           ILibraryService service,
                           IMapper mapper)
    {
        _dataProvider = dataProvider;
        _libraryService = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks([FromQuery] string? order)
    {
        var books = Enum.TryParse<OrderByProperty>(order, true, out OrderByProperty orderBy)
            ? await _dataProvider.GetBooksAsync(orderBy)
            : await _dataProvider.GetBooksAsync();

        var bookDtos = _mapper.Map<List<GetBookDto>>(books);
        if (bookDtos == null)
            return StatusCode(500);

        return Ok(bookDtos);
    }

    [HttpGet]
    [Route("/api/recommended")]
    public async Task<IActionResult> GetRecommended([FromQuery] string? genre = null)
    {
        var books = await _dataProvider.GetRecommendedAsync(genre);

        var bookDtos = _mapper.Map<List<GetBookDto>>(books);
        if (bookDtos == null)
            return StatusCode(500);

        return Ok(bookDtos);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ServiceFilter(typeof(CheckIfExistsAttribute))]
    public IActionResult GetBookById()
    {
        var bookDto = _mapper.Map<DetailsBookDto>(HttpContext.Items["book"] as Book);
        if (bookDto == null)
            return StatusCode(500);

        return Ok(bookDto);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ServiceFilter(typeof(CheckIfExistsAttribute))]
    public async Task<IActionResult> DeleteBook([FromRoute] int id, [FromQuery] string key)
    {
        var config = HttpContext.RequestServices.GetService<IConfiguration>();
        var secretKey = config?["SecretKey"];
        if (key != secretKey)
            return BadRequest();

        await _libraryService.DeleteBookAsync(id);
        return NoContent();
    }

    [HttpPost]
    [Route("save")]
    [ValidateModel<BookDto>]
    public async Task<IActionResult> SaveBook([FromBody] BookDto dto)
    {
        var book = _mapper.Map<Book>(dto);
        if (book == null)
            return StatusCode(500);

        if (book.Id == default)
        {
            var bookId = await _libraryService.AddBookAsync(book);
            return Created("id", new { id = bookId });
        }
        await _libraryService.UpdateBookAsync(book);
        return Ok(new { id = book.Id });
    }

    [HttpPut]
    [Route("{id:int}/review")]
    [ValidateModel<ReviewDto>]
    public async Task<IActionResult> SaveReview([FromRoute] int id, [FromBody] ReviewDto dto)
    {
        var review = _mapper.Map<Review>(dto, opt => opt.Items["BookId"] = id);
        if (review == null)
            return StatusCode(500);

        var reviewId = await _libraryService.AddReviewAsync(review);
        return Ok(new { id = reviewId });
    }

    [HttpPut]
    [Route("{id:int}/rate")]
    [ValidateModel<RatingDto>]
    public async Task<IActionResult> AddRate([FromRoute] int id, [FromBody] RatingDto dto)
    {
        var rating = _mapper.Map<Rating>(dto, opt => opt.Items["BookId"] = id);
        if (rating == null)
            return StatusCode(500);

        await _libraryService.AddRatingAsync(rating);
        return Ok();
    }
}
