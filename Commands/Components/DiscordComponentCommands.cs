using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace csharp_discord_bot.Commands.Components
{
    /// <summary>
    /// Discord Components [Dropdowns, Modal]
    /// </summary>
    public class DiscordComponentCommands : BaseCommandModule
    {
        [Command("dropdown-list")]
        public async Task DropDownList(CommandContext ctx)
        {
            List<DiscordSelectComponentOption> optionsList = [
                new DiscordSelectComponentOption("Option 1", "option1"),
                new DiscordSelectComponentOption("Option 2", "option2"),
                new DiscordSelectComponentOption("Option 3", "option3")
            ];

            var options = optionsList.AsEnumerable();

            var dropdown = new DiscordSelectComponent("dropDownList", "Select....", options);

            var dropdownMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                         .WithColor(DiscordColor.Gold)
                         .WithTitle("This embed has a dropdown list on it")
                ).AddComponents(dropdown);

            await ctx.Channel.SendMessageAsync(dropdownMessage);
        }

        [Command("channel-list")]
        public async Task ChannelList(CommandContext ctx)
        {
            var channelComponent = new DiscordChannelSelectComponent("channelDropdownList", "Select Channel...");

            var dropDownMessage = new DiscordMessageBuilder()
            .AddEmbed(new DiscordEmbedBuilder()
            .WithColor(DiscordColor.Gold)
            .WithTitle("This embed has a channel dropdown list on it"))
            .AddComponents(channelComponent);

            await ctx.Channel.SendMessageAsync(dropDownMessage);
        }
    }
}