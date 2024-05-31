using csharp_discord_bot.config;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace CSharp_Discord_Bot
{
    public class Program
    {
        private static DiscordClient? Client { get; set; }
        private static CommandsNextExtension? Commands { get; set; }

        public static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        private static async Task MainAsync()
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
            await Client.ConnectAsync();
            await Task.Delay(-1); // to keep bot running forever, as long as the program is running
        }
    }
}