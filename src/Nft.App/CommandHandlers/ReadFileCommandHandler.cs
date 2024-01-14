using Nft.Common;
using System.CommandLine.Invocation;

namespace Nft.App.Commands
{
    public class ReadFileCommandHandler : ICommandHandler
    {
        private readonly IConsoleOutputHandlers _consoleOutputHandlers;

        public string FilePath { get; set; }

        public ReadFileCommandHandler(IConsoleOutputHandlers consoleOutputHandlers)
        {
            _consoleOutputHandlers = consoleOutputHandlers ?? throw new ArgumentNullException(nameof(consoleOutputHandlers));
        }

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            Console.WriteLine(await _consoleOutputHandlers.ReadFileAsync(new FileInfo(FilePath)));

            return 0;
        }

        public int Invoke(InvocationContext context)
        {
            Console.WriteLine(_consoleOutputHandlers.ReadFileAsync(new FileInfo(FilePath)).ConfigureAwait(false));

            return 0;
        }
    }
}