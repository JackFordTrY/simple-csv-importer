namespace BO.TestTask.Application.Handlers.Employee.Commands.Update;

public sealed class UpdateEmployeeCommandResponse
{
    public bool Updated { get; set; }

    public UpdateEmployeeCommandResponse(bool updated)
    {
        Updated = updated;
    }
}
