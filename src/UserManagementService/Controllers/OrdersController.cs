using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagementService.Core.ordersDTO;
using UserManagementService.Core.ServiceContract;

namespace UserManagementService.app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrder _orderRepository;

        public OrdersController(IOrder order)
        {
            _orderRepository = order;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetOrdersDTO>>> GetAllOrders()
        {
            try
            {
                var getOrdersDTOs = await _orderRepository.GetOrders();
                return Ok(getOrdersDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new orderResponse { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<GetOrdersDTO>> AddOrder(addOrderDTO order)
        {
            try
            {
                var addedOrder = await _orderRepository.AddOrder(order);
                return Ok("Added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new orderResponse { Status = false, Message = ex.Message });
            }
        }
    }
}
