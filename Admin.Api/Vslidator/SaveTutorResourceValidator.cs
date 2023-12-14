using Admin.Api.Resources;
using FluentValidation;

namespace Admin.Api.Vslidator
{
    public class SaveTutorResourceValidator : AbstractValidator<SaveTutorResource>
    {

        public SaveTutorResourceValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(90);
        }
    }
}
