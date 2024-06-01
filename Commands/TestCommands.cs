using csharp_discord_bot.Helpers;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;

namespace csharp_discord_bot.Commands
{
    /// <summary>
    /// Discord bot Commands
    /// </summary>
    public class TestCommands : BaseCommandModule
    {
        [Command("test")]
        public async Task MyFirstCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"Hello, {ctx.User.Username}");
        }

        [Command("add")]
        public async Task Add(CommandContext ctx, int number1, int number2)
        {
            Console.WriteLine($"Fist Number : {number1} and Second Number : {number2}");
            int result = number1 + number2;
            await ctx.Channel.SendMessageAsync($"Result is = {result}");
        }

        [Command("subtract")]
        public async Task Subtract(CommandContext ctx, int number1, int number2)
        {
            Console.WriteLine($"Fist Number : {number1} and Second Number : {number2}");
            int result = number1 - number2;
            await ctx.Channel.SendMessageAsync($"Result is = {result}");
        }

        [Command("embed")]
        public async Task EmbedMessage(CommandContext ctx)
        {
            var message = new DiscordEmbedBuilder
            {
                Title = "This is my First Discord Embed",
                Description = $"This command was executed by {ctx.User.Username}",
                Color = DiscordColor.Blue
            };

            await ctx.Channel.SendMessageAsync(message);
        }

        [Command("interactivity-test")]
        public async Task InterActivityTest(CommandContext ctx)
        {
            Console.WriteLine("Interactivity Test...");
            var interactivity = DiscordSetup.Client?.GetInteractivity();
            if (interactivity is not null)
            {
                var messageToRetriever = await interactivity.WaitForMessageAsync(message => message.Content == "Hello");
                if (messageToRetriever.Result.Content == "Hello")
                {
                    await ctx.Channel.SendMessageAsync($"{ctx.User.Username} said Hello.");
                }
            }
        }

        [Command("reaction")]
        public async Task ReactionTest(CommandContext ctx)
        {
            Console.WriteLine("Reaction Testing...");
            string jumpUrl = "https://discord.com/channels/1220625821643837541/1220625821643837544/1246442292152369182";

            var interactivity = DiscordSetup.Client?.GetInteractivity();
            if (interactivity is not null)
            {
                var messageToReact = await interactivity.WaitForReactionAsync(message => message.Message.JumpLink == new Uri(jumpUrl));
                if (messageToReact.Result.Message.JumpLink == new Uri(jumpUrl))
                {
                    await ctx.Channel.SendMessageAsync($"{ctx.User.Username} used the emoji with the name {messageToReact.Result.Emoji.Name}");
                }
            }
        }
    }
}