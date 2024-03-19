using Application.Features.Districts.Commands.Create;
using Application.Features.Districts.Commands.Delete;
using Application.Features.Districts.Commands.Update;
using Application.Features.Districts.Queries.GetById;
using Application.Features.Districts.Queries.GetListByCityId;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]

public class DistrictsController : BaseController
{
    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] CreateDistrictCommand createDistrictCommand)
    {
        CreatedDistrictResponse response = await Mediator.Send(createDistrictCommand);

        return Created(uri: "", response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateDistrictCommand updateDistrictCommand)
    {
        UpdatedDistrictResponse response = await Mediator.Send(updateDistrictCommand);

        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedDistrictResponse response = await Mediator.Send(new DeleteDistrictCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdDistrictResponse response = await Mediator.Send(new GetByIdDistrictQuery { Id = id });
        return Ok(response);
    }

    [HttpPost("GetListByCityId/{cityId}")]
    public async Task<IActionResult> GetListByCityId([FromRoute] Guid cityId)
    {
        GetListDistrictByCityIdQuery listDistrictByCityIdQuery = new() { CityId = cityId};
        IList<GetListDistrictByCityIdModelListItemDto> response = await Mediator.Send(listDistrictByCityIdQuery);
        return Ok(response);
    }
}