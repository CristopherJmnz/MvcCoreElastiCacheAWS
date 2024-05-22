﻿using MvcCoreElastiCacheAWS.Helpers;
using MvcCoreElastiCacheAWS.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MvcCoreElastiCacheAWS.Services
{
    public class AWSCacheService
    {
        private IDatabase cache;
        public AWSCacheService()
        {
            this.cache = HelperCacheRedis.Connection.GetDatabase();
        }

        public async Task<List<Coche>> GetCochesFavoritosAsync()
        {
            string jsonCoches = await this.cache.StringGetAsync("cochesfavoritos");
            if (jsonCoches != null)
            {
                List<Coche> cars = JsonConvert.DeserializeObject<List<Coche>>(jsonCoches);
                return cars;
            }
            return null;
        }

        public async Task AddCocheFavoritoAsync(Coche car)
        {
            List<Coche> cars = await this.GetCochesFavoritosAsync();
            if (cars != null)
            {
                cars = new List<Coche>();
            }
            cars.Add(car);
            string jsonCoches = JsonConvert.SerializeObject(cars);
            await this.cache.StringSetAsync
                ("cochesfavoritos", jsonCoches, TimeSpan.FromMinutes(30));
        }

        public async Task DeleteCocheFavoritoAsync(int idcoche)
        {
            List<Coche> cars = await this.GetCochesFavoritosAsync();
            if (cars != null)
            {
                Coche carDelete = cars.FirstOrDefault(x => x.IdCoche == idcoche);
                cars.Remove(carDelete);
            }
            if (cars.Count == 0)
            {
                await this.cache.KeyDeleteAsync("cochesfavoritos");
            }
            else
            {
                string jsonCoches = JsonConvert.SerializeObject(cars);
                await this.cache.StringSetAsync
                    ("cochesfavoritos", jsonCoches, TimeSpan.FromMinutes(30));
            }
        }
    }
}
