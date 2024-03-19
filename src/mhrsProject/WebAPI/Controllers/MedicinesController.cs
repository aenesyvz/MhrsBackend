using Application.Features.Medicines.Commands.Create;
using Application.Features.Medicines.Commands.Delete;
using Application.Features.Medicines.Commands.Update;
using Application.Features.Medicines.Queries.GetById;
using Application.Features.Medicines.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]

public class MedicinesController : BaseController
{
    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] CreateMedicineCommand createMedicineCommand)
    {
        CreatedMedicineResponse response = await Mediator.Send(createMedicineCommand);

        return Created(uri: "", response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateMedicineCommand updateMedicineCommand)
    {
        UpdatedMedicineResponse response = await Mediator.Send(updateMedicineCommand);

        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedMedicineResponse response = await Mediator.Send(new DeleteMedicineCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdMedicineResponse response = await Mediator.Send(new GetByIdMedicineQuery { Id = id });
        return Ok(response);
    }

    [HttpGet("GetList")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListMedicineQuery getListMedicineQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListMedicineListItemDto> response = await Mediator.Send(getListMedicineQuery);
        return Ok(response);
    }
}