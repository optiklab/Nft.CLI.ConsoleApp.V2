using System.Xml.Serialization;

namespace Nft.App.Models
{
    public class OwnershipInfo
    {
        [XmlElement("WalletAddress")]
        public string WalletAddress { get; set; }

        [XmlElement("Timestamp")]
        public DateTime Timestamp {  get; set; }
    }

}
