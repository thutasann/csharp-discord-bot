using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace csharp_discord_bot.Commands.Slash
{
    public class ModalSL : ApplicationCommandModule
    {
        [SlashCommand("modal", "show a modal")]
        public async Task Modal(InteractionContext ctx)
        {
            var modal = new DiscordInteractionResponseBuilder()
                .WithTitle("Test Modal")
                .WithCustomId("modal")
                .AddComponents(new TextInputComponent("Random", "randomTextBox", "Type something here..."));

            await ctx.CreateResponseAsync(InteractionResponseType.Modal, modal);
        }

    }
}