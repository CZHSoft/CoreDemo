using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderApi.Repositorys;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : Controller
    {
        public readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "order", "order2" };
        }

        [HttpGet("create")]
        public string CreateOrder()
        {
            bool res = _orderRepository.CreateOrder(new Models.Order() { OrderId = Guid.NewGuid().ToString(), OrderTime = DateTime.Now });

            return "ok";
        }
    }
}
