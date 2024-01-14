using Nft.App.Models;

namespace Nft.App.Repository
{
    public interface IFileSystem
    {
        Task<TokenStorage> ReadAsync();

        Task<bool> DeleteAsync();

        Task<bool> SaveAsync(TokenStorage tokenStorage);
    }
}
