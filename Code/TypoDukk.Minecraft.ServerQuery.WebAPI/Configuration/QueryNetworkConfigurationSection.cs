using System;
using Microsoft.Extensions.Configuration;

namespace TypoDukk.Minecraft.ServerQuery.WebAPI.Controllers
{
    public sealed class QueryNetworkConfigurationSection
    {
        public NetworkConfiguration[]? Networks { get; set; }
    }

    public sealed class NetworkConfiguration 
    {
        public string? Name { get; set; }

        public ServerConfiguration[]? Servers { get; set; }

        public string? UseNumberOfPlayersFromServer { get; set; }

        public string? UseMaxNumberOfPlayersFromServer { get; set; }

        public string? UsePlayersFromServer { get; set; }

        public string? UseMessageOfTheDayFromServer { get; set; }

        public string? UseFaviconFromServer { get; set; }

        public bool? HideNumberOfPlayers { get; set; }

        public bool? HideMaxNumberOfPlayers { get; set; }

        public bool? HidePlayers { get; set; }
    }

    public sealed class ServerConfiguration 
    {
        public ServerConfiguration()
        {
            this.Name = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string? Host { get; set; }

        public ushort? Port { get; set; }

        public bool? HideMessageOfTheDay { get; set; }

        public bool? HideFavicon { get; set; }

        public bool? HideNumberOfPlayers { get; set; }

        public bool? HideMaxNumberOfPlayers { get; set; }

        public bool? HidePlayers { get; set; }

        public bool? HideServer { get; set; }

        public bool? RequireOnline { get; set; }
    }
}