using Erm.BusinessLayer;
using Erm.BusinessLayer.Services;

using Microsoft.AspNetCore.Mvc;

namespace Erm.PresentationLayer.WebApi.Controllers;

[ApiController]
[Route("api/riskprofiles")]
public sealed class RiskProfileController : ControllerBase
{
    private readonly RiskProfileService _riskProfileService;

    public RiskProfileController()
    {
        _riskProfileService = new();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RiskProfileInfo riskProfileInfo)
    {
        await _riskProfileService.CreateAsync(riskProfileInfo);
        return Ok("Новый риск успешно создан.");
    }

    [HttpGet]
    public async Task<IActionResult> Query([FromQuery] string? query, [FromQuery] string? name)
    {
        if (!string.IsNullOrEmpty(query))
            return Ok(await _riskProfileService.QueryAsync(query));
        else if (!string.IsNullOrEmpty(name))
            return Ok(await _riskProfileService.GetAsync(name));
        else
            return BadRequest();
    }
    
    [HttpDelete]
    [Route("{name}")]
    public async Task<IActionResult> Delete([FromRoute] string name)
    {
        await _riskProfileService.DeleteAsync(name);
        return Ok("Риск успешно удалён.");
    }

    [HttpPut]
    [Route("{name}")]
    public async Task<IActionResult> Update([FromRoute] string name, [FromBody] RiskProfileInfo riskProfileInfo)
    {
        await _riskProfileService.UpdateAsync(name, riskProfileInfo);
        return Ok("Риск успешно обновился");
    }
}