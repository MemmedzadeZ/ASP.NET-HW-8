using Microsoft.AspNetCore.Mvc;
using WbDemo.Data;
using WbDemo.Dtos;
using WbDemo.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WbDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly DemoDbContext _demoDbContext;
        public CustomerController(DemoDbContext demoDbContext)
        {
            _demoDbContext = demoDbContext;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<CustomerDto> Get()
        {
            var item = _demoDbContext.Customers;
            var customer = item.Select(c => new CustomerDto
            {
                Name = c.Name,
                Surname = c.Surname,
            });
            return customer;
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public ActionResult<CustomerExtendedDto> Get(int id)
        {
            var item = _demoDbContext.Customers;
            var customers = item.Select(c => new CustomerExtendedDto
            {
                Name = c.Name,
                Surname = c.Surname,
            });
            return customers.FirstOrDefault(c => c.Id == id);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post([FromBody] CustomerDto dto)
        {
            if(dto.Name !=null && dto.Surname != null)
            {
                var customer = new Customer
                {
                    Name = dto.Name,
                    Surname = dto.Surname,

                };
                _demoDbContext.Customers.Add(customer);
                _demoDbContext.SaveChanges();
                return Ok(dto);

            }
            return BadRequest("Invalid Name and Surname");
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CustomerDto dto)
        {
            var customer = _demoDbContext.Customers.FirstOrDefault(c => c.Id==id);
            if (customer == null) return NotFound("Customer NotFound");
            customer.Name = dto.Name;
            customer.Surname = dto.Surname;
            return Ok(customer);
            
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var customer = _demoDbContext.Customers.FirstOrDefault(c=>c.Id==id);
            if (customer == null) return NotFound()
         ; _demoDbContext.Customers.Remove(customer);
            return Ok();
        }
    }
}
