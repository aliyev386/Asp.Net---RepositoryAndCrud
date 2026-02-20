using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.DTOS.Order
{
    public class UpdateOrderDto
    {
        public string? OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string? OrderNote { get; set; }
        public decimal? Total { get; set; }

        public int CostumerId { get; set; }
    }
}
