using Application.Features.Hospitals.Commands.Create;
using Application.Features.Hospitals.Commands.Delete;
using Application.Features.Hospitals.Commands.Update;
using Application.Features.Hospitals.Queries.GetById;
using Application.Features.Hospitals.Queries.GetList;
using Application.Features.Hospitals.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]

public class HospitalsController : BaseController
{
    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] CreateHospitalCommand createHospitalCommand)
    {
        CreatedHospitalResponse response = await Mediator.Send(createHospitalCommand);

        return Created(uri: "", response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateHospitalCommand updateHospitalCommand)
    {
        UpdatedHospitalResponse response = await Mediator.Send(updateHospitalCommand);

        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedHospitalResponse response = await Mediator.Send(new DeleteHospitalCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdHospitalResponse response = await Mediator.Send(new GetByIdHospitalQuery { Id = id });
        return Ok(response);
    }

    [HttpGet("GetList")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListHospitalQuery getListHospitalQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListHospitalListItemDto> response = await Mediator.Send(getListHospitalQuery);
        return Ok(response);
    }

    [HttpPost("GetList/ByDynamic")]
    public async Task<IActionResult> GetListByDynamic([FromBody] DynamicQuery dynamicQuery)
    {
        GetListHospitalByDynamicModelQuery getListHospitalByDynamicModelQuery = new() { DynamicQuery = dynamicQuery };
        IList<GetListHospitalByDynamicModelListItemDto> response = await Mediator.Send(getListHospitalByDynamicModelQuery);

        return Ok(response);
    }
}