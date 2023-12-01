using Illuvium.Nft.Common;
using System.Text;

namespace Illuvium.Nft.App.Cli
{
    /// <summary>
    /// This is fully static class that incapsulates the high-level logic needed for the expected console output.
    /// Every command usually needs to do 3 things:
    /// - Read the data persisted during previous run (if exists),
    /// - Execute the new command
    /// - Persist the new data (on disk in our case)
    /// </summary>
    public class ConsoleOutputHandlers : IConsoleOutputHandlers
    {
        private const string NoStateWarning = $"WARNING! It was not possible to recover previous state of the application. So it will run as of first time.";

        private ITransactionsManager _transactionsManager;

        public ConsoleOutputHandlers(ITransactionsManager transactionsManager) 
        {
            _transactionsManager = transactionsManager;
        }

        public async Task<string> ReadFileAsync(FileInfo file)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            if (!file.Exists)
                throw new ArgumentException("file does not exists");

            if (!await _transactionsManager.InitializeAsync())
            {
                Console.WriteLine(NoStateWarning);
            }

            int result = await _transactionsManager.LoadFromFileAsync(file);

            await _transactionsManager.PersistAsync();

            return $"Read {result} transaction(s)";
        }

        public async Task<string> ReadJsonAsync(string json)
        {
            if (string.IsNullOrEmpty(json))
                throw new ArgumentNullException("json");

            if (!await _transactionsManager.InitializeAsync())
            {
                Console.WriteLine(NoStateWarning);
            }

            int result = await _transactionsManager.LoadFromJsonAsync(json);

            await _transactionsManager.PersistAsync();

            return $"Read {result} transaction(s)";
        }

        public async Task<string> ShowOwnerAsync(string tokenId)
        {
            if (string.IsNullOrEmpty(tokenId))
                throw new ArgumentNullException("tokenId");

            if (!await _transactionsManager.InitializeAsync())
            {
                Console.WriteLine(NoStateWarning);
            }

            string walletId = await _transactionsManager.FindWalletOwnerAsync(tokenId);

            if (string.IsNullOrEmpty(walletId))
            {
                return $"Token {tokenId} is not owned by any wallet";
            }

            return $"Token {tokenId} is owned by {walletId}";
        }

        public async Task<string> ReportTokensAsync(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentNullException("address");

            if (!await _transactionsManager.InitializeAsync())
            {
                Console.WriteLine(NoStateWarning);
            }

            var tokens = await _transactionsManager.GetTokensAsync(address);

            if (tokens.Any())
            {
                var sb = new StringBuilder($"Wallet {address} holds {tokens.Count} Tokens:");

                foreach (var token in tokens)
                {
                    sb.AppendLine(token);
                }

                return sb.ToString();
            }

            return $"Wallet {address} holds no Tokens";
        }

        public async Task<string> ResetAsync()
        {
            if (!await _transactionsManager.InitializeAsync())
            {
                Console.WriteLine(NoStateWarning);
            }

            if (await _transactionsManager.ClearAsync())
            {
                return "Program was reset";
            }

            return "Oops. Something went wrong.";
        }
    }
}
