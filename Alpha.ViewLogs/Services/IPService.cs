namespace Alpha.ViewLogs.Services
{
    public interface IPService
    {
        string FormatIP(string ip);

        string FormatCountryCode(string ip, string country);
    }
}
