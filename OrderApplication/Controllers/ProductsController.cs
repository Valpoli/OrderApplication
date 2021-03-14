using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderApplication.Data;
using OrderApplication.DTO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderApplication.Models;
using DTO;

namespace OrderApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly Context _context;

        public ProductsController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var product = from products in _context.Product
                join product_descriptions in _context.Product_Description on products.id equals product_descriptions
                    .product_id
                select new ProductDTO
                {
                    Product_id = products.id,
                    Product_price = products.price,
                    ISBN = products.isbn,
                    Product_name = product_descriptions.product_name,
                    Product_description = product_descriptions.product_description
                };

            return await product.ToListAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDTO> GetProducts_byId(int id)
        {
            var product = from products in _context.Product
                join product_descriptions in _context.Product_Description on products.id equals product_descriptions
                    .product_id
                select new ProductDTO
                {
                    Product_id = products.id,
                    Product_price = products.price,
                    ISBN = products.isbn,
                    Product_name = product_descriptions.product_name,
                    Product_description = product_descriptions.product_description
                };

            var product_by_id = product.ToList().Find(x => x.Product_id == id);

            if (product_by_id == null)
            {
                return NotFound();
            }

            return product_by_id;
        }

        [HttpPost]
        public async Task<ActionResult<AddProduct>> Add_Products(AddProduct productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = new Product()
            {
                isbn = productDTO.ISBN,
                price = productDTO.Product_price
            };
            await _context.Product.AddAsync(product);
            await _context.SaveChangesAsync();

            var product_description = new Product_description()
            {
                product_id = product.id,
                product_name = productDTO.Product_name,
                product_description = productDTO.Product_description
            };
            await _context.AddAsync(product_description);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new {id = product.id}, productDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete_Product(int id)
        {
            var product = _context.Product.Find(id);
            var product_description = _context.Product_Description.SingleOrDefault(x => x.product_id == id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(product);
                _context.Remove(product_description);
                await _context.SaveChangesAsync();
                return product;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update_Products(int id, ProductDTO product)
        {
            if (id != product.Product_id || !ProductExists(id))
            {
                return BadRequest();
            }
            else
            {
                var products = _context.Product.SingleOrDefault(x => x.id == id);
                var products_description = _context.Product_Description.SingleOrDefault(x => x.product_id == id);

                products.isbn = product.ISBN;
                products.price = product.Product_price;
                products_description.product_name = product.Product_name;
                products_description.product_description = product.Product_description;
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(x => x.id == id);
        }
    }
}