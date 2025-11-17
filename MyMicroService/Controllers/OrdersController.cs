using DataAccess.Commands;
using DataAccess.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            var orders = await _mediator.Send(new GetOrdersQuery());
            //var orders = new List<Order>
            //{
            //    new() { Id = Guid.NewGuid(), CustomerName = "Alice", Items = new List<string> { "Item1", "Item2" }, Status = "Pending" },
            //    new() { Id = Guid.NewGuid(), CustomerName = "Bob", Items = new List<string> { "Item3" }, Status = "Shipped" }
            //};
            return Ok(orders);
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(Guid id)
        {
            var orders = await _mediator.Send(new GetOrdersQuery() {  OrderId = id});
            //var order = new Order { Id = id, CustomerName = "Alice", Items = new List<string> { "Item1", "Item2" }, Status = "Pending" };
            return Ok(orders.LastOrDefault());
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Order order)
        {
            // Add order to data store
            var cmd = new CreateOrderCommand
            {
                CustomerName = order.CustomerName,
                Items = order.Items
            };
            Guid newOrderId = await _mediator.Send(cmd);
            order.Id = newOrderId;

            return CreatedAtAction(nameof(Get), new { id = newOrderId }, order);
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] Order order)
        {
            // Update order in data store
            var cmd = new UpdateOrderCommand
            {
                OrderId = id,
                CustomerName = order.CustomerName,
                Items = order.Items,
            };
            await _mediator.Send(cmd);
            return NoContent();
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            // Delete order from data store (not implemented)
            return NoContent();
        }
    }
}
