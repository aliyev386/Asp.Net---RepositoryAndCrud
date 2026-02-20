using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.DTOS.Order;

public class AllOrderDto
{
    public string?  OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string?  OrderNote { get; set; }
    public decimal? Total { get; set; }
    public string?  CostumerName { get; set; }

}
