using Newtonsoft.Json;

namespace csharp_discord_bot.Config
{
    public class JSONReader
    {
        public string Token { get; set; } = string.Empty;
        public string Prefix { get; set; } = string.Empty;

        public async Task ReadJSONAsync()
        {
            try
            {
                using StreamReader str = new("config.json");
                string json = await str.ReadToEndAsync();
                JSONStructure? data = JsonConvert.DeserializeObject<JSONStructure>(json);
                if (data is not null)
                {
                    Token = data.Token;
                    Prefix = data.Prefix;
                }
                else
                {
                    Console.WriteLine("No Data found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in reading json file : {ex.Message}");
            }
        }
    }

    internal sealed class JSONStructure
    {
        public string Token { get; set; } = string.Empty;
        public string Prefix { get; set; } = string.Empty;
    }
}