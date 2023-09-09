using System.Runtime.Serialization;
using TypoDukk.Minecraft.ServerQuery.WebAPI.Models;

namespace TypoDukk.Minecraft.ServerQuery.WebAPI.Views;

public class QueryNetworkServerView
{
    private readonly QueryNetworkServer server;
    private readonly QueryNetworkServerViewOptions options;

    public QueryNetworkServerView(QueryNetworkServer server, QueryNetworkServerViewOptions options)
    {
        this.server = server ?? throw new ArgumentNullException(nameof(server));
        this.options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public string Name { get { return this.server.Name; } }

    public bool Online { get { return this.server.Online; } }

    public string? Favicon { get { return this.options.HideFavicon ? null : this.server.Favicon; } }

    public MessageOfTheDay? MessageOfTheDay { get { return this.options.HideMessageOfTheDay ? null : this.server.MessageOfTheDay; } }

    public int? NumberOfPlayers { get { return this.options.HideNumberOfPlayers ? null : this.server.NumberOfPlayers; } }

    public int? MaxNumberOfPlayers { get { return this.options.HideMaxNumberOfPlayers ? null : this.server.MaxNumberOfPlayers; } }

    public IEnumerable<string>? Players { get { return this.options.HidePlayers ? null : this.server.Players; } }

    internal QueryNetworkServer Server { get { return this.server; } }

    internal QueryNetworkServerViewOptions Options { get { return this.options; } }
}