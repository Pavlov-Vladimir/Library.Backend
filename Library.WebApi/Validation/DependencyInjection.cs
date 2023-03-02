using Library.WebApi.Validation.Validators;

namespace Library.WebApi.Validation;

public static class DependencyInjection
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<BookDto>, BookDtoValidator>();
        services.AddScoped<IValidator<RatingDto>, RatingDtoValidator>();
        services.AddScoped<IValidator<ReviewDto>, ReviewDtoValidator>();
        services.AddScoped<IValidator<CreateBookDto>, CreateBookDtoValidator>();
        services.AddScoped<IValidator<UpdateBookDto>, UpdateBookDtoValidator>();

        return services;
    }
}
