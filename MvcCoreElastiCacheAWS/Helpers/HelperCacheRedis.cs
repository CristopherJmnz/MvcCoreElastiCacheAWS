using StackExchange.Redis;

namespace MvcCoreElastiCacheAWS.Helpers
{
    public class HelperCacheRedis
    {
        private static Lazy<ConnectionMultiplexer>
            createConnection=new Lazy<ConnectionMultiplexer>(() =>
            {
                //aqui va la connecton string
                return ConnectionMultiplexer.Connect("");
            });
        
        public ConnectionMultiplexer ConnectionMultiplexer
        {
            get
            {
                return createConnection.Value;
            }
        }
    }
}
