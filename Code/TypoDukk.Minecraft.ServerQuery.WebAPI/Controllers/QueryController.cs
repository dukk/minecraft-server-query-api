using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MineStatLib;
using TypoDukk.Minecraft.ServerQuery.WebAPI.Models;

namespace TypoDukk.Minecraft.ServerQuery.WebAPI.Controllers
{
    [ApiController]
    [Route("query")]
    public class QueryController : ControllerBase
    {
        private readonly ILogger<QueryController> logger;

        public QueryController(ILogger<QueryController> logger)
        {
            this.logger = logger;
        }

        [HttpGet(Name = "QueryServer")]
        public QueryResponse Get([FromQuery] string host, [FromQuery] ushort port = 25565)
        {
            this.logger.LogInformation("Query server '{0}', on port '{1}", host, port);

            QueryResponse response = new QueryResponse();
            MineStat? mineStat;

            try 
            {
                mineStat = new MineStat(host, port);
            }
            catch 
            {
                mineStat = null;
            }

            if (null != mineStat)
            {
                response.Online = mineStat.ServerUp;
                response.Favicon = mineStat.Favicon;
                response.MessageOfTheDay = new MessageOfTheDay() {
                    Basic = mineStat.Stripped_Motd,
                    Formatted = mineStat.Motd
                };
                response.NumberOfPlayers = mineStat.CurrentPlayersInt;;
                response.MaxNumberOfPlayers = mineStat.MaximumPlayersInt;
                response.Players = mineStat.PlayerList ?? Array.Empty<string>();
            }

            return response;
        }
    }
}