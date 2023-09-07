namespace TypoDukk.Minecraft.ServerQuery.WebAPI.Models
{
    public class QueryResponse
    {
        public QueryResponse()
        {
            this.Timestamp = DateTime.UtcNow;
        }

        public bool Online { get; set; }

        public string? Favicon { get; set; }

        public MessageOfTheDay? MessageOfTheDay { get; set; }

        public int? NumberOfPlayers { get; set; }

        public int? MaxNumberOfPlayers { get; set; }

        public IEnumerable<string>? Players { get; set; }

        public DateTime Timestamp { get; set; }
    }
}