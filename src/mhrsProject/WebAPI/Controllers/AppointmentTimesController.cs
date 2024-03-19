using Application.Features.AppointmentTimes.Commands.Create;
using Application.Features.AppointmentTimes.Commands.Delete;
using Application.Features.AppointmentTimes.Commands.Update;
using Application.Features.AppointmentTimes.Queries.GetById;
using Application.Features.AppointmentTimes.Queries.GetList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]

public class AppointmentTimesController : BaseController
{
    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] CreateAppointmentTimeCommand createAppointmentTimeCommand)
    {
        CreatedAppointmentTimeResponse response = await Mediator.Send(createAppointmentTimeCommand);

        return Created(uri: "", response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateAppointmentTimeCommand updateAppointmentTimeCommand)
    {
        UpdatedAppointmentTimeResponse response = await Mediator.Send(updateAppointmentTimeCommand);

        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedAppointmentTimeResponse response = await Mediator.Send(new DeleteAppointmentTimeCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdAppointmentTimeResponse response = await Mediator.Send(new GetByIdAppointmentTimeQuery { Id = id });
        return Ok(response);
    }

    [HttpGet("GetList")]
    public async Task<IActionResult> GetList()
    {
        GetListAppointmentTimeQuery getListAppointmentTimeQuery = new();
        IList<GetListAppointmentTimeListItemDto> response = await Mediator.Send(getListAppointmentTimeQuery);
        return Ok(response);
    }
}