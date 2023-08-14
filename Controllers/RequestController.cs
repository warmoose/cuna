using Microsoft.AspNetCore.Mvc;

namespace cuna.Controllers;

using Services;

[ApiController]
[Route("[controller]")]
public class RequestController : ControllerBase
{
    private readonly ILogger<RequestController> _logger;
    private readonly IThirdPartyService _service;

    public RequestController(ILogger<RequestController> logger, IThirdPartyService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] RequestDto request)
    {
        var result = await _service.SubmitAsync(request.Body);
        return Ok(result);
    }

    public class RequestDto
    {
        public string Body { get; set; } = string.Empty;
    }
}
