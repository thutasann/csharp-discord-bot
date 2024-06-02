using csharp_discord_bot.Helpers;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;

namespace csharp_discord_bot.Commands
{
    /// <summary>
    /// Poll with Reaction Command
    /// </summary>
    public class PollCommand : BaseCommandModule
    {
        [Command("poll")]
        public async Task PollCommandMethod(CommandContext ctx, string option1, string option2, string option3, string option4, [RemainingText] string pollTitle)
        {
            Console.WriteLine($"Poll Options : {option1} {option2} {option3} {option4}");
            try
            {
                var interactivity = DiscordSetup.Client?.GetInteractivity();
                var pollTime = TimeSpan.FromSeconds(10);

                if (interactivity is not null)
                {

                    DiscordEmoji[] emojiOptions = [
                        DiscordEmoji.FromName(DiscordSetup.Client!, ":one:"),
                        DiscordEmoji.FromName(DiscordSetup.Client!, ":two:"),
                        DiscordEmoji.FromName(DiscordSetup.Client!, ":three:"),
                        DiscordEmoji.FromName(DiscordSetup.Client!, ":four:"),
                    ];

                    string optionsDescription = $"{emojiOptions[0]} | {option1} \n" +
                                                $"{emojiOptions[1]} | {option2} \n" +
                                                $"{emojiOptions[2]} | {option3} \n" +
                                                $"{emojiOptions[3]} | {option4}";

                    var pollMessage = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Red,
                        Title = pollTitle,
                        Description = optionsDescription
                    };

                    var sentPoll = await ctx.Channel.SendMessageAsync(embed: pollMessage);
                    foreach (var emoji in emojiOptions)
                    {
                        await sentPoll.CreateReactionAsync(emoji);
                    }

                    var totalReactions = await interactivity.CollectReactionsAsync(sentPoll, pollTime);

                    int count1 = 0;
                    int count2 = 0;
                    int count3 = 0;
                    int count4 = 0;

                    foreach (var emoji in totalReactions)
                    {
                        if (emoji.Emoji == emojiOptions[0])
                        {
                            count1++;
                        }
                        if (emoji.Emoji == emojiOptions[1])
                        {
                            count2++;
                        }
                        if (emoji.Emoji == emojiOptions[2])
                        {
                            count3++;
                        }
                        if (emoji.Emoji == emojiOptions[3])
                        {
                            count4++;
                        }
                    }

                    int totalValues = count1 + count2 + count3 + count4;
                    string resultsDescriptions = $"{emojiOptions[0]} : {count1} Votes \n" +
                                                $"{emojiOptions[1]} : {count2} Votes \n" +
                                                $"{emojiOptions[2]} : {count3} Votes \n" +
                                                $"{emojiOptions[3]} : {count4} Votes \n" +
                                                $"Total Votes: {totalValues}";

                    var resultEmbed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Green,
                        Title = "Results of the Poll",
                        Description = resultsDescriptions
                    };
                    await ctx.Channel.SendMessageAsync(embed: resultEmbed);
                }
                else
                {
                    Console.WriteLine("Interactivity not found ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Poll Command Error : {ex.Message}");
            }
        }
    }
}