using Application.Features.Cities.Commands.Create;
using Application.Features.Cities.Commands.Delete;
using Application.Features.Cities.Commands.Update;
using Application.Features.Cities.Queries.GetById;
using Application.Features.Cities.Queries.GetList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]

public class CitiesController : BaseController
{
    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] CreateCityCommand createCityCommand)
    {
        CreatedCityResponse response = await Mediator.Send(createCityCommand);

        return Created(uri: "", response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateCityCommand updateCityCommand)
    {
        UpdatedCityResponse response = await Mediator.Send(updateCityCommand);

        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedCityResponse response = await Mediator.Send(new DeleteCityCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdCityResponse response = await Mediator.Send(new GetByIdCityQuery { Id = id });
        return Ok(response);
    }

    [HttpGet("GetList")]
    public async Task<IActionResult> GetList()
    {
        GetListCityQuery getListCityQuery = new();
        IList<GetListCityListItemDto> response = await Mediator.Send(getListCityQuery);
        return Ok(response);
    }
}