using System.Xml.Serialization;

namespace Nft.App.Storage
{
    [Serializable]
    [XmlRoot("TokenStorage")]
    public class TokenStorageSer
    {
        public TokenStorageSer()
        {
            SerializableNftTokenWalletMap = new List<NftTokenToWalletKeyValueElement>();
            SerializableWalletNftTokensMap = new List<WalletToNftTokensKeyValueElement>();
            SerializableNftTokenOwnershipMap = new List<NftTokenOwnershipKeyValueElement>();
        }

        [XmlElement("SerializableNftTokenWalletMap")]
        public List<NftTokenToWalletKeyValueElement> SerializableNftTokenWalletMap { get; set; }

        [XmlElement("SerializableWalletNftTokensMap")]
        public List<WalletToNftTokensKeyValueElement> SerializableWalletNftTokensMap { get; set; }

        [XmlElement("SerializableNftTokenOwnershipMap")]
        public List<NftTokenOwnershipKeyValueElement> SerializableNftTokenOwnershipMap { get; set; }
    }
}
