using Nft.Common;
using System.CommandLine.Invocation;

namespace Nft.App.Commands
{
    public class WalletCommandHandler : ICommandHandler
    {
        private readonly IConsoleOutputHandlers _consoleOutputHandlers;

        public string Address { get; set; }

        public WalletCommandHandler(IConsoleOutputHandlers consoleOutputHandlers)
        {
            _consoleOutputHandlers = consoleOutputHandlers ?? throw new ArgumentNullException(nameof(consoleOutputHandlers));
        }

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            Console.WriteLine(await _consoleOutputHandlers.ReportTokensAsync(Address));

            return 0;
        }

        public int Invoke(InvocationContext context)
        {
            Console.WriteLine(_consoleOutputHandlers.ReportTokensAsync(Address).ConfigureAwait(false));

            return 0;
        }
    }
}