using Application.Features.Models.Queries.GetList;
using Application.Features.Models.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModelsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetListModel([FromQuery] PageRequest pageRequest)
    {
        GetListModelQuery getListModelQuery = new() { PageRequest = pageRequest };
        var response = await Mediator.Send(getListModelQuery);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest,
        [FromBody] DynamicQuery? dynamicQuery = null)
    {
        GetListByDynamicModelQuery getListByDynamicModelQuery =
            new() { PageRequest = pageRequest, DynamicQuery = dynamicQuery };
        var response = await Mediator.Send(getListByDynamicModelQuery);
        return Ok(response);
    }
}