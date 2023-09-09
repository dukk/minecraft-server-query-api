namespace TypoDukk.Minecraft.ServerQuery.WebAPI.Views;

public class QueryNetworkServerViewOptions
{
    public QueryNetworkServerViewOptions()
    {

    }

    public bool HideServer { get; set; }

    public bool HideNumberOfPlayers { get; set; }

    public bool HideMaxNumberOfPlayers { get; set; }

    public bool HidePlayers { get; set; }

    public bool HideFavicon { get; set; }

    public bool HideMessageOfTheDay { get; set; }
}