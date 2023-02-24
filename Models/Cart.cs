using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Cart
    {
        public int CartId { get; set; }

        public int NumberOfArticles { get; set; }

        public double Total { get; set; }
        
        public ICollection<CartRow> Rows { get; set; }
    }
}
