using Microsoft.AspNetCore.Mvc;

namespace cuna.Controllers;

using Models;
using Services;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("[controller]")]
public class StatusController
{
    private readonly ILogger<StatusController> _logger;
    private readonly IThirdPartyService _service;

    public StatusController(ILogger<StatusController> logger, IThirdPartyService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet("{key}")]
    public async Task<ThirdPartyStatus> GetAsync([Required] string key)
    {
        return await _service.GetAsync(key);
    }

}