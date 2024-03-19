using Application.Features.Diseases.Commands.Create;
using Application.Features.Diseases.Commands.Delete;
using Application.Features.Diseases.Commands.Update;
using Application.Features.Diseases.Queries.GetById;
using Application.Features.Diseases.Queries.GetList;
using Application.Features.Diseases.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]

public class DiseasesController : BaseController
{
    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] CreateDiseaseCommand createDiseaseCommand)
    {
        CreatedDiseaseResponse response = await Mediator.Send(createDiseaseCommand);

        return Created(uri: "", response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateDiseaseCommand updateDiseaseCommand)
    {
        UpdatedDiseaseResponse response = await Mediator.Send(updateDiseaseCommand);

        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedDiseaseResponse response = await Mediator.Send(new DeleteDiseaseCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdDiseaseResponse response = await Mediator.Send(new GetByIdDiseaseQuery { Id = id });
        return Ok(response);
    }

    [HttpGet("GetList")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListDiseaseQuery getListDiseaseQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListDiseaseListItemDto> response = await Mediator.Send(getListDiseaseQuery);
        return Ok(response);
    }

    [HttpGet("GetList/ByDynamic/{id}")]
    public async Task<IActionResult> GetListByDynamic([FromRoute] Guid polyclinicId)
    {
        GetListDiseaseByDynamicModelQuery getListDiseaseByDynamicModelQuery = new() { PolyclinicId = polyclinicId };
        IList<GetListDiseaseByDynamicModelListItemDto> response = await Mediator.Send(getListDiseaseByDynamicModelQuery);

        return Ok(response);
    }
}