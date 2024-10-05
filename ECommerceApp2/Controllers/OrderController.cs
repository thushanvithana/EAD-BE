using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceApp2.Models;
using ECommerceApp2.Services.Interfaces;

namespace ECommerceApp2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(string id)
        {
            var orderDto = await _orderService.GetOrderByIdAsync(id);
            if (orderDto == null) return NotFound();
            return Ok(orderDto);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByCustomerId(string customerId)
        {
            var orders = await _orderService.GetOrdersByCustomerIdAsync(customerId);
            return Ok(orders);
        }

        // Existing endpoint to get all orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(Order order)
        {
            await _orderService.CreateOrderAsync(order);
            // Assuming order.Id is generated and populated
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(string id, Order order)
        {
            if (id != order.Id) return BadRequest();
            await _orderService.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> CancelOrder(string id, [FromQuery] string cancellationReason)
        {
            await _orderService.CancelOrderAsync(id, cancellationReason);
            return NoContent();
        }



        [HttpPost("create-with-email")]
        public async Task<ActionResult> CreateOrderWithEmail([FromBody] CreateOrderRequest request)
        {
            // Call service to create order with email
            var order = await _orderService.CreateOrderWithEmailAsync(request);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }


        [HttpGet("{id}/detailed-items")]
        public async Task<ActionResult<OrderWithDetailsDto>> GetOrderWithDetails(string id)
        {
            var orderWithDetailsDto = await _orderService.GetOrderWithDetailsByIdAsync(id);
            if (orderWithDetailsDto == null) return NotFound();
            return Ok(orderWithDetailsDto);
        }


        [HttpGet("GetAll-detailed")]
        public async Task<ActionResult<IEnumerable<OrderWithDetailsDto>>> GetAllOrdersWithDetails()
        {
            var ordersWithDetails = await _orderService.GetAllOrdersWithDetailsAsync();
            return Ok(ordersWithDetails);
        }



        [HttpPost("{id}/ready-for-delivery")]
        public async Task<ActionResult> MarkItemAsReadyForDelivery(string id, [FromQuery] string productId, [FromQuery] string vendorId)
        {
            await _orderService.MarkItemAsReadyForDelivery(id, productId, vendorId);
            return NoContent();
        }
    }
}
