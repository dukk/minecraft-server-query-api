using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MineStatLib;
using TypoDukk.Minecraft.ServerQuery.WebAPI.Configuration;
using TypoDukk.Minecraft.ServerQuery.WebAPI.Models;
using TypoDukk.Minecraft.ServerQuery.WebAPI.Services;
using TypoDukk.Minecraft.ServerQuery.WebAPI.Views;

namespace TypoDukk.Minecraft.ServerQuery.WebAPI.Controllers
{
    [ApiController]
    [Route("query-network")]
    public class QueryNetworkController : ControllerBase
    {
        private readonly ILogger<QueryController> logger;
        private readonly IQueryNetworkService queryNetworkService;
        private readonly QueryNetworkConfigurationSection? queryNetworkConfigurationSection;

        public QueryNetworkController(ILogger<QueryController> logger, QueryNetworkConfigurationSection? queryNetworkConfigurationSection, IQueryNetworkService queryNetworkService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.queryNetworkService = queryNetworkService ?? throw new ArgumentNullException(nameof(queryNetworkService));
            this.queryNetworkConfigurationSection = queryNetworkConfigurationSection ?? throw new ArgumentNullException(nameof(queryNetworkConfigurationSection));
        }

        [HttpGet("{network}", Name = "QueryNetwork")]
        public async Task<QueryNetworkResponseView> Get([FromRoute] string? network)
        { 
            if (network is null)
                throw new ArgumentNullException(nameof(network));

            this.logger.LogInformation("Querying network '{network}", network);

            var networkConfiguration = (this.queryNetworkConfigurationSection?.Networks?.FirstOrDefault(n => n.Name == network))
                ?? throw new ArgumentException($"Unable to find '{network}' network configuration.", nameof(network));

            var response = await this.queryNetworkService.QueryNetworkAsync(network);

            var responseView = new QueryNetworkResponseView(response,
                QueryNetworkResponseViewOptions.FromConfiguration(networkConfiguration));

            return responseView;
        }
    }
}