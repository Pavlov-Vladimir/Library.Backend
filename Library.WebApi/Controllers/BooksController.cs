using Library.Domain.Common.Enums;
using Library.Domain.Contracts.DataProviders;
using Library.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly ILibraryDataProvider _dataProvider;
    private readonly ILibraryService _service;
    private readonly IMapper _mapper;

    public BooksController(ILibraryDataProvider dataProvider,
                           ILibraryService service,
                           IMapper mapper)
    {
        _dataProvider = dataProvider;
        _service = service;
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
    public async Task<IActionResult> GetBookById([FromRoute] int id)
    {
        var book = await _dataProvider.GetBookByIdAsync(id);
        if (book == null)
            return NotFound();

        var bookDto = _mapper.Map<DetailsBookDto>(book);
        if (bookDto == null)
            return StatusCode(500);

        return Ok(bookDto);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteBook([FromRoute] int id, [FromQuery] string key)
    {
        var book = await _dataProvider.GetBookByIdAsync(id);
        if (book == null)
            return NotFound();

        var config = HttpContext.RequestServices.GetService<IConfiguration>();
        var secretKey = config?["SecretKey"];
        if (key != secretKey)
            return BadRequest();

        await _service.DeleteBookAsync(id);
        return NoContent();
    }

    [HttpPost]
    [Route("save")]
    public async Task<IActionResult> SaveBook([FromBody] BookDto dto)
    {
        if (dto == null)
            return BadRequest();

        var validator = HttpContext.RequestServices.GetService<IValidator<BookDto>>();
        if (validator == null)
            return StatusCode(500);

        if (!validator.Validate(dto).IsValid)
            return BadRequest();

        var book = _mapper.Map<Book>(dto);
        if (book == null)
            return StatusCode(500);

        if (book.Id == default)
        {
            var bookId = await _service.AddBookAsync(book);
            return Created("id", new { id = bookId });
        }
        await _service.UpdateBookAsync(book);
        return Ok(new { id = book.Id });
    }

    [HttpPut]
    [Route("{id:int}/review")]
    public async Task<IActionResult> SaveReview([FromRoute] int id, [FromBody] ReviewDto dto)
    {
        if (dto == null)
            return BadRequest();

        var validator = HttpContext.RequestServices.GetService<IValidator<ReviewDto>>();
        if (validator == null)
            return StatusCode(500);

        if (!validator.Validate(dto).IsValid)
            return BadRequest();

        var review = _mapper.Map<Review>(dto, opt => opt.Items["BookId"] = id);
        if (review == null)
            return StatusCode(500);

        var reviewId = await _service.AddReviewAsync(review);
        return Ok(new { id = reviewId });
    }

    [HttpPut]
    [Route("{id:int}/rate")]
    public async Task<IActionResult> AddRate([FromRoute] int id, [FromBody] RatingDto dto)
    {
        if (dto == null)
            return BadRequest();

        var validator = HttpContext.RequestServices.GetService<IValidator<RatingDto>>();
        if (validator == null)
            return StatusCode(500);

        if (!validator.Validate(dto).IsValid)
            return BadRequest();

        var rating = _mapper.Map<Rating>(dto, opt => opt.Items["BookId"] = id);
        if (rating == null)
            return StatusCode(500);

        await _service.AddRatingAsync(rating);
        return Ok();
    }
}
