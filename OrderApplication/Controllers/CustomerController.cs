using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApplication.Models;
using OrderApplication.Data;
using OrderApplication.DTO;

namespace backend_database_HTTP_Requests.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly Context _context;

        public CustomersController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var customer = from customers in _context.Customer
                join customers_descriptions in _context.Customer_description on customers.Id equals
                    customers_descriptions.Customers_id
                select new CustomerDTO
                {
                    Customer_id = customers.Id,
                    Age = customers_descriptions.age,
                    First_name = customers_descriptions.first_name,
                    Last_name = customers_descriptions.last_name,
                    Address = customers_descriptions.address,
                    Country = customers_descriptions.country,
                    Grade = customers.grade
                };

            return await customer.ToListAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerDTO> GetCustomers_byId(int id)
        {
            var product = from products in _context.Product
                join product_descriptions in _context.Product_Description on products.id equals product_descriptions.product_id
                join order in _context.Order on products.id equals order.Product_id
                select new ProductDTO
                {
                    Product_id = products.id,
                    Product_price = products.price,
                    ISBN = products.isbn,
                    Product_name = product_descriptions.product_name,
                    Product_description = product_descriptions.product_description,
                    table_number = order.table_number,
                    Customer_id = order.Customer_id,
                    Id = order.Id,
                };

            var customer = from customers in _context.Customer
                join customers_descriptions in _context.Customer_description on customers.Id equals
                    customers_descriptions.Customers_id
                join order in _context.Order on customers.Id equals order.Customer_id
                select new CustomerDetailsDTO
                {
                    Customer_id = customers.Id,
                    Age = customers_descriptions.age,
                    First_name = customers_descriptions.first_name,
                    Last_name = customers_descriptions.last_name,
                    Address = customers_descriptions.address,
                    Country = customers_descriptions.country,
                    Grade = customers.grade,
                    Products = product.Where(x => x.Product_id == order.Product_id).ToList()
                };

            var customer_by_id = customer.ToList().Find(x => x.Customer_id == id);

            if (customer_by_id == null)
            {
                return NotFound();
            }

            return customer_by_id;
        }
    }
}