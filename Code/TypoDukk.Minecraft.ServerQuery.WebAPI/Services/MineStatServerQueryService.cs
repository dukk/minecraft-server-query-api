using MineStatLib;
using TypoDukk.Minecraft.ServerQuery.WebAPI.Models;

namespace TypoDukk.Minecraft.ServerQuery.WebAPI.Services
{
    public class MineStatServerQueryService : ServerQueryService
    {
        private readonly ILogger<MineStatServerQueryService> logger;
        
        public MineStatServerQueryService(ILogger<MineStatServerQueryService> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override QueryResponse? QueryServer(string host, ushort port = 25565)
        {
            this.logger.LogInformation("Query server '{host}', on port '{port}", host, port);

            // dukk: MineStat does the network query in the constructor. Feels wrong so I'm trying to compensate even though they probably handle it...
            MineStat? mineStat;

            try
            {
                // dukk: Ignoring the protocol for now, might come back to this
                mineStat = new MineStat(host, port);
            }
            catch
            {
                mineStat = null;
            }

            if (mineStat is null)
                return null;
            
            return new QueryResponse
            {
                Online = mineStat.ServerUp,
                Favicon = mineStat.Favicon,
                MessageOfTheDay = new MessageOfTheDay()
                {
                    Basic = mineStat.Stripped_Motd,
                    Formatted = mineStat.Motd
                },
                NumberOfPlayers = mineStat.CurrentPlayersInt,
                MaxNumberOfPlayers = mineStat.MaximumPlayersInt,
                Players = mineStat.PlayerList ?? Array.Empty<string>()
            };
        }
    }
}
