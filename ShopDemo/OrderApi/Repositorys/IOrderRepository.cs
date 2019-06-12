using OrderApi.Models;
using System.Threading.Tasks;

namespace OrderApi.Repositorys
{
    public interface IOrderRepository
    {
        bool CreateOrder(Order order);
    }
}
