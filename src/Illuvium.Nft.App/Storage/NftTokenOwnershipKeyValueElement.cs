using Illuvium.Nft.App.Models;
using System.Xml.Serialization;

namespace Illuvium.Nft.App.Storage
{
    public class NftTokenOwnershipKeyValueElement
    {
        [XmlElement("Key")]
        public string Key { get; set; }

        [XmlElement("Value")]
        public NFTToken Value { get; set; }
    }
}
