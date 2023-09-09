namespace TypoDukk.Minecraft.ServerQuery.WebAPI.Models
{
    public class QueryNetworkResponse
    {
        private readonly string[] requiredOnlineServers;

        public QueryNetworkResponse(string[] requiredOnlineServers)
        {
            this.Servers = new List<QueryNetworkServer>();
            this.requiredOnlineServers = requiredOnlineServers;
            this.Timestamp = DateTime.UtcNow;
        }

        public NetworkStatus Status
        {
            get
            {
                var status = NetworkStatus.Online;
                var atleastOne = false;

                foreach (var server in this.Servers)
                {
                    if (!server.Online)
                    {
                        if (requiredOnlineServers.Contains(server.Name))
                        {
                            status = NetworkStatus.Offline;
                            break;
                        }

                        status = NetworkStatus.Partual;
                    }
                    else
                    {
                        atleastOne = true;
                    }
                }

                status = (status == NetworkStatus.Partual && atleastOne)
                    ? NetworkStatus.Partual
                    : NetworkStatus.Offline;

                return status;
            }
        }

        public int NumberOfPlayers => this.Players.Distinct().Count();

        public int? MaxNumberOfPlayers => this.Servers.Max(server => server.MaxNumberOfPlayers);

        public IEnumerable<string> Players => this.getPlayers().Distinct();

        public DateTime Timestamp { get; set; }

        public IList<QueryNetworkServer> Servers { get; set; }

        private IEnumerable<string> getPlayers()
        {
            foreach (var server in this.Servers)
            {
                foreach (var player in server.Players)
                {
                    yield return player;
                }
            }
        }
    }
}