using TypoDukk.Minecraft.ServerQuery.WebAPI.Models;

namespace TypoDukk.Minecraft.ServerQuery.WebAPI.Services
{
    public interface IServerQueryService
    {
        QueryResponse? QueryServer(string host, ushort port = 25565);

        Task<QueryResponse?> QueryServerAsync(string host, ushort port = 25565);
    }

    public abstract class ServerQueryService : IServerQueryService
    {
        public abstract QueryResponse? QueryServer(string host, ushort port = 25565);

        public virtual Task<QueryResponse?> QueryServerAsync(string host, ushort port = 25565) => new(() => this.QueryServer(host, port));
    }
}
