using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderApi.Models
{
    public class Order
    {
        [Key]
        [MaxLength(36)]
        public string OrderId { get; set; }

        public DateTime OrderTime { get; set; }

    }
}
