namespace BO.TestTask.Application.Dtos.Validators;

public class EmployeeUpdateValidator : AbstractValidator<EmployeeUpdateDto>
{
    public EmployeeUpdateValidator()
    {
        RuleFor(x => x.Name).MaximumLength(255);
        When(x => x.DateOfBirth != null, () =>
        {
            RuleFor(x => x.DateOfBirth)
                .Must(x => x!.Value.ToDateTime(TimeOnly.MinValue) < DateTime.Now);
        });
        RuleFor(x => x.Phone)
            .MaximumLength(13)
            .Matches("^(\\+\\d{1,2}\\s?)?1?\\-?\\.?\\s?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$");
        RuleFor(x => x.Salary).GreaterThanOrEqualTo(0);
    }
}
