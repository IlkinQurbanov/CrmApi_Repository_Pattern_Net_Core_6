using Admin.Api.Resources;
using FluentValidation;

namespace Admin.Api.Vslidator
{
    public class SaveGroupResourceValidator : AbstractValidator<SaveGroupResource>
    {

        public SaveGroupResourceValidator()
        {
            RuleFor(g => g.GroupNumber)
                   .NotEmpty()
                   .WithMessage("Group Number must not be empty.")
                   .GreaterThan(0)
                   .WithMessage("Group Number must be greater than 0.");

            RuleFor(g => g.StartDate)
                .NotEmpty()
                .WithMessage("Start Date must not be empty.");



            //RuleFor(g => g.ActualEndDate)
            //    .NotEmpty()
            //    .WithMessage("Actual End Date must not be empty.");

            RuleFor(g => g.ServiceId)
                .NotEmpty()
                .WithMessage("Service Id must not be empty.")
                .GreaterThan(0)
                .WithMessage("Service Id must be greater than 0.");

            RuleFor(g => g.NumberOfLessonsPerWeek)
                .NotEmpty()
                .WithMessage("Number Of Lessons Per Week must not be empty.")
                .GreaterThan(0)
                .WithMessage("Number Of Lessons Per Week must be greater than 0.");


            RuleFor(g => g.LessonTypeId)
                .NotEmpty()
                .WithMessage("Lesson Type Id must not be empty.")
                .GreaterThan(0)
                .WithMessage("Lesson Type Id must be greater than 0.");

            RuleFor(g => g.OnlinePassDate)
                .NotEmpty()
                .WithMessage("Online Pass Date must not be empty.");

            RuleFor(g => g.TutorId)
                .NotEmpty()
                .WithMessage("Tutor Id must not be empty.")
                .GreaterThan(0)
                .WithMessage("Tutor Id must be greater than 0.");

            RuleFor(g => g.StartTime)
                .NotEmpty()
                .WithMessage("Start Time must not be empty.");



            RuleFor(g => g.CountOfStuudents)
                .NotEmpty()
                .WithMessage("Count Of Students must not be empty.")
                .GreaterThan(0)
                .WithMessage("Count Of Students must be greater than 0.");




        }
    }
}
