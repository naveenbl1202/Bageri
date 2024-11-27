using System.ComponentModel.DataAnnotations;

namespace SkaftoBageriWMS.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryID { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [StringLength(255)]
        public string ProductName { get; set; }

        public int? ID { get; set; } // Nullable if not all products have suppliers

        [Required(ErrorMessage = "Batch Number is required.")]
        [StringLength(50)]
        public string BatchNumber { get; set; }

        [Range(0, 9999999999.99)]
        public decimal QuantityInStock { get; set; }

        [Range(0, 9999999999.99)]
        public decimal ReorderPoint { get; set; }
    }
}
