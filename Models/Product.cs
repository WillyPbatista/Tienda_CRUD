using System.ComponentModel.DataAnnotations;

namespace Tienda_CURD;

public class Product
{
    public int productID { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public decimal price { get; set; }
}
