using csharp_discord_bot.Commands;
using csharp_discord_bot.Commands.Components;
using csharp_discord_bot.Commands.Slash;
using csharp_discord_bot.Config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;

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
                AutoReconnect = true,
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
            Client.GuildMemberAdded += GuildMemberHandler;
            Client.ComponentInteractionCreated += ComponentInteractionCreated;

            // Comamnds Config
            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = [jsonReader.Prefix],
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            var slashCommandsConfiguration = Client.UseSlashCommands();

            // register slash commands
            slashCommandsConfiguration.RegisterCommands<BasicSL>();
            slashCommandsConfiguration.RegisterCommands<CalculatorSL>();

            // Commands Events
            Commands.CommandErrored += CommanderErroredHandler;

            // register commands
            Commands.RegisterCommands<TestCommands>();
            Commands.RegisterCommands<CardGameEmbed>();
            Commands.RegisterCommands<PollCommand>();
            Commands.RegisterCommands<InteractionComponents>();
            Commands.RegisterCommands<DiscordComponentCommands>();

            await Client.ConnectAsync();
            await Task.Delay(-1); // to keep bot running forever, as long as the program is running
        }

        /// <summary>
        /// Guidd Member Handler
        /// </summary>
        private static async Task GuildMemberHandler(DiscordClient sender, GuildMemberAddEventArgs args)
        {
            var defaultChannel = args.Guild.GetDefaultChannel();

            var welcomeEmbed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Gold,
                Title = $"Welcome {args.Member.Username} to the server",
                Description = "Hope you enjoy your stay, please read the rules"
            };
            await defaultChannel.SendMessageAsync(embed: welcomeEmbed);
        }

        /// <summary>
        /// Component Interactons Created Event
        /// </summary>
        private static async Task ComponentInteractionCreated(DiscordClient sender, ComponentInteractionCreateEventArgs args)
        {
            // --------- Dropdown List
            if (args.Id == "dropDownList" && args.Interaction.Data.ComponentType == ComponentType.StringSelect)
            {
                var options = args.Values;
                foreach (var option in options)
                {
                    switch (option)
                    {
                        case "option1":
                            await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has Selected Option 1"));
                            break;
                        case "option2":
                            await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has Selected Option 2"));
                            break;
                        case "option3":
                            await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has Selected Option 3"));
                            break;
                    }
                }
            }
            else if (args.Id == "channelDropdownList")
            {
                var options = args.Values;
                foreach (var channel in options)
                {
                    var selectedChannel = await Client!.GetChannelAsync(ulong.Parse(channel));
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has Selected the channel with name {selectedChannel.Name}"));
                }
            }
            else if (args.Id == "mentionDropdownList")
            {
                var options = args.Values;
                foreach (var channel in options)
                {
                    var selectedUser = await Client!.GetUserAsync(ulong.Parse(channel));
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has mentioned the user with name {selectedUser.Mention}"));
                }
            }

            // --------- Button Events
            switch (args.Interaction.Data.CustomId)
            {
                case "button1":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has pressed button 1"));
                    break;
                case "button2":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has pressed button 2"));
                    break;
                case "basicsButton":
                    await args.Interaction.DeferAsync();
                    var basicCommandsEmbed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Black,
                        Title = "Basic Commands",
                        Description = "!test => Send a basic message \n" +
                            "!embed => Send a basic embed message \n" +
                            "!calculator => Perform an operation on 2 numbers  \n" +
                            "!cardgame => Play a simple card game"
                    };
                    await args.Interaction.EditOriginalResponseAsync(new DiscordWebhookBuilder().AddEmbed(basicCommandsEmbed));
                    break;
                case "calculatorButton":
                    await args.Interaction.DeferAsync();
                    var calculatorCommandsEmbed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Black,
                        Title = "Basic Commands",
                        Description = "/calculator add -> Perform 2 numbers operations"
                    };
                    await args.Interaction.EditOriginalResponseAsync(new DiscordWebhookBuilder().AddEmbed(calculatorCommandsEmbed));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Commander Error Handler
        /// </summary>
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

        /// <summary>
        /// Voice Channel Handler
        /// </summary>
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