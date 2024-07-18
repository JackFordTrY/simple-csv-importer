namespace BO.TestTask.Database.Models;

public class EmployeeEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public bool Married { get; set; }

    public string Phone { get; set; }

    public decimal Salary { get; set; }

    private EmployeeEntity(
        Guid id,
        string name,
        DateOnly dateOfBirth,
        bool married,
        string phone,
        decimal salary)
    {
        Id = id;
        Name = name;
        DateOfBirth = dateOfBirth;
        Married = married;
        Phone = phone;
        Salary = salary;
    }

    public static EmployeeEntity Create(
        string name,
        DateOnly dateOfBirth,
        bool married,
        string phone,
        decimal salary)
    {
        return new EmployeeEntity(
            Guid.NewGuid(),
            name,
            dateOfBirth,
            married,
            phone,
            salary);
    }

    public static void Update(
        EmployeeEntity entity,
        string name,
        DateOnly? dateOfBirth,
        bool? married,
        string phone,
        decimal? salary)
    {
        entity.Name = name ?? entity.Name;
        entity.DateOfBirth = dateOfBirth ?? entity.DateOfBirth;
        entity.Married = married ?? entity.Married;
        entity.Phone = phone ?? entity.Phone;
        entity.Salary = salary ?? entity.Salary;
    }
}
