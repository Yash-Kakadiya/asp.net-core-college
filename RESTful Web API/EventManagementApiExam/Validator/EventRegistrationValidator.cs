using EventManagementApiExam.Models;
using FluentValidation;

// ParticipantName must contain minimum 3 characters
// Email must be in a valid email format
// EventName is required
// Age must be between 18 and 30
// RegistrationDate must not be a future date
namespace EventManagementApiExam.Validator
{
    public class EventRegistrationValidator : AbstractValidator<EventRegistration>
    {
        public EventRegistrationValidator()
        {
            RuleFor(e => e.ParticipantName).NotEmpty().MinimumLength(3).WithMessage("ParticipantName must contain minimum 3 characters.");

            RuleFor(e => e.Email).EmailAddress().WithMessage("Email must be in a valid email format.");

            RuleFor(e => e.EventName).NotEmpty().WithMessage("EventName is required.");
            
            RuleFor(e => e.Age).InclusiveBetween(18, 30).WithMessage("Age must be between 18 and 30.");

            RuleFor(e => e.RegistrationDate).LessThanOrEqualTo(DateTime.Now).WithMessage("RegistrationDate must not be a future date");
        }
    }
}
