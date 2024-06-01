using csharp_discord_bot.Commands;
using csharp_discord_bot.Config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;

namespace csharp_discord_bot.Helpers
{
    /// <summary>
    /// Discord Bot Setup
    /// </summary>
    public static class DiscordSetup
    {
        public static DiscordClient? Client { get; set; }
        private static CommandsNextExtension? Commands { get; set; }

        /// <summary>
        /// Main Method for Discord Bot!
        /// </summary>
        /// <returns></returns>

        public static async Task MainAsync()
        {
            var jsonReader = new JSONReader();
            await jsonReader.ReadJSONAsync();

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(discordConfig);

            // Set the default timeout for Commands that ues interactivity
            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(1)
            });

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = [jsonReader.Prefix],
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            // register commands
            Commands.RegisterCommands<TestCommands>();
            Commands.RegisterCommands<CardGameEmbed>();

            await Client.ConnectAsync();
            await Task.Delay(-1); // to keep bot running forever, as long as the program is running
        }
    }
}