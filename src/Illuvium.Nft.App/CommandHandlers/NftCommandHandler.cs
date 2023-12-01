using Illuvium.Nft.Common;
using System.CommandLine.Invocation;

namespace Illuvium.Nft.App.Commands
{
    public class NftCommandHandler : ICommandHandler
    {
        private readonly IConsoleOutputHandlers _consoleOutputHandlers;

        public string TokenId { get; set; }

        public NftCommandHandler(IConsoleOutputHandlers consoleOutputHandlers)
        {
            _consoleOutputHandlers = consoleOutputHandlers ?? throw new ArgumentNullException(nameof(consoleOutputHandlers));
        }

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            Console.WriteLine(await _consoleOutputHandlers.ShowOwnerAsync(TokenId));

            return 0;
        }

        public int Invoke(InvocationContext context)
        {
            Console.WriteLine(_consoleOutputHandlers.ShowOwnerAsync(TokenId).ConfigureAwait(false));

            return 0;
        }
    }
}