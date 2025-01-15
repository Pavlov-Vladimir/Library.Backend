using System.Text.RegularExpressions;

namespace Library.WebApi.Validation.Validators;

public class BookDtoValidator : AbstractValidator<BookDto>
{
    public BookDtoValidator()
    {
        RuleFor(dto => dto.Id).Must(id => id == null || id > 0)
            .WithMessage("Incorrect book id");

        RuleFor(dto => dto.Title).NotEmpty().Length(1, 250)
            .WithMessage("The Title is required and length must be less then 250");

        RuleFor(dto => dto.Author).NotEmpty().Length(1, 100)
            .WithMessage("The Author is required and length must be less then 100");

        RuleFor(dto => dto.Content).NotEmpty()
            .WithMessage("The Content is required");

        RuleFor(dto => dto.Cover).Must(c => string.IsNullOrEmpty(c) || IsBase64String(c))
            .WithMessage("Wrong data format for Cover");

        RuleFor(dto => dto.Genre).NotEmpty().Length(1, 50)
            .WithMessage("The Genre is required and length must be less then 50");
    }

    private bool IsBase64String(string base64)
    {
        var base64Regex = new Regex(@"^(?:[A-Za-z0-9+\/]{4})*(?:[A-Za-z0-9+\/]{2}==|[A-Za-z0-9+\/]{3}=)?$");
        return base64Regex.IsMatch(base64.Split(',')[1]);
    }
}