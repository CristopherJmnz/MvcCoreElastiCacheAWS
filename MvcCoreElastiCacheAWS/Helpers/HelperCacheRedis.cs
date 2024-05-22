using StackExchange.Redis;

namespace MvcCoreElastiCacheAWS.Helpers
{
    public class HelperCacheRedis
    {
        private static Lazy<ConnectionMultiplexer>
            createConnection=new Lazy<ConnectionMultiplexer>(() =>
            {
                //aqui va la connecton string
                string connectionString = 
                "cache-coches.1luavn.ng.0001.use1.cache.amazonaws.com:6379";
                return ConnectionMultiplexer.Connect(connectionString);
            });
        
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return createConnection.Value;
            }
        }
    }
}
