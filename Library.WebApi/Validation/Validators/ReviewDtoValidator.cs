namespace Library.WebApi.Validation.Validators;

public class ReviewDtoValidator : AbstractValidator<ReviewDto>
{
    public ReviewDtoValidator()
    {
        RuleFor(dto => dto.Message).NotEmpty()
            .WithMessage("The Message is required");

        RuleFor(dto => dto.Reviewer).NotEmpty().Length(1, 50)
            .WithMessage("The Reviewer is required and length should be less then 50");
    }
}
