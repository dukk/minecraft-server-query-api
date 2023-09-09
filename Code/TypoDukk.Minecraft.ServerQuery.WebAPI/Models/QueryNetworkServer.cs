namespace TypoDukk.Minecraft.ServerQuery.WebAPI.Models
{
    public class QueryNetworkServer
    {
        public QueryNetworkServer(string name)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Players = new List<string>();
        }

        public string Name { get; set; }

        public bool Online { get; set; }

        public string? Favicon { get; set; }

        public MessageOfTheDay? MessageOfTheDay { get; set; }

        public int NumberOfPlayers { get; set; }

        public int MaxNumberOfPlayers { get; set; }

        public IList<string> Players { get; set; }

        public static QueryNetworkServer FromQueryResponse(string name, QueryResponse queryResponse)
        {
            // dukk: This needs to be mapped to a new type because the network query can hide or overwrite the values.
            var server = new QueryNetworkServer(name)
            {
                Online = queryResponse.Online,
                MessageOfTheDay = queryResponse.MessageOfTheDay,
                Favicon = queryResponse.Favicon,
                NumberOfPlayers = queryResponse.NumberOfPlayers,
                MaxNumberOfPlayers = queryResponse.MaxNumberOfPlayers,
                Players = queryResponse.Players
            };

            return server;
        }
    }
}