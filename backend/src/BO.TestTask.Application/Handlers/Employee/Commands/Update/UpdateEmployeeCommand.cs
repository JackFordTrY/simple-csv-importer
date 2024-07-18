namespace BO.TestTask.Application.Handlers.Employee.Commands.Update;

public sealed class UpdateEmployeeCommand : IRequest<UpdateEmployeeCommandResponse>
{
    public Guid Id { get; set; }

    public EmployeeUpdateDto Dto { get; set; }

    public UpdateEmployeeCommand(Guid id, EmployeeUpdateDto dto)
    {
        Id = id;
        Dto = dto;
    }
}
