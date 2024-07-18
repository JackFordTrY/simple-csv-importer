namespace BO.TestTask.Application.Dtos;

public class EmployeeUpdateDto
{
    public string? Name { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public bool? Married { get; set; }

    public string? Phone { get; set; }

    public decimal? Salary { get; set; }

    public EmployeeUpdateDto(string? name, DateOnly? dateOfBirth, bool? married, string? phone, decimal? salary)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
        Married = married;
        Phone = phone;
        Salary = salary;
    }
}
