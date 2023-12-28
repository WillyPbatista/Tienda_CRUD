
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Tienda_CURD.Models
{
    public class OrderDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderDetailsID { get; set; }
        [Required]
        public int Cant { get; set; }
        [Required]
        public int OrderID { get; set; }
        public Order Order { get; set; } = null!;   
        [Required]
        public int ProductID { get; set; }
        public Product Product { get; set; } = null!;
        
    }
}