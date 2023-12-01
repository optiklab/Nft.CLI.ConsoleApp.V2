namespace Illuvium.Nft.Common
{
    public interface IConsoleOutputHandlers
    {
        Task<string> ReadFileAsync(FileInfo file);
        Task<string> ReadJsonAsync(string json);
        Task<string> ShowOwnerAsync(string tokenId);
        Task<string> ReportTokensAsync(string address);
        Task<string> ResetAsync();
    }
}
