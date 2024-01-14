using System.CommandLine;

namespace Nft.App.Commands
{
    public class ReadFileCommand : Command
    {
        public ReadFileCommand()
            : base(name: "--read-file", "Reads transactions from the ﬁle in the speciﬁed location.")
        {
            var fileAgrument = new Argument<string>("filePath", "File path to read.");

            AddArgument(fileAgrument);
        }
    }
}