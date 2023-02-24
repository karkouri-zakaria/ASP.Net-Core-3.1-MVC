using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class CartRow
    {
        public int CartRowId { get; set; } 

        public int Quantity { get; set; }

        public int? Id { get; set; }

        public int? CartId { get; set; }

        public Cart Cart { get; set; }

        public Product Product { get; set; }
    }
}
