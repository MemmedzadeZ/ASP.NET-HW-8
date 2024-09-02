using Microsoft.AspNetCore.Mvc;
using WbDemo.Data;
using WbDemo.Dtos;
using WbDemo.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WbDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DemoDbContext _demoDbContext;
        public ProductController(DemoDbContext demoDbContext)
        {
            _demoDbContext = demoDbContext;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<ProductDto> Get()
        {
           var item = _demoDbContext.Products;
            var products = item.Select(p => new ProductDto
            {
                Name = p.Name,
                Discount = p.Discount,
                Price = p.Price,
            });
            return products;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public ActionResult<ProductExtendedDto> Get(int id)
        {
            var item = _demoDbContext.Products;
            var products = item.Select(p => new ProductExtendedDto
            {
                Id = p.Id,
                Name = p.Name,
                Discount = p.Discount,
                Price = p.Price,



            });
            return products.FirstOrDefault(p => p.Id == id);

        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductDto dto)
        {
            if(dto.Price > 0)
            {
                var product = new Product
                {
                    Name = dto.Name,
                    Discount = dto.Discount,
                    Price = dto.Price,

                };
                _demoDbContext.Products.Add(product);
                _demoDbContext.SaveChanges();

                return Ok(dto);

            }
            return BadRequest("Invalid Price");
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductDto dto)
        {
            var product = _demoDbContext.Products.FirstOrDefault(p => p.Id == id);
            if(product == null) return NotFound("Product NotFound");

            product.Name = dto.Name;
                product.Discount = dto.Discount;
                product.Price = dto.Price;
                return Ok(product);
            
           
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _demoDbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            _demoDbContext.Products.Remove(product);
            return Ok();
        }
    }
}
