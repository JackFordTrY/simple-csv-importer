using static BO.TestTask.Application.Dtos.EmployeeDto;

namespace BO.TestTask.Application.Handlers.Employee.Commands.ImportFile;

public sealed class ImportFileCommandHandler : IRequestHandler<ImportFileCommand, ImportFileCommandResponse>
{
    private readonly AppDbContext _dbContext;
    private readonly IValidator<EmployeeDto> _employeeValidator;

    public ImportFileCommandHandler(
        AppDbContext dbContext,
        IValidator<EmployeeDto> employeeValidator)
    {
        _dbContext = dbContext;
        _employeeValidator = employeeValidator;
    }

    public async Task<ImportFileCommandResponse> Handle(ImportFileCommand request, CancellationToken cancellationToken)
    {
        bool isSuccess = true;
        var errors = new List<string>();
        var employeesToInsert = new List<EmployeeEntity>();

        var line = await request.Reader.ReadLineAsync(cancellationToken);
        int lineIndex = 1;

        if (string.IsNullOrEmpty(line))
        {
            return new ImportFileCommandResponse(isSuccess, errors);
        }

        var properties = GetPropertiesOrder(line, out bool hasHeaderLine);

        if (hasHeaderLine)
        {
            await IncrementLine();
        }

        while (line != null)
        {
            if (line.Equals(string.Empty))
            {
                await IncrementLine();
                continue;
            }

            var dto = GetEmployeeAddDto(line, properties);

            var validationResult = _employeeValidator.Validate(dto);

            if (validationResult.IsValid && isSuccess)
            {
                var employee = EmployeeEntity.Create(
                    dto.Name,
                    dto.DateOfBirth,
                    dto.Married,
                    dto.Phone,
                    dto.Salary);

                employeesToInsert.Add(employee);
            }
            else
            {
                var combinedMessages = string.Join("; ", validationResult.Errors.Select(x => x.ErrorMessage));

                errors.Add($"Errors occured at line {lineIndex}: ${combinedMessages}");
            }

            await IncrementLine();
        }

        if (isSuccess)
        {
            await _dbContext.Employees.AddRangeAsync(employeesToInsert, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return new ImportFileCommandResponse(isSuccess, errors);

        async Task IncrementLine()
        {
            line = await request.Reader.ReadLineAsync(cancellationToken);
            lineIndex++;
        }
    }

    /// <summary>
    /// Returns <see cref="Dictionary{TKey, TValue}"/> representing order of columns in the csv file. Also determines if file has headers
    /// </summary>
    /// <param name="line"></param>
    /// <param name="hasHeaderLine"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private static Dictionary<string, int> GetPropertiesOrder(string line, out bool hasHeaderLine)
    {
        var result = new Dictionary<string, int>();

        string[] properties = typeof(EmployeeEntity).GetProperties()
            .Where(x => !x.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
            .Select(x => x.Name)
            .ToArray();

        string[] splitedLine = line.Split(',');

        if (splitedLine.Length != properties.Length)
        {
            throw new InvalidOperationException("File doesn't have enough columns");
        }

        hasHeaderLine = properties.All(p => splitedLine.Contains(p, StringComparer.OrdinalIgnoreCase));

        FillDictionary(result, hasHeaderLine ? splitedLine : properties);

        return result;
    }

    private static void FillDictionary(Dictionary<string, int> dictionary, IEnumerable<string> source)
    {
        int index = 0;

        foreach (var item in source)
        {
            dictionary[item.ToLower()] = index++;
        }
    }

    private static EmployeeDto GetEmployeeAddDto(string line, Dictionary<string, int> properties)
    {
        string[] splitedLine = line.Split(',');

        return new EmployeeDto(
            splitedLine[properties[NameKey]],
            DateOnly.TryParse(splitedLine[properties[DateOfBirthKey]], out var dob) ? dob : null,
            bool.TryParse(splitedLine[properties[MarriedKey]], out var married) ? married : null,
            splitedLine[properties[PhoneKey]],
            decimal.TryParse(splitedLine[properties[SalaryKey]], out var salary) ? salary : -1);
    }
}
