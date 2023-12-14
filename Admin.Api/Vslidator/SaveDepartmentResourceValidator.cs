using Admin.Api.Resources;
using FluentValidation;

namespace Admin.Api.Vslidator
{
    public class SaveDepartmentResourceValidator : AbstractValidator<SaveDepartmentResouorce>
    {

        public SaveDepartmentResourceValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty()
                .MaximumLength(150);
        }
    }
}
