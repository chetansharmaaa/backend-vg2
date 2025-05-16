using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.Data;
using UserManagementService.Core.Models;
using UserManagementService.Core.RepositoryContract;

namespace UserManagementService.Infra
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CoolpalzContext _dbContext;

        public OrderRepository(CoolpalzContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Order> AddOrder(Order order)
        {
             /*order.OrderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    ProductName = "Electronic",
                    Quantity = 1,
                    Price = 500.0m,
                    CashbackEligible = true,
                    CashbackAmount = 10m
                },
                new OrderItem
                {
                    ProductName = "Furniture",
                    Quantity = 1,
                    Price = 500.0m,
                    CashbackEligible = true,
                    CashbackAmount = 50m
                },

            };*/
            order.OrderCashbacks = new List<OrderCashback>
            {
               new OrderCashback
            {
                   OrderId = order.OrderId,
                    UserId = order.UserId,
                   CashbackAmount = 10.0m,
                Status = "Pending",
                ApprovedDate = null
            }
            };
            order.TotalAmount = order.OrderItems.Sum(x => x.Price * x.Quantity);

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            await CalculateCashback(order.OrderId);

            return order;

        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _dbContext.Orders.Include(o => o.OrderItems).Include(o => o.OrderCashbacks).ToListAsync();
        }

        public Task<Order> GetAllOrders(int id)
        {
            throw new NotImplementedException();
        }

        private async Task CalculateCashback(int orderId)
        {
            var order = await _dbContext.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)   return ;
             decimal totalCashback = order.OrderItems.Where(item => item.CashbackEligible.GetValueOrDefault()).Sum(item => item.CashbackAmount.GetValueOrDefault());

            var cashbackTransaction = new CashbackTransaction
            {
                UserId = order.UserId,
                Amount = totalCashback,
                Source = "Order",
                TransactionDate = DateTime.UtcNow
            };

            _dbContext.CashbackTransactions.Add(cashbackTransaction);
            await _dbContext.SaveChangesAsync();
        }
    }
}
