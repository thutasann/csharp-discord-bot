using csharp_discord_bot.Commands;
using csharp_discord_bot.Config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
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

            // Discord Config
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

            // Discord Client Evnets
            // Client.MessageCreated += MessageCreatedHandler;
            Client.VoiceStateUpdated += VoiceChannelHandler;

            // Comamnds Config
            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = [jsonReader.Prefix],
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            // Commands Events
            Commands.CommandErrored += CommanderErroredHandler;

            // register commands
            Commands.RegisterCommands<TestCommands>();
            Commands.RegisterCommands<CardGameEmbed>();
            Commands.RegisterCommands<PollCommand>();

            await Client.ConnectAsync();
            await Task.Delay(-1); // to keep bot running forever, as long as the program is running
        }

        private static async Task CommanderErroredHandler(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            if (e.Exception is ChecksFailedException exception)
            {
                string timeLeft = string.Empty;

                foreach (var check in exception.FailedChecks)
                {
                    // cooldown command
                    var cooldown = (CooldownAttribute)check;
                    timeLeft = cooldown.GetRemainingCooldown(e.Context).ToString(@"hh\:mm\:ss");
                }

                var coolDownMessage = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Red,
                    Title = "Please wait for the cooldown to end",
                    Description = $"Time Left : {timeLeft}"
                };

                await e.Context.Channel.SendMessageAsync(embed: coolDownMessage);
            }
        }

        private static async Task MessageCreatedHandler(DiscordClient sender, MessageCreateEventArgs e)
        {
            await e.Channel.SendMessageAsync("This event handler was triggered");
        }

        private static async Task VoiceChannelHandler(DiscordClient sender, VoiceStateUpdateEventArgs e)
        {
            Console.WriteLine("Voice Channel handler...");
            if (e.Before == null && e.Channel.Name == "General")
            {
                await e.Channel.SendMessageAsync($"{e.User.Username} joined the Voice Channel");
            }
        }

    }
}