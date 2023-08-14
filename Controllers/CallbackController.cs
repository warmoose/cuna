using cuna.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace cuna.Controllers;

using Models;

[ApiController]
[Route("[controller]")]
public class CallbackController : ControllerBase
{
    private readonly ILogger<CallbackController> _logger;
    private readonly IThirdPartyService _service;

    public CallbackController(ILogger<CallbackController> logger, IThirdPartyService service)
    {
        _logger = logger;
        _service = service;
    }

    /// <summary>
    /// No Detail is expected in the body since, to the caller, is the first time the status is being updated.
    /// Otherwise this looks identical to the PutAsync method which does expect a Detail in the body.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    [HttpPost("{key}")]
    public async Task<ActionResult> PostAsync([Required] string key, [Required, FromBody] string status)
    {
        if (string.IsNullOrWhiteSpace(key) || !ThirdPartyStatus.IsValidStatus(status))
        {
            _logger.LogError($"Invalid status submitted for {key}: '{status}'");
            return BadRequest();
        }
        await _service.UpdateStatusAsync(new ThirdPartyUpdateStatus()
        {
            Id = key,
            Status = status,
        });
        return NoContent();
    }

    [HttpPut("{key}")]
    public async Task<ActionResult> PutAsync([Required] string key, [Required, FromBody] StatusUpdateDto statusUpdate)
    {
        if (string.IsNullOrWhiteSpace(key) || !ThirdPartyStatus.IsValidStatus(statusUpdate.Status))
        {
            _logger.LogError($"Invalid status submitted for {key}: '{statusUpdate.Status}'");
            return BadRequest();
        }
        await _service.UpdateStatusAsync(new ThirdPartyUpdateStatus()
        {
            Id = key,
            Status = statusUpdate.Status,
            Detail = statusUpdate.Detail
        });
        return NoContent();
    }

    public record StatusUpdateDto
    {
        [Required] public string Status { get; set; } = string.Empty;
        [Required] public string Detail { get; set; } = string.Empty;
    }
}

