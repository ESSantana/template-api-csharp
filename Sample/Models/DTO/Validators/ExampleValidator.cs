using FluentValidation;
using Sample.Core.Resources;

namespace Sample.API.Models.DTO.Validators
{
    public class ExampleValidator : AbstractValidator<ExampleDTO>
    {
        private readonly IResourceLocalizer _localizer;
        public ExampleValidator(IResourceLocalizer localizer)
        {
            _localizer = localizer;

            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0)
                .WithMessage(string.Format(_localizer.GetString("FIELD_FORMAT"), "ID"));

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "NAME"))
                .NotEmpty()
                .WithMessage(string.Format(_localizer.GetString("FIELD_REQUIRED"), "NAME"))
                .MaximumLength(50)
                .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "NAME", 50));

            RuleFor(x => x.Description)
                .MaximumLength(255)
                .WithMessage(string.Format(_localizer.GetString("SIZE_RULE"), "DESCRIPTION", 255));
        }
    }
}
