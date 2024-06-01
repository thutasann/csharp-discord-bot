using csharp_discord_bot.config;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace csharp_discord_bot.Helpers
{
    public static class DiscordSetup
    {
        private static DiscordClient? Client { get; set; }
        private static CommandsNextExtension? Commands { get; set; }


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

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = [jsonReader.Prefix],
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            await Client.ConnectAsync();
            await Task.Delay(-1); // to keep bot running forever, as long as the program is running
        }
    }
}