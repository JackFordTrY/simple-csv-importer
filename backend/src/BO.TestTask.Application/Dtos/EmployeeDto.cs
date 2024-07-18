namespace BO.TestTask.Application.Dtos;

public class EmployeeDto
{
    public const string NameKey = "name";
    public const string DateOfBirthKey = "dateofbirth";
    public const string MarriedKey = "married";
    public const string PhoneKey = "phone";
    public const string SalaryKey = "salary";

    public string Name { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public bool Married { get; set; }

    public string Phone { get; set; }

    public decimal Salary { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public EmployeeDto()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {

    }

    public EmployeeDto(string name, DateOnly? dateOfBirth, bool? married, string phone, decimal salary)
    {
        Name = name;
        DateOfBirth = dateOfBirth ?? DateOnly.MaxValue;
        Married = married ?? false;
        Phone = phone;
        Salary = salary;
    }
}
