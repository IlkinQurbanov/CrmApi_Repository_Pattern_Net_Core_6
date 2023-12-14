using Admin.Api.Resources;
using FluentValidation;

namespace Admin.Api.Vslidator
{
    public class SaveServiceResourceValidator : AbstractValidator<SaveServiceResource>
    {

        public SaveServiceResourceValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
