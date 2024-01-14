using System.CommandLine;

namespace Nft.App.Commands
{
    public class ReadInlineCommand : Command
    {
        public ReadInlineCommand()
            : base(name: "--read-inline", "Reads either a single json element, or an array of json elements representing transactions as an argument.")
        {
            var readInlineAgrument = new Argument<string>("json", "Json text.");

            AddArgument(readInlineAgrument);
        }
    }
}