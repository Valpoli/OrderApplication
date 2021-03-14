using OrderApplication.Models;

namespace OrderApplication.DTO
{
    public class ProductDTO : Order
    {
        public decimal Product_price { get; set; }
        public string ISBN { get; set; }
        public string Product_name { get; set; }
        public string Product_description { get; set; }
    }
}