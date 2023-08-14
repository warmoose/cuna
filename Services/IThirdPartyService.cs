namespace cuna.Services;

using Models;

public interface IThirdPartyService
{
    /// <summary>
    /// returns the unique id of the request
    /// </summary>
    /// <param name="content"></param>
    /// <returns>unique id</returns>
    Task<string> SubmitAsync(string content);

    /// <summary>
    /// updates the status of the request
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    Task UpdateStatusAsync(ThirdPartyUpdateStatus status);

    /// <summary>
    /// returns the latest status of the request
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ThirdPartyStatus> GetAsync(string id);

    /// <summary>
    /// returns the history of the request
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ThirdPartyStatusHistory> GetHistoryAsync(string id);
}