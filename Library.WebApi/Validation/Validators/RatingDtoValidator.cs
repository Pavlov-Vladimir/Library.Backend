namespace Library.WebApi.Validation.Validators;

public class RatingDtoValidator : AbstractValidator<RatingDto>
{
    public RatingDtoValidator()
    {
        RuleFor(dto => dto.Score).InclusiveBetween(1, 5)
            .WithMessage("Score can be from 1 to 5");
    }
}
