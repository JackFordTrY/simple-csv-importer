namespace BO.TestTask.Application.Handlers.Employee.Commands.ImportFile;

public sealed class ImportFileCommandResponse
{
    public bool IsSuccess { get; set; }

    public IReadOnlyList<string> Errors { get; set; }

    public ImportFileCommandResponse(bool isSuccess, IReadOnlyList<string> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }
}
