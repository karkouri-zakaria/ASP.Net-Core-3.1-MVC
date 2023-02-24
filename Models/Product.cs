using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="No Product Name")]
        [Display(Name ="Name")]
        [MaxLength(15)]
        [MinLength(5)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "No Product Price")]
        public double Price { get; set; }

        [Required(ErrorMessage = "No Product Quantity")]
        [Range(0,100)]
        public int Quantity { get; set; }
            
        public ICollection<CartRow> Rows { get; set; }
    }
}
