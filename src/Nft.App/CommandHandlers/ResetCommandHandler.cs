using Nft.Common;
using System.CommandLine.Invocation;

namespace Nft.App.Commands
{
    public class ResetCommandHandler : ICommandHandler
    {
        private readonly IConsoleOutputHandlers _consoleOutputHandlers;

        public ResetCommandHandler(IConsoleOutputHandlers consoleOutputHandlers)
        {
            _consoleOutputHandlers = consoleOutputHandlers ?? throw new ArgumentNullException(nameof(consoleOutputHandlers));
        }

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            Console.WriteLine(await _consoleOutputHandlers.ResetAsync());

            return 0;
        }

        public int Invoke(InvocationContext context)
        {
            Console.WriteLine(_consoleOutputHandlers.ResetAsync().ConfigureAwait(false));

            return 0;
        }
    }
}