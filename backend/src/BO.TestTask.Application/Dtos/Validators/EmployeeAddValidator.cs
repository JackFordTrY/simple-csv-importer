namespace BO.TestTask.Application.Dtos.Validators;

public class EmployeeAddValidator : AbstractValidator<EmployeeDto>
{
    public EmployeeAddValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        RuleFor(x => x.DateOfBirth)
            .Must(x => x.ToDateTime(TimeOnly.MinValue) < DateTime.Now);
        RuleFor(x => x.Phone)
            .NotEmpty()
            .MaximumLength(13)
            .Matches("^(\\+\\d{1,2}\\s?)?1?\\-?\\.?\\s?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$");
        RuleFor(x => x.Married).NotNull();
        RuleFor(x => x.Salary).GreaterThanOrEqualTo(0);
    }
}
