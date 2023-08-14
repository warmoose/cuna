namespace cuna.Models;

using System.ComponentModel.DataAnnotations;

public class ThirdPartyStatus
{
    public static string NotFound = "NOT FOUND";
    public static string Initial = "CREATED";
    public static string Started = "STARTED";
    public static string Processed = "PROCESSED";
    public static string Completed = "COMPLETED";
    public static string Error = "ERROR";

    public string Status { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;

    public static bool IsValidStatus(string status)
    {
        return status switch
        {
            "NOT FOUND" => true,
            "INITIAL" => true,
            "STARTED" => true,
            "PROCESSED" => true,
            "COMPLETED" => true,
            "ERROR" => true,
            _ => false,
        };
    }
}

public class ThirdPartyStatusHistory : ThirdPartyStatus
{
    public string Id { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public DateTime LastUpdated { get; set; }
    public ThirdPartyUpdateStatus[] History { get; set; } = Array.Empty<ThirdPartyUpdateStatus>();
}
public record ThirdPartyUpdateStatus
{
    [Required] public string Id = string.Empty;
    [Required] public DateTime Timestamp = DateTime.UtcNow;
    [Required] public string Status = string.Empty;
    [Required] public string Detail = string.Empty;

    public static ThirdPartyUpdateStatus NotFound => new()
    {
        Status = ThirdPartyStatus.NotFound,
    };
}
