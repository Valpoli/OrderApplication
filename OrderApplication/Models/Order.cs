using System.ComponentModel.DataAnnotations;
namespace OrderApplication.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int Customer_id { get; set; }
        public int Product_id { get; set; }
        public int table_number { get; set; }
    }
}