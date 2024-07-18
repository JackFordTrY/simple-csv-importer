namespace BO.TestTask.Application.Handlers.Employee.Commands.ImportFile;

public sealed class ImportFileCommand : IRequest<ImportFileCommandResponse>
{
    public StreamReader Reader { get; set; }

    public ImportFileCommand(StreamReader reader)
    {
        Reader = reader;
    }
}
