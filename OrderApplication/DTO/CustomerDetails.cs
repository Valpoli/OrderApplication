using System.Collections.Generic;
using OrderApplication.Models;
using OrderApplication.DTO;

namespace DTO
{
    public class CustomerDetailsDTO : CustomerDTO
    {
        public List<ProductDTO> Products { get; set; }
    }
}