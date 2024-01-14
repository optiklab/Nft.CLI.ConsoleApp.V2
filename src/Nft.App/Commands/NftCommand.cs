using System.CommandLine;

namespace Nft.App.Commands
{
    public class NftCommand : Command
    {
        public NftCommand()
            : base(name: "--nft", "Returns ownership information for the nft with the given id.")
        {
            var nftAgrument = new Argument<string>("tokenId", "NFT token Id.");

            AddArgument(nftAgrument);
        }
    }
}