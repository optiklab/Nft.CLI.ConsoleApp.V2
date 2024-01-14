namespace Nft.App.Models
{
    public class NFTToken
    {
        public string TokenId { get; set; }

        /// <summary>
        /// Allows to efficiently insert new owners.
        /// </summary>
        public LinkedList<OwnershipInfo> OwnershipInfo { get; set; }
    }
}
