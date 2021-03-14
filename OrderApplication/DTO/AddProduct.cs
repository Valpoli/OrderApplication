using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class AddProduct
    {
        [Required]
        public string Product_name { get; set; }
        [Required]
        public string Product_description { get; set; }
        [Required]
        public decimal Product_price { get; set; }
        [Required]
        public string ISBN { get; set; }
    }
}