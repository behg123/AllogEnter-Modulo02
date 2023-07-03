using System.Data;
using FluentValidation;

namespace Univali.Api.Features.Courses.Commands.CreateCourse;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .WithMessage("You should fill out a Title")
            .MaximumLength(100)
            .WithMessage("The Title shouldn't have more than 100 characteres");

        RuleFor(c => c.Price)
            .NotEmpty()
            .WithMessage("You should fill out a Price")
            .PrecisionScale(7,2,true)
            .WithMessage("The Price should have a maximum precision of 7 digits, with 2 decimal places");
        
        RuleFor(c => c.Description)
            .NotEmpty()
            .WithMessage("You should fill out a Description");
    }
}