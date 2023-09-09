using TypoDukk.Minecraft.ServerQuery.WebAPI.Controllers;

namespace TypoDukk.Minecraft.ServerQuery.WebAPI.Views;

public class QueryNetworkResponseViewOptions
{
    public QueryNetworkResponseViewOptions()
    {
        this.ServerOptions = new Dictionary<string, QueryNetworkServerViewOptions>();
    }
    public string? UseServersNumberOfPlayers { get; set; }

    public string? UseServersMaxNumberOfPlayers { get; set; }

    public string? UseServersPlayers { get; set; }

    public string? UseServersFavicon { get; set; }

    public string? UseServersMessageOfTheDay { get; set; }

    public bool HideNumberOfPlayers { get; set; }

    public bool HideMaxNumberOfPlayers { get; set; }

    public bool HidePlayers { get; set; }

    public IDictionary<string, QueryNetworkServerViewOptions> ServerOptions { get; set; }

    public static QueryNetworkResponseViewOptions FromConfiguration(NetworkConfiguration configuration)
    {
        var options = new QueryNetworkResponseViewOptions
        {
            HideNumberOfPlayers = configuration.HideNumberOfPlayers ?? false,
            HideMaxNumberOfPlayers = configuration.HideMaxNumberOfPlayers ?? false,
            HidePlayers = configuration.HidePlayers ?? false,
            UseServersFavicon = configuration.UseFaviconFromServer,
            UseServersMessageOfTheDay = configuration.UseMessageOfTheDayFromServer,
            UseServersNumberOfPlayers = configuration.UseNumberOfPlayersFromServer,
            UseServersMaxNumberOfPlayers = configuration.UseMaxNumberOfPlayersFromServer,
            UseServersPlayers = configuration.UsePlayersFromServer,
            ServerOptions = new Dictionary<string, QueryNetworkServerViewOptions>(
                from server in configuration.Servers
                select new KeyValuePair<string, QueryNetworkServerViewOptions>(server.Name,
                    new QueryNetworkServerViewOptions()
                    {
                        HideServer = server.HideServer ?? false,
                        HideFavicon = server.HideFavicon ?? false,
                        HideMessageOfTheDay = server.HideMessageOfTheDay ?? false,
                        HideNumberOfPlayers = server.HideNumberOfPlayers ?? false,
                        HideMaxNumberOfPlayers = server.HideMaxNumberOfPlayers ?? false,
                        HidePlayers = server.HidePlayers ?? false
                    }))
        };

        return options;
    }
}
