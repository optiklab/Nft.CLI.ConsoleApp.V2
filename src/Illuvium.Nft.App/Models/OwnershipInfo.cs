using System.Xml.Serialization;

namespace Illuvium.Nft.App.Models
{
    public class OwnershipInfo
    {
        [XmlElement("WalletAddress")]
        public string WalletAddress { get; set; }

        [XmlElement("Timestamp")]
        public DateTime Timestamp {  get; set; }
    }

}
