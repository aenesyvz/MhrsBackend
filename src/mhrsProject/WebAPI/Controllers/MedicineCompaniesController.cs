using Application.Features.MedicineCompanies.Commands.Create;
using Application.Features.MedicineCompanies.Commands.Delete;
using Application.Features.MedicineCompanies.Commands.Update;
using Application.Features.MedicineCompanies.Queries.GetById;
using Application.Features.MedicineCompanies.Queries.GetByIdWithMedicines;
using Application.Features.MedicineCompanies.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]

public class MedicineCompaniesController : BaseController
{
    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromBody] CreateMedicineCompanyCommand createMedicineCompanyCommand)
    {
        CreatedMedicineCompanyResponse response = await Mediator.Send(createMedicineCompanyCommand);

        return Created(uri: "", response);
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateMedicineCompanyCommand updateMedicineCompanyCommand)
    {
        UpdatedMedicineCompanyResponse response = await Mediator.Send(updateMedicineCompanyCommand);

        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedMedicineCompanyResponse response = await Mediator.Send(new DeleteMedicineCompanyCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdMedicineCompanyResponse response = await Mediator.Send(new GetByIdMedicineCompanyQuery { Id = id });
        return Ok(response);
    }

    [HttpGet("GetByIdWithMedicines/{id}")]
    public async Task<IActionResult> GetByIdWithMedicines([FromRoute] Guid id)
    {
        GetByIdMedicineCompanyWithMedicinesResponse response = await Mediator.Send(new GetByIdMedicineCompanyWithMedicinesQuery { Id = id });
        return Ok(response);
    }

    [HttpGet("GetList")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListMedicineCompanyQuery getListMedicineCompanyQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListMedicineCompanyListItemDto> response = await Mediator.Send(getListMedicineCompanyQuery);
        return Ok(response);
    }
}