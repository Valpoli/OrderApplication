﻿using System.ComponentModel.DataAnnotations;
namespace OrderApplication.Models
{
    public class CustomerDTO
    {
        public int Customer_id { get; set; }
        public string Grade { get; set; }
        public int Age { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
    }
}