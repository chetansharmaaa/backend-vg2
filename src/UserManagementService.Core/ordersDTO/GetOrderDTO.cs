using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.Models;

namespace UserManagementService.Core.ordersDTO
{
    public class GetOrdersDTO
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = null!;

        public string? PaymentMethod { get; set; }

        public string? TransactionId { get; set; }

        //public virtual ICollection<OrderCashback> OrderCashbacks { get; set; } = new List<OrderCashback>();

        //public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        //public virtual User User { get; set; } = null!;
    }
    public static class OrderExtension
    {
        public static GetOrdersDTO   ToDTO(this Order order)
        {
            return new GetOrdersDTO
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                PaymentMethod = order.PaymentMethod,
                TransactionId = order.TransactionId,
                //OrderCashbacks = order.OrderCashbacks,
               // OrderItems = order.OrderItems,
              //  User = order.User
            };
        }
    }
}
