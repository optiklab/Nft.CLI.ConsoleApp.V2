﻿using System.Xml.Serialization;

namespace Nft.App.Storage
{
    public class NftTokenToWalletKeyValueElement
    {
        [XmlElement("Key")]
        public string Key { get; set; }

        [XmlElement("Value")]
        public string Value { get; set; }
    }
}
