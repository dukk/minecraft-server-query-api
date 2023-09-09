using Microsoft.AspNetCore.HttpOverrides;
using TypoDukk.Minecraft.ServerQuery.WebAPI.Configuration;
using TypoDukk.Minecraft.ServerQuery.WebAPI.Controllers;
using TypoDukk.Minecraft.ServerQuery.WebAPI.Models;

namespace TypoDukk.Minecraft.ServerQuery.WebAPI.Services;

public interface IQueryNetworkService
{
    Task<QueryNetworkResponse> QueryNetworkAsync(string network);
}

public class QueryNetworkService : IQueryNetworkService
{
    private readonly ILogger<QueryNetworkService> logger;
    private readonly QueryNetworkConfigurationSection? queryNetworkConfigurationSection;
    private readonly IServerQueryService serverQueryService;

    public QueryNetworkService(ILogger<QueryNetworkService> logger, QueryNetworkConfigurationSection? queryNetworkConfigurationSection, IServerQueryService serverQueryService)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.queryNetworkConfigurationSection = queryNetworkConfigurationSection ?? throw new ArgumentNullException(nameof(queryNetworkConfigurationSection));
        this.serverQueryService = serverQueryService ?? throw new ArgumentNullException(nameof(serverQueryService));
    }

    public async Task<QueryNetworkResponse> QueryNetworkAsync(string network)
    {
        var networkConfiguration = (this.queryNetworkConfigurationSection?.Networks?.FirstOrDefault(n => n.Name == network))
                ?? throw new ArgumentException($"Unable to find '{network}' network configuration.", nameof(network));
        var response = new QueryNetworkResponse((from server in networkConfiguration.Servers
                                                 where server.RequireOnline ?? false
                                                 select server.Name).ToArray());

        if (networkConfiguration.Servers is null)
            throw new ConfigurationException("'Servers' is required for network configuration.");

        // dukk: Parallel isn't always great in ASP.Net but in my use case it works, not a lot of users + cached results
        await Parallel.ForEachAsync(networkConfiguration.Servers, async (server, cancelToken) =>
        {
            if (server.Host is null)
                throw new ConfigurationException("'Host' is required for server configuration.");

            try
            {
                var serverQueryResponse = await this.serverQueryService.QueryServerAsync(server.Host, server.Port.GetValueOrDefault(25565));

                response.Servers.Add(QueryNetworkServer.FromQueryResponse(server.Name, serverQueryResponse));
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed while querying network '{network}' server '{server}'.", network, server);
            }
        });

        return response;
    }
}
