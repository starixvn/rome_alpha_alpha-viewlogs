namespace Alpha.ViewLogs.Services
{
    using Alpha.ViewLogs.Models.LoginLogs;
    using Params.LoginLogs;

    public interface LoginLogsService
    {
        PagingLoginLogs GetLogs(GetLogsParam getLogsParam);
    }
}
