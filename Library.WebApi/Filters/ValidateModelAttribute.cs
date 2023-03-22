namespace Library.WebApi.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class ValidateModelAttribute<T> : Attribute, IActionFilter where T : class
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var param = context.ActionArguments.SingleOrDefault(p => p.Value is T);
        if (param.Value == null)
        {
            context.Result = new BadRequestObjectResult("Model is null");
            return;
        }

        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
        if (validator == null)
        {
            context.Result = new ObjectResult($"Validator for type {nameof(T)} is not exists")
            {
            StatusCode = 500
        };
            return;
        }

        var dto = (T)param.Value;
        if (!validator.Validate(dto).IsValid)
        {
            context.Result = new BadRequestObjectResult("Model is not valid");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
