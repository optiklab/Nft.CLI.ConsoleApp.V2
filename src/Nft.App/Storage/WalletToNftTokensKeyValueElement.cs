using System.Xml.Serialization;

namespace Nft.App.Storage
{

    public class WalletToNftTokensKeyValueElement
    {
        [XmlElement("Key")]
        public string Key { get; set; }

        [XmlElement("Value")]
        public List<string> Value { get; set; }
    }
}
