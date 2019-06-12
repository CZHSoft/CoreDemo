using DotNetCore.CAP;
using OrderApi.Dbs;
using OrderApi.Models;
using System;

namespace OrderApi.Repositorys
{
    public class OrderRepository : IOrderRepository
    {
        public readonly OrderContext _context;
        public readonly ICapPublisher _capPublisher;

        public OrderRepository(OrderContext context, ICapPublisher capPublisher)
        {
            this._context = context;
            this._capPublisher = capPublisher;
        }

        public bool CreateOrder(Order order)
        {
            using (var trans = _context.Database.BeginTransaction())
            {
                var orderEntity = new Order()
                {
                    OrderTime = order.OrderTime
                };

                _context.Orders.Add(orderEntity);
                _context.SaveChanges();

                // When using EF, no need to pass transaction
                //var orderMessage = new OrderMessage()
                //{
                //    ID = orderEntity.ID,
                //    OrderTime = orderEntity.OrderTime,
                //};

                _capPublisher.Publish("czhsoft.services.order.create", orderEntity);

                trans.Commit();
            }

            return true;
        }


    }
}
