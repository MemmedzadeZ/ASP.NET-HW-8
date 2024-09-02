using Microsoft.AspNetCore.Mvc;
using WbDemo.Data;
using WbDemo.Dtos;
using WbDemo.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WbDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly DemoDbContext _demoDbContext;

        public OrderController(DemoDbContext demoDbContext)
        {
            _demoDbContext = demoDbContext;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public IEnumerable<OrderDto> Get()
        {
            var item = _demoDbContext.Orders;
            var orders = item.Select(o => new OrderDto
            {
                Date = o.Date,
                ProductId = o.ProductId,
                CustomerId = o.CustomerId,
            });
            return orders;
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public ActionResult<OrderExtendedDto> Get(int id)
        {
            var item = _demoDbContext.Orders;
            var orders = item.Select(o => new OrderExtendedDto
            {
                Id = o.Id,
                Date = o.Date,
                ProductId = o.ProductId,
                CustomerId = o.CustomerId,
            });
            return orders.FirstOrDefault(p => p.Id == id);
        }

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult Post([FromBody] OrderDto dto)
        {
            if(dto.CustomerId >0 && dto.ProductId > 0)
            {
                var order = new Order
                {
                    Date = dto.Date,
                    ProductId = dto.ProductId,
                    CustomerId = dto.CustomerId,


                };
                _demoDbContext.Orders.Add(order);
                _demoDbContext.SaveChanges();
                return Ok(dto);
                
            }
            return BadRequest("Invalid Id Product and Customer");
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] OrderDto dto)
        {
            var order = _demoDbContext.Orders.FirstOrDefault(o => o.Id == id);
            if(order == null) return NotFound("Order NotFound");
            
               order.Date = dto.Date;
                order.ProductId = dto.ProductId;
                order.CustomerId = dto.CustomerId;
                return Ok(order);
            
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _demoDbContext.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null) return NotFound();
            _demoDbContext.Orders.Remove(order);
            return Ok();


        }
    }
}
