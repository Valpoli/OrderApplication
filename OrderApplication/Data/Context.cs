using Microsoft.EntityFrameworkCore;
using OrderApplication.Models;

namespace OrderApplication.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) {}
        public DbSet<Product> Product {get; set;}
        public DbSet<Product_description> Product_Description {get; set;}
        public DbSet<Customer> Customer {get; set;}
        public DbSet<Customer_description> Customer_description {get; set;}
        public DbSet<Order> Order {get; set;}
    }
}