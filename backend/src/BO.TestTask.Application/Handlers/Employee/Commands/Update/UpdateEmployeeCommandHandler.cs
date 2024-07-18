using BO.TestTask.Application.Exceptions;

namespace BO.TestTask.Application.Handlers.Employee.Commands.Update;

public sealed class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeCommandResponse>
{
    private readonly AppDbContext _dbContext;
    private readonly IValidator<EmployeeUpdateDto> _validator;

    public UpdateEmployeeCommandHandler(
        AppDbContext dbContext,
        IValidator<EmployeeUpdateDto> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }

    public async Task<UpdateEmployeeCommandResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request.Dto);

        var employee = (await _dbContext.Employees.FindAsync([request.Id], cancellationToken)) ?? throw new NotFoundException();
        EmployeeEntity.Update(
            employee,
            request.Dto.Name,
            request.Dto.DateOfBirth,
            request.Dto.Married,
            request.Dto.Phone,
            request.Dto.Salary);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateEmployeeCommandResponse(true);
    }
}
