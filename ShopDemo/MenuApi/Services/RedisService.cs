using System;
using System.Collections.Generic;
using ShopModel;
using StackExchange.Redis;

namespace MenuApi.Services
{
    public class RedisService : IRedisService
    {
        public List<MenuViewModel> GetMenu()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379,password=123456");
            IDatabase db = redis.GetDatabase();
            var data = db.HashGetAll("0757rc");

            return new List<MenuViewModel>();

        }
    }
}
