using Illuvium.Nft.App.Models;
using Illuvium.Nft.App.Storage;

namespace Illuvium.Nft.App.Repository
{
    /// <summary>
    /// Implements most primitive way to persist the state.
    /// Though, this way is NOT something I would use for production: 
    /// https://learn.microsoft.com/en-us/dotnet/standard/serialization/binaryformatter-security-guide
    /// </summary>
    public class XmlFileSystem : IFileSystem
    {
        private const string IlluviumDb = "IlluviumDb.xml";

        public async Task<TokenStorage> ReadAsync()
        {
            if (File.Exists(IlluviumDb))
            {
                try
                {
                    using (StreamReader sr = File.OpenText(IlluviumDb))
                    {
                        var tokenStorageSer = XmlHelpers.ConvertStringToXMLObject(typeof(TokenStorageSer), sr.ReadToEnd()) as TokenStorageSer;

                        if (tokenStorageSer != null)
                        {
                            var tokenStorage = new TokenStorage();
                            tokenStorage.NftTokenWalletMap = tokenStorageSer.SerializableNftTokenWalletMap.ToDictionary(x => x.Key, x => x.Value);
                            tokenStorage.WalletNftTokensMap = tokenStorageSer.SerializableWalletNftTokensMap.ToDictionary(x => x.Key, x => x.Value);
                            tokenStorage.NftTokenOwnershipMap = tokenStorageSer.SerializableNftTokenOwnershipMap
                                .ToDictionary(
                                x => x.Key,
                                x => new Models.NFTToken
                                {
                                    TokenId = x.Value.TokenId,
                                    OwnershipInfo = ConvertFromOwnership(x.Value.OwnershipInfo)
                                });

                            return await Task<TokenStorage>.FromResult(tokenStorage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return null;
        }

        public async Task<bool> DeleteAsync()
        {
            if (File.Exists(IlluviumDb))
            {
                File.Delete(IlluviumDb);

                return await Task<bool>.FromResult(true);
            }

            return await Task<bool>.FromResult(false);
        }

        public async Task<bool> SaveAsync(TokenStorage tokenStorage)
        {
            bool result = true;

            var ser = new TokenStorageSer();
            ser.SerializableNftTokenWalletMap = tokenStorage.NftTokenWalletMap.Select(x => new NftTokenToWalletKeyValueElement { Key = x.Key, Value = x.Value }).ToList();
            ser.SerializableWalletNftTokensMap = tokenStorage.WalletNftTokensMap.Select(x => new WalletToNftTokensKeyValueElement { Key = x.Key, Value = x.Value }).ToList();
            ser.SerializableNftTokenOwnershipMap = tokenStorage.NftTokenOwnershipMap
                .Select(x => new NftTokenOwnershipKeyValueElement
                {
                    Key = x.Key,
                    Value = new Storage.NFTToken
                    {
                        TokenId = x.Value.TokenId,
                        OwnershipInfo = ConvertToOwnership(x.Value.OwnershipInfo)
                    }
                }).ToList();

            try
            {
                string tokenStorageString = XmlHelpers.ConvertXMLObjectToString(typeof(TokenStorageSer), ser);

                using (StreamWriter sr = File.CreateText(IlluviumDb))
                {
                    sr.Write(tokenStorageString);
                    sr.Close();
                }
            }
            catch
            {
                result = false;
            }

            return await Task<bool>.FromResult(result);
        }

        private List<OwnershipInfo> ConvertToOwnership(LinkedList<OwnershipInfo> list)
        {
            var result = new List<OwnershipInfo>();

            foreach (var item in list)
            {
                result.Add(item);
            }

            return result;
        }

        private LinkedList<OwnershipInfo> ConvertFromOwnership(List<OwnershipInfo> list)
        {
            var result = new LinkedList<OwnershipInfo>();

            foreach (var item in list)
            {
                result.AddFirst(item);
            }

            return result;
        }
    }
}
