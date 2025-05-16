using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementService.Core.ordersDTO
{
    public  class OrderItemDTO
    {
         
            public string ProductName { get; set; } = null!;
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public bool CashbackEligible { get; set; }
            public decimal CashbackAmount { get; set; }
        
    }
}
