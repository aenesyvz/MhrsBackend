using Application.Features.Prescriptions.Commands.Create;
using Application.Features.Prescriptions.Commands.Delete;
using Application.Features.Prescriptions.Commands.Update;
using Application.Features.Prescriptions.Queries.GetById;
using Application.Features.Prescriptions.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]

public class PrescriptionsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreatePrescriptionCommand createPrescriptionCommand)
    {
        CreatedPrescriptionResponse response = await Mediator.Send(createPrescriptionCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePrescriptionCommand updatePrescriptionCommand)
    {
        UpdatedPrescriptionResponse response = await Mediator.Send(updatePrescriptionCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedPrescriptionResponse response = await Mediator.Send(new DeletePrescriptionCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdPrescriptionResponse response = await Mediator.Send(new GetByIdPrescriptionQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListPrescriptionQuery getListPrescriptionQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListPrescriptionListItemDto> response = await Mediator.Send(getListPrescriptionQuery);
        return Ok(response);
    }
}