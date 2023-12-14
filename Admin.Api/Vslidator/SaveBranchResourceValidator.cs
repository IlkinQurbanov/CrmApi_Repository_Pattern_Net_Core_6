using Admin.Api.Resources;
using FluentValidation;

namespace Admin.Api.Vslidator
{
    public class SaveBranchResourceValidator : AbstractValidator<SaveBranchResource>
    {
        public SaveBranchResourceValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(90);
        }
    }
}
