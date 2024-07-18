using Microsoft.AspNetCore.Mvc;

namespace BO.TestTask.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<GetAllEmployeesQueryResponse>> GetAll(
        [FromQuery] GetAllEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UpdateEmployeeCommandResponse>> Update(
        Guid id,
        [FromBody] EmployeeUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new UpdateEmployeeCommand(id, dto), cancellationToken);
        return Ok(response);
    }

    [HttpPost("import")]
    public async Task<ActionResult<ImportFileCommandResponse>> ImportCsv(IFormFile file, CancellationToken cancellationToken)
    {
        using var stream = file.OpenReadStream();
        using var reader = new StreamReader(stream);

        var response = await _mediator.Send(new ImportFileCommand(reader), cancellationToken);
        return Ok(response);
    }
}
