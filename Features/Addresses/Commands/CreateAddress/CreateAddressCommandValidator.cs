using FluentValidation;

namespace Univali.Api.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {
        RuleFor(a => a.Street)
            .NotEmpty()
            .WithMessage("You should fill out a Street")
            .MaximumLength(50)
            .WithMessage("The Street shoudn't have more than 50 characters");
        
        RuleFor(a => a.City)
            .NotEmpty()
            .WithMessage("You should fill out a City")
            .MaximumLength(50)
            .WithMessage("The City shoudn't have more than 50 characters");
        

    }
}