using System.ComponentModel.DataAnnotations;
namespace OrderApplication.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        public string grade { get; set; }

    }
}