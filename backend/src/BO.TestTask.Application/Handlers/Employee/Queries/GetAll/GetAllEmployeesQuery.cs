namespace BO.TestTask.Application.Handlers.Employee.Queries.GetAll;

public sealed class GetAllEmployeesQuery : IRequest<GetAllEmployeesQueryResponse>
{
    public int Take { get; set; } = 25;

    public int Skip { get; set; } = 0;

    public string? SortColumn { get; set; }

    public string SortOrder { get; set; } = "Asc";

    public string? Name {  get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public bool? Married { get; set; }

    public string? Phone { get; set; }

    public decimal? Salary { get; set; }
}
