using FluentAssertions;
using Nft.App;
using Nft.App.Cli;
using Xunit;

namespace Nft.Tests
{
    public class ConsoleOutputHandlersTests
    {
        private const string MintDTransaction = "{ \"Type\": \"Mint\", \"TokenId\": \"0xD000000000000000000000000000000000000000\", \"Address\": \"0x1000000000000000000000000000000000000000\" }";

        [Fact]
        public async Task Successfully_Loads_From_Valid_Json()
        {
            var consoleOutputHandlers = new ConsoleOutputHandlers(new TransactionsManager(new FileSystemMock()));

            string result = await consoleOutputHandlers.ReadJsonAsync(MintDTransaction);

            result.Should().BeEquivalentTo("Read 1 transaction(s)");
        }

        private const string MintThenBurnTransactions = "[{\"Type\": \"Mint\",\"TokenId\": \"0xA000000000000000000000000000000000000000\",\"Address\": \"0x1000000000000000000000000000000000000000\"},{\"Type\": \"Burn\", \"TokenId\": \"0xA000000000000000000000000000000000000000\"}]";
        [Fact]
        public async Task Successfully_Loads_Mint_And_Burns_From_Valid_JsonArray()
        {
            var consoleOutputHandlers = new ConsoleOutputHandlers(new TransactionsManager(new FileSystemMock()));

            string result = await consoleOutputHandlers.ReadJsonAsync(MintThenBurnTransactions);

            result.Should().BeEquivalentTo("Read 2 transaction(s)");
        }

        private const string MintBurnAndTransferTransactions = "[\r\n\t{\r\n\t\t\"Type\": \"Mint\",\r\n\t\t\"TokenId\": \"0xA000000000000000000000000000000000000000\",\r\n\t\t\"Address\": \"0x1000000000000000000000000000000000000000\"\r\n\t},\r\n\t{\r\n\t\t\"Type\": \"Mint\",\r\n\t\t\"TokenId\": \"0xB000000000000000000000000000000000000000\",\r\n\t\t\"Address\": \"0x2000000000000000000000000000000000000000\"\r\n\t},\r\n\t{\r\n\t\t\"Type\": \"Mint\",\r\n\t\t\"TokenId\": \"0xC000000000000000000000000000000000000000\",\r\n\t\t\"Address\": \"0x3000000000000000000000000000000000000000\"\r\n\t},\r\n\t{\r\n\t\t\"Type\": \"Burn\",\r\n\t\t\"TokenId\": \"0xA000000000000000000000000000000000000000\"\r\n\t},\r\n\t{\r\n\t\t\"Type\": \"Transfer\",\r\n\t\t\"TokenId\": \"0xB000000000000000000000000000000000000000\",\r\n\t\t\"From\": \"0x2000000000000000000000000000000000000000\",\r\n\t\t\"To\": \"0x3000000000000000000000000000000000000000\"\r\n\t}\r\n]";
        [Fact]
        public async Task Successfully_Loads_Mint_Burn_And_Transfer_From_Valid_JsonArray()
        {
            var consoleOutputHandlers = new ConsoleOutputHandlers(new TransactionsManager(new FileSystemMock()));

            string result = await consoleOutputHandlers.ReadJsonAsync(MintBurnAndTransferTransactions);

            result.Should().BeEquivalentTo("Read 5 transaction(s)");
        }

        [Fact]
        public async Task Successfully_Loads_From_File()
        {
            var consoleOutputHandlers = new ConsoleOutputHandlers(new TransactionsManager(new FileSystemMock()));

            string result = await consoleOutputHandlers.ReadFileAsync(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\transactions.json"));

            result.Should().BeEquivalentTo("Read 5 transaction(s)");
        }

        [Fact]
        public void Throws_Exception_When_No_File()
        {
            var consoleOutputHandlers = new ConsoleOutputHandlers(new TransactionsManager(new FileSystemMock()));

            var act = async () => await consoleOutputHandlers.ReadFileAsync(new FileInfo("no.json"));

            var exception = Assert.ThrowsAsync<ArgumentException>(act);

            Assert.Equal("file does not exists", exception.Result.Message);
        }

        [Fact]
        public async Task Successfully_Loads_Mint_Burn_And_Transfer_From_Valid_JsonArray1()
        {
            var fileSystemMock = new FileSystemMock();

            var consoleOutputHandlers = new ConsoleOutputHandlers(new TransactionsManager(new FileSystemMock()));
            string result = await consoleOutputHandlers.ReadJsonAsync(MintBurnAndTransferTransactions);

            result.Should().BeEquivalentTo("Read 5 transaction(s)");

            result = await consoleOutputHandlers.ShowOwnerAsync("0xA000000000000000000000000000000000000000");
            result.Should().BeEquivalentTo("Token 0xA000000000000000000000000000000000000000 is not owned by any wallet");

            result = await consoleOutputHandlers.ShowOwnerAsync("0xB000000000000000000000000000000000000000");
            result.Should().BeEquivalentTo("Token 0xB000000000000000000000000000000000000000 is owned by 0x3000000000000000000000000000000000000000");

            result = await consoleOutputHandlers.ShowOwnerAsync("0xC000000000000000000000000000000000000000");
            result.Should().BeEquivalentTo("Token 0xC000000000000000000000000000000000000000 is owned by 0x3000000000000000000000000000000000000000");

            result = await consoleOutputHandlers.ShowOwnerAsync("0xD000000000000000000000000000000000000000");
            result.Should().BeEquivalentTo("Token 0xD000000000000000000000000000000000000000 is not owned by any wallet");

            result = await consoleOutputHandlers.ReadJsonAsync(MintDTransaction);
            result.Should().BeEquivalentTo("Read 1 transaction(s)");

            result = await consoleOutputHandlers.ShowOwnerAsync("0xD000000000000000000000000000000000000000");
            result.Should().BeEquivalentTo("Token 0xD000000000000000000000000000000000000000 is owned by 0x1000000000000000000000000000000000000000");

            result = await consoleOutputHandlers.ReportTokensAsync("0x3000000000000000000000000000000000000000");
            result.Should().StartWith("Wallet 0x3000000000000000000000000000000000000000 holds 2 Tokens:");
            result.Should().Contain("0xB000000000000000000000000000000000000000");
            result.Should().Contain("0xC000000000000000000000000000000000000000");

            result = await consoleOutputHandlers.ResetAsync();
            result.Should().BeEquivalentTo("Program was reset");

            result = await consoleOutputHandlers.ReportTokensAsync("0x3000000000000000000000000000000000000000");
            result.Should().StartWith("Wallet 0x3000000000000000000000000000000000000000 holds no Tokens");
        }
    }
}