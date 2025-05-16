using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.Models;

namespace UserManagementService.Core.RepositoryContract
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders();

        Task<Order> GetAllOrders( int id);

        Task<Order> AddOrder(Order order);

    }
}
