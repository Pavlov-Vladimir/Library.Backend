namespace Library.WebApi.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class CheckIfExistsAttribute : ActionFilterAttribute
{
    private readonly ILibraryDataProvider _dataProvider;

    public CheckIfExistsAttribute(ILibraryDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
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
            return;
        }

        context.HttpContext.Items.Add("book", book);
        await next();  // Proceed to the action
    }
}