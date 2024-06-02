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
                await ctx.DeferAsync();

                var embedMessage = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Blue,
                    Title = "Test Embed"
                };

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embedMessage));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test Slash Error : {ex.Message}");
            }
        }

        [SlashCommand("parameters", "This slash command allows parameters")]
        public async Task ParameterSlashCommand(InteractionContext ctx, [Option("testopt", "Type in anything")] string payload, [Option("numopt", "Type in a number")] long number)
        {
            await ctx.DeferAsync();

            var embedMessage = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Brown,
                Title = "Test Embed",
                Description = $"{payload} {number}"
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embedMessage));
        }

        [SlashCommand("discordParameters", "This slash contains allow passing of DiscordParameters")]
        public async Task DiscordParameters(InteractionContext ctx, [Option("user", "Pass in a Discord User ")] DiscordUser user, [Option("file", "Upload a file here")] DiscordAttachment file)
        {
            await ctx.DeferAsync();

            var embedMessage = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Blue,
                Title = "Test Embed",
                Description = $"{user.Username} {file.FileName} {file.FileSize}"
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embedMessage));
        }

    }
}