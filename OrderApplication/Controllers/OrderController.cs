using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApplication.Models;
using OrderApplication.Data;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly Context _context;

        public OrderController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderProducts()
        {
            return await _context.Order.ToListAsync();
        }

        [HttpPost("MakeOrder")]
        public async Task<ActionResult<IEnumerable<MakeOrderDTO>>> MakeOrder(MakeOrderDTO request)
        {
            foreach(var item in request.products)
            {
                var order = new Order()
                {
                    Customer_id = request.Customer_id,
                    Product_id = item,
                    table_number = 1,
                };
                await _context.Order.AddAsync(order);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetOrderProducts",request);
        }
    }
}