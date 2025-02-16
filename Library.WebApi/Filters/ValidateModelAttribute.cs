namespace Library.WebApi.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class ValidateModelAttribute<T> : Attribute, IActionFilter where T : class
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var param = context.ActionArguments.FirstOrDefault(p => p.Value is T);
        if (param.Key == null || param.Value == null)
        {
            context.Result = new BadRequestObjectResult("Model is null");
            return;
        }

        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
        if (validator == null)
        {
            //context.Result = new ObjectResult($"Validator for type {nameof(T)} is not exists")
            //{
            //    StatusCode = 500
            //};
            return;
        }

        var dto = (T)param.Value;
        var validationResult = validator.Validate(dto);
        if (!validationResult.IsValid)
        {
            context.Result = new BadRequestObjectResult(validationResult.Errors);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
