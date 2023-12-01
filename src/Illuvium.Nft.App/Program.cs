using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.NamingConventionBinder;
using Illuvium.Nft.App;
using Illuvium.Nft.App.Cli;
using Illuvium.Nft.App.Commands;
using Illuvium.Nft.Common;
using Microsoft.Extensions.DependencyInjection;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using Microsoft.Extensions.Hosting;
using Illuvium.Nft.App.Repository;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var root = new RootCommand();
        root.Description = "Illuvium app to work with NFT tokens.";

        root.AddCommand(new ReadFileCommand());
        root.AddCommand(new ReadInlineCommand());
        root.AddCommand(new WalletCommand());
        root.AddCommand(new ResetCommand());
        root.AddCommand(new NftCommand());

        root.Handler = CommandHandler.Create(() => root.Invoke(args));

        return await new CommandLineBuilder(root)
           .UseHost(_ => Host.CreateDefaultBuilder(args), builder => builder
                .ConfigureServices(RegisterServices)
                .UseCommandHandler<ReadFileCommand, ReadFileCommandHandler>()
                .UseCommandHandler<ReadInlineCommand, ReadInlineCommandHandler>()
                .UseCommandHandler<WalletCommand, WalletCommandHandler>()
                .UseCommandHandler<ResetCommand, ResetCommandHandler>()
                .UseCommandHandler<NftCommand, NftCommandHandler>())
           .UseDefaults()
           //.UseCustomErrorHandler(ExceptionHook)
           .Build()
           .InvokeAsync(args);
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddSingleton<IFileSystem, XmlFileSystem>();
        services.AddSingleton<ITransactionsManager, TransactionsManager>();
        services.AddSingleton<IConsoleOutputHandlers, ConsoleOutputHandlers>();
    }
}