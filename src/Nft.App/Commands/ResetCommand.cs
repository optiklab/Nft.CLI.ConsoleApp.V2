using System.CommandLine;

namespace Nft.App.Commands
{
    public class ResetCommand : Command
    {
        public ResetCommand()
            : base(name: "--reset", "Deletes all data previously processed by the program.")
        { }
    }
}