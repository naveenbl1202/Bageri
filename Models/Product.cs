using SkaftoBageriWMS.Api.Models;

namespace SkaftoBageriWMS.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        // Change the supplier to a reference to the Supplier entity
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; } // Navigation property
    }
}
