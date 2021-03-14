using System.ComponentModel.DataAnnotations;
namespace OrderApplication.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }
        public decimal price { get; set; }
        public string isbn { get; set; }
    }
}