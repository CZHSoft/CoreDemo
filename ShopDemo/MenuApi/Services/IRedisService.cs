using ShopModel;
using System.Collections.Generic;

namespace MenuApi.Services
{
    public interface IRedisService
    {
        List<MenuViewModel> GetMenu();
    }
}
