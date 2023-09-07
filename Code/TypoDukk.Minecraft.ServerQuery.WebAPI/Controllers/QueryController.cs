using Microsoft.AspNetCore.Mvc;

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
        public MinecraftStatus.Status Get([FromQuery] string host, [FromQuery] int port = 25565)
        {
            this.logger.LogInformation("Query server '{0}', on port '{1}", host, port);

            return MinecraftStatus.Status.GetStatus(host, port);
        }
    }
}