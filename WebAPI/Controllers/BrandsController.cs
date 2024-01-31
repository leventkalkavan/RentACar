using Application.Features.Brands.Commands.Create;
using Application.Features.Brands.Commands.Delete;
using Application.Features.Brands.Commands.Update;
using Application.Features.Brands.Queries.GetById;
using Application.Features.Brands.Queries.GetList;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateBrand([FromBody] CreateBrandCommand createBrandCommand)
    {
        var response = await Mediator.Send(createBrandCommand);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetListBrand([FromQuery] PageRequest pageRequest)
    {
        GetListBrandQuery getListBrandQuery = new() { PageRequest = pageRequest };
        var response = await Mediator.Send(getListBrandQuery);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdBrand([FromRoute] Guid id)
    {
        GetByIdBrandQuery getByIdBrandQuery = new() { Id = id };
        var response = await Mediator.Send(getByIdBrandQuery);
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBrand([FromBody] UpdateBrandCommand updateBrandCommand)
    {
        var response = await Mediator.Send(updateBrandCommand);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand([FromRoute] Guid id)
    {
        var response = await Mediator.Send(new DeleteBrandCommand { Id = id });
        return Ok(response);
    }
}