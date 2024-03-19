using Application.Features.Polyclinics.Commands.Create;
using Application.Features.Polyclinics.Commands.Delete;
using Application.Features.Polyclinics.Commands.Update;
using Application.Features.Polyclinics.Queries.GetById;
using Application.Features.Polyclinics.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]

public class PolyclinicsController : BaseController
{
    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] CreatePolyclinicCommand createPolyclinicCommand)
    {
        CreatedPolyclinicResponse response = await Mediator.Send(createPolyclinicCommand);

        return Created(uri: "", response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdatePolyclinicCommand updatePolyclinicCommand)
    {
        UpdatedPolyclinicResponse response = await Mediator.Send(updatePolyclinicCommand);

        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedPolyclinicResponse response = await Mediator.Send(new DeletePolyclinicCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdPolyclinicResponse response = await Mediator.Send(new GetByIdPolyclinicQuery { Id = id });
        return Ok(response);
    }

    [HttpGet("GetList")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListPolyclinicQuery getListPolyclinicQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListPolyclinicListItemDto> response = await Mediator.Send(getListPolyclinicQuery);
        return Ok(response);
    }
}
