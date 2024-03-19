using Application.Features.Appointments.Commands.Create;
using Application.Features.Appointments.Commands.Delete;
using Application.Features.Appointments.Commands.Update;
using Application.Features.Appointments.Queries.GetById;
using Application.Features.Appointments.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]

public class AppointmentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateAppointmentCommand createAppointmentCommand)
    {
        CreatedAppointmentResponse response = await Mediator.Send(createAppointmentCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAppointmentCommand updateAppointmentCommand)
    {
        UpdatedAppointmentResponse response = await Mediator.Send(updateAppointmentCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedAppointmentResponse response = await Mediator.Send(new DeleteAppointmentCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdAppointmentResponse response = await Mediator.Send(new GetByIdAppointmentQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAppointmentQuery getListAppointmentQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListAppointmentListItemDto> response = await Mediator.Send(getListAppointmentQuery);
        return Ok(response);
    }
}