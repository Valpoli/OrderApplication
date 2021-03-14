using System.Collections.Generic;

namespace DTO
{
    public class MakeOrderDTO
    {
        public int Customer_id { get; set; }
        public List<int> products {get; set;}
    }
}