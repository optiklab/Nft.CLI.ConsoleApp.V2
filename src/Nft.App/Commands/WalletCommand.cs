using System.CommandLine;

namespace Nft.App.Commands
{
    public class WalletCommand : Command
    {
        public WalletCommand()
            : base(name: "--wallet", "Lists all NFTs currently owned by the wallet of the given address.")
        {
            var walletAgrument = new Argument<string>("Address", "Wallet address.");

            AddArgument(walletAgrument);
        }
    }
}