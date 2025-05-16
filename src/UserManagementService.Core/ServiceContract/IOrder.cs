using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.ordersDTO;

namespace UserManagementService.Core.ServiceContract
{
    public interface IOrder
    {
        Task<List<GetOrdersDTO>> GetOrders();

        Task<GetOrdersDTO> AddOrder(addOrderDTO order);
    }
}
