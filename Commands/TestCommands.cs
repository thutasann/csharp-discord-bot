using csharp_discord_bot.Helpers;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

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

        [Command("cardgame")]
        public async Task CardGame(CommandContext ctx)
        {
            var userCard = new CardSystem();
            var botCard = new CardSystem();

            var userCardEmbed = new DiscordEmbedBuilder
            {
                Title = $"Your card is {userCard.SelectedCard}",
                Color = DiscordColor.Lilac,
            };

            await ctx.Channel.SendMessageAsync(embed: userCardEmbed);

            var botCardEmbed = new DiscordEmbedBuilder
            {
                Title = $"The bot Drew a {botCard.SelectedCard}",
                Color = DiscordColor.Orange
            };

            await ctx.Channel.SendMessageAsync(embed: botCardEmbed);

            if (userCard.SelectedNumber > botCard.SelectedNumber)
            {
                // User wins
                var winMessage = new DiscordEmbedBuilder
                {
                    Title = "Congrat!, You Win the game!!",
                    Color = DiscordColor.Green
                };

                await ctx.Channel.SendMessageAsync(embed: winMessage);
            }
            else
            {
                // Bot wins
                var loseMessage = new DiscordEmbedBuilder
                {
                    Title = "You Lose the game!!",
                    Color = DiscordColor.Red
                };

                await ctx.Channel.SendMessageAsync(embed: loseMessage);
            }
        }
    }
}