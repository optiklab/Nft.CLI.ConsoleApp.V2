using Illuvium.Nft.App.Models;
using System.Xml.Serialization;

namespace Illuvium.Nft.App.Storage
{
    [Serializable]
    public class NFTToken
    {
        [XmlElement("TokenId")]
        public string TokenId { get; set; }

        /// <summary>
        /// Allows to efficiently insert new owners.
        /// </summary>
        [XmlElement("OwnershipInfo")]
        public List<OwnershipInfo> OwnershipInfo { get; set; }
    }
}
