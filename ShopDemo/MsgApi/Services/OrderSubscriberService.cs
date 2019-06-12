using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MsgApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MsgApi.Services
{
    public class OrderSubscriberService : IOrderSubscriberService, ICapSubscribe
    {
        public OrderSubscriberService()
        {
        }

        [CapSubscribe("czhsoft.services.order.create")]
        public void ConsumeOrderMessage(Order message)
        {
            Console.Out.WriteLine($"[MsgApi] Received message : order id {message.OrderId}");
        }

    }
}
