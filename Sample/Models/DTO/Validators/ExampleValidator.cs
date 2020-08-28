﻿using FluentValidation;

namespace Sample.API.Models.DTO.Validators
{
    public class ExampleValidator : AbstractValidator<ExampleDTO>
    {
        public ExampleValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Invalid Id format");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Name Required");

            RuleFor(x => x.Description)
                .MaximumLength(255)
                .WithMessage("Description length should be less than 255");
        }
    }
}
