namespace Library.WebApi.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class CheckIfExistsAttribute : Attribute, IActionFilter
{
    private readonly ILibraryDataProvider _dataProvider;

    public CheckIfExistsAttribute(ILibraryDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public async void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.RouteData.Values.ContainsKey("id")
            || !int.TryParse((string?)context.RouteData.Values["id"], out int id))
        {
            context.Result = new BadRequestObjectResult("Bad id parameter.");
            return;
        }

        var book = await _dataProvider.GetBookByIdAsync(id);
        if (book == null)
        {
            context.Result = new NotFoundObjectResult("The book is not found.");
        }
        else
        {
            context.HttpContext.Items.Add("book", book);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
