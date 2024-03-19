using Application.Features.PrescriptionDetails.Commands.Create;
using Application.Features.PrescriptionDetails.Commands.Delete;
using Application.Features.PrescriptionDetails.Commands.Update;
using Application.Features.PrescriptionDetails.Queries.GetById;
using Application.Features.PrescriptionDetails.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]

public class PrescriptionDetailsController : BaseController
{
    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] CreatePrescriptionDetailCommand createPrescriptionDetailCommand)
    {
        CreatedPrescriptionDetailResponse response = await Mediator.Send(createPrescriptionDetailCommand);

        return Created(uri: "", response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdatePrescriptionDetailCommand updatePrescriptionDetailCommand)
    {
        UpdatedPrescriptionDetailResponse response = await Mediator.Send(updatePrescriptionDetailCommand);

        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedPrescriptionDetailResponse response = await Mediator.Send(new DeletePrescriptionDetailCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdPrescriptionDetailResponse response = await Mediator.Send(new GetByIdPrescriptionDetailQuery { Id = id });
        return Ok(response);
    }

    [HttpGet("GetList")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListPrescriptionDetailQuery getListPrescriptionDetailQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListPrescriptionDetailListItemDto> response = await Mediator.Send(getListPrescriptionDetailQuery);
        return Ok(response);
    }
}