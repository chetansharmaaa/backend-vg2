using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.ordersDTO;
using UserManagementService.Core.RepositoryContract;
using UserManagementService.Core.ServiceContract;

namespace UserManagementService.Core.Service
{
    public class OrderService : IOrder
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<GetOrdersDTO> AddOrder(addOrderDTO orderDto)
        {
            var order = orderDto.ToModel();
            var addedOrder = await _orderRepository.AddOrder(order);
            return addedOrder.ToDTO();
        }

        public async Task<List<GetOrdersDTO>> GetOrders()
        {
            var orders = await _orderRepository.GetAllOrders();
            return orders.Select(o => o.ToDTO()).ToList();
        }
    }
}
