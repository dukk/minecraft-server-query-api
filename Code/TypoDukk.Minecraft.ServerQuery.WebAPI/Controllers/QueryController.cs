using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MineStatLib;
using TypoDukk.Minecraft.ServerQuery.WebAPI.Models;
using TypoDukk.Minecraft.ServerQuery.WebAPI.Services;

namespace TypoDukk.Minecraft.ServerQuery.WebAPI.Controllers
{
    [ApiController]
    [Route("query")]
    public class QueryController : ControllerBase
    {
        private readonly ILogger<QueryController> logger;
        private readonly IServerQueryService serverQueryService;

        public QueryController(ILogger<QueryController> logger, IServerQueryService serverQueryService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.serverQueryService = serverQueryService ?? throw new ArgumentNullException(nameof(serverQueryService));
        }

        [HttpGet(Name = "QueryServer")]
        public async Task<QueryResponse?> Get([FromQuery] string host, [FromQuery] ushort port = 25565)
        {
            this.logger.LogDebug("Query server '{host}', on port '{port}", host, port);

            return await this.serverQueryService.QueryServerAsync(host, port);
        }
    }
}