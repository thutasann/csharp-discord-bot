using csharp_discord_bot.Helpers;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace csharp_discord_bot.Commands
{
    public class CardGameEmbed : BaseCommandModule
    {
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