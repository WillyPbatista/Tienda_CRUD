using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Tienda_CURD.Models;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderID { get; set; }
    public DateTime Date { get; set; }
    public int ClientID { get; set; }
    public Cliente Client { get; set; } = null!;
    public List<OrderDetails> Details { get; set; } = null!;
}
