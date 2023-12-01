using Illuvium.Nft.Common;
using System.CommandLine.Invocation;

namespace Illuvium.Nft.App.Commands
{
    public class ReadInlineCommandHandler : ICommandHandler
    {
        private readonly IConsoleOutputHandlers _consoleOutputHandlers;

        public string Json { get; set; }

        public ReadInlineCommandHandler(IConsoleOutputHandlers consoleOutputHandlers)
        {
            _consoleOutputHandlers = consoleOutputHandlers ?? throw new ArgumentNullException(nameof(consoleOutputHandlers));
        }

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            Console.WriteLine(await _consoleOutputHandlers.ReadJsonAsync(Json));

            return 0;
        }

        public int Invoke(InvocationContext context)
        {
            Console.WriteLine(_consoleOutputHandlers.ReadJsonAsync(Json).ConfigureAwait(false));

            return 0;
        }
    }
}