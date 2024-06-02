using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace csharp_discord_bot.Commands.Slash
{
    public class BasicSL : ApplicationCommandModule
    {
        [SlashCommand("slash", "This is my First Slash Command")]
        public async Task MyFirstSlashCommand(InteractionContext ctx)
        {
            try
            {
                await ctx.Interaction.CreateResponseAsync(DSharpPlus.InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Hello world"));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test Slash Error : {ex.Message}");
            }
        }
    }
}