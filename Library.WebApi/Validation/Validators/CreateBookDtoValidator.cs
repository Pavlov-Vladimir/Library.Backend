namespace Library.WebApi.Validation.Validators;

public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
{
    public CreateBookDtoValidator()
    {
        RuleFor(dto => dto.Title).NotEmpty().Length(1, 250)
            .WithMessage("The Title is required and length must be less then 250");

        RuleFor(dto => dto.Author).NotEmpty().Length(1, 100)
            .WithMessage("The Author is required and length must be less then 100");

        RuleFor(dto => dto.Content).NotEmpty()
            .WithMessage("The Content is required");

        RuleFor(dto => dto.Cover).Must(c => string.IsNullOrEmpty(c) || c.EndsWith("="))
            .WithMessage("Wrong data format for Cover");

        RuleFor(dto => dto.Genre).NotEmpty().Length(1, 50)
            .WithMessage("The Genre is required and length must be less then 50");
    }
}
