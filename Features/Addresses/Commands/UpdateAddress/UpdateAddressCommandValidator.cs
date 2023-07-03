using FluentValidation;

namespace Univali.Api.Features.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
    {
        RuleFor(a => a.Street)
            .NotEmpty()
            .WithMessage("You sould fill out a Street")
            .MaximumLength(50)
            .WithMessage("The Street shouldn't have more than 50 characters");

        RuleFor(a => a.City)
            .NotEmpty()
            .WithMessage("You sould fill out a City")
            .MaximumLength(50)
            .WithMessage("The City shouldn't have more than 50 characters");
    }
}