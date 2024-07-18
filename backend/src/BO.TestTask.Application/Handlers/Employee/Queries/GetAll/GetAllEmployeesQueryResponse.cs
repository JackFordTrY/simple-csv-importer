namespace BO.TestTask.Application.Handlers.Employee.Queries.GetAll;

public class GetAllEmployeesQueryResponse
{
    public IReadOnlyList<EmployeeEntity> Employees { get; set; }

    public int Total { get; set; }

    public GetAllEmployeesQueryResponse(IReadOnlyList<EmployeeEntity> employees, int total)
    {
        Employees = employees;
        Total = total;
    }
}
