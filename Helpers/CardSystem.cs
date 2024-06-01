namespace csharp_discord_bot.Helpers
{
    /// <summary>
    /// Card System for Card Game
    /// </summary>
    public class CardSystem
    {
        private readonly int[] CardNumbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];
        private readonly string[] CardSuits = ["Clubs", "Spades", "Diamonds", "Hearts"];

        public int SelectedNumber { get; set; }
        public string SelectedCard { get; set; } = string.Empty;

        public CardSystem()
        {
            var random = new Random();
            var numberIndex = random.Next(0, CardNumbers.Length - 1);
            var suitIndex = random.Next(0, CardSuits.Length - 1);

            SelectedNumber = CardNumbers[numberIndex];
            SelectedCard = $"{CardNumbers[numberIndex]} of {CardSuits[suitIndex]}";
        }
    }
}