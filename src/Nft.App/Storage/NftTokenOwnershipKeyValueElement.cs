using Nft.App.Models;
using System.Xml.Serialization;

namespace Nft.App.Storage
{
    public class NftTokenOwnershipKeyValueElement
    {
        [XmlElement("Key")]
        public string Key { get; set; }

        [XmlElement("Value")]
        public NFTToken Value { get; set; }
    }
}
