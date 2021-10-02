using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InkyBotSharp.Services;
using Discord.Commands;

namespace InkyBotSharp
{
    public class Program
    {
        private IConfigurationRoot _config;
        private DiscordSocketClient _client;

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public Program()
        {
            var projectPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

            // create the configuration
            var _builder = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile(path: "config.json");

            // build the configuration and assign to _config          
            _config = _builder.Build();
        }


        public async Task MainAsync()
        {
            var services = new ServiceCollection();             // Create a new instance of a service collection
            ConfigureServices(services);

            var provider = services.BuildServiceProvider();     // Build the service provider
            provider.GetRequiredService<LoggingService>();      // Start the logging service
            provider.GetRequiredService<CommandHandler>(); 		// Start the command handler service
            provider.GetRequiredService<StoryManager>(); 		// Start the story manager
            provider.GetRequiredService<EmojiManager>();        // Start the emoji manager

            await provider.GetRequiredService<StartupService>().StartAsync();       // Start the startup service

            await Task.Delay(-1);
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            {                                       // Add discord to the collection
                LogLevel = LogSeverity.Verbose,     // Tell the logger to give Verbose amount of info
                MessageCacheSize = 1000             // Cache 1,000 messages per channel
            }))
           .AddSingleton(new CommandService(new CommandServiceConfig
           {                                       // Add the command service to the collection
                LogLevel = LogSeverity.Verbose,     // Tell the logger to give Verbose amount of info
                DefaultRunMode = RunMode.Async,     // Force all commands to run async by default
            }))
           .AddSingleton<CommandHandler>()         // Add the command handler to the collection
           .AddSingleton<StartupService>()         // Add startupservice to the collection
           .AddSingleton<LoggingService>()         // Add loggingservice to the collection
           .AddSingleton<StoryManager>()           //Add StoryManager to the collection
           .AddSingleton<EmojiManager>()          //Add EmojiManager to the collection
           .AddSingleton(_config);           // Add the configuration to the collection
        }

        
    }
}
