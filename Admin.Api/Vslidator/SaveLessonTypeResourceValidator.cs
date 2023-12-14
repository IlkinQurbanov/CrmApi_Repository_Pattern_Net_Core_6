using Admin.Api.Resources;
using FluentValidation;

namespace Admin.Api.Vslidator
{
    public class SaveLessonTypeResourceValidator : AbstractValidator<SaveLessonTypeResource>
    {
        public SaveLessonTypeResourceValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
