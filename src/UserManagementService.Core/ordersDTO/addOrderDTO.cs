using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.Models;

namespace UserManagementService.Core.ordersDTO
{
    public class addOrderDTO
    {
        //public int OrderId { get; set; }

        public int UserId { get; set; }

        //public DateTime? OrderDate { get; set; }

        //public decimal TotalAmount { get; set; }

        public string Status { get; set; } = null!;

        public string? PaymentMethod { get; set; }

        public string? TransactionId { get; set; }

        //public virtual ICollection<OrderCashback> OrderCashbacks { get; set; } = new List<OrderCashback>();

        public virtual ICollection<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();

        //public virtual User User { get; set; } = null!;

        public Order ToModel()
        {
            return new Order
            {
                 UserId = UserId,
                  Status = Status,
                PaymentMethod = PaymentMethod,
                TransactionId = TransactionId,
                 OrderItems = OrderItems.ToOrderItem(),
             };
        }

       
    }
    public static class OrderItemExtensions
    {
        public static ICollection<OrderItem> ToOrderItem(this ICollection<OrderItemDTO> orderItemDTOs)
        {
            return orderItemDTOs.Select(dto => new OrderItem
            {
                ProductName = dto.ProductName,
                Quantity = dto.Quantity,
                Price = dto.Price,
                CashbackEligible = dto.CashbackEligible,
                CashbackAmount = dto.CashbackAmount
            }).ToList();
        }
    }


}
