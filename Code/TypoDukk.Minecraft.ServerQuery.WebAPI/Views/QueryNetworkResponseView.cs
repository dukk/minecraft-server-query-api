using System.Linq;
using System.Runtime.Serialization;
using TypoDukk.Minecraft.ServerQuery.WebAPI.Models;

namespace TypoDukk.Minecraft.ServerQuery.WebAPI.Views;

public class QueryNetworkResponseView
{
    private readonly QueryNetworkResponse response;
    private readonly QueryNetworkResponseViewOptions options;
    private readonly IEnumerable<QueryNetworkServerView> servers;

    public QueryNetworkResponseView(QueryNetworkResponse response, QueryNetworkResponseViewOptions options)
    {
        this.response = response ?? throw new ArgumentNullException(nameof(response));
        this.options = options ?? throw new ArgumentNullException(nameof(options));
        this.servers = new List<QueryNetworkServerView>(from server in this.response.Servers
                                                        select new QueryNetworkServerView(server,
                                                            options.ServerOptions[server.Name] ?? new QueryNetworkServerViewOptions()));
    }

    public string Status => this.response.Status.ToString().ToLower();

    public string? Favicon => this.options.UseServersFavicon is null
                ? null
                : this.getServer(this.options.UseServersFavicon).Favicon;

    public MessageOfTheDay? MessageOfTheDay => this.options.UseServersMessageOfTheDay is null
                ? null
                : this.getServer(this.options.UseServersMessageOfTheDay).MessageOfTheDay;

    public int? NumberOfPlayers => this.options.HideNumberOfPlayers
                ? null
                : this.options.UseServersNumberOfPlayers is null
                    ? this.response.NumberOfPlayers
                    : this.getServer(this.options.UseServersNumberOfPlayers).NumberOfPlayers;

    public int? MaxNumberOfPlayers => this.options.HideMaxNumberOfPlayers
                ? null
                : this.options.UseServersMaxNumberOfPlayers is null
                    ? this.response.MaxNumberOfPlayers
                    : this.getServer(this.options.UseServersMaxNumberOfPlayers).MaxNumberOfPlayers;

    public IEnumerable<string>? Players => this.options.HidePlayers
                ? null
                : this.options.UseServersPlayers is null
                    ? this.response.Players
                    : this.getServer(this.options.UseServersPlayers).Players;

    public DateTime Timestamp => this.response.Timestamp;

    public IEnumerable<QueryNetworkServerView> Servers
    {
        get
        {
            foreach (var server in servers)
            {
                if (!server.Options.HideServer)
                    yield return server;
            }
        }
    }

    private QueryNetworkServer getServer(string name)
    {
        return this.servers.First(server => server.Name == name).Server;
    }
}