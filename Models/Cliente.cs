using System.ComponentModel.DataAnnotations;

namespace Tienda_CURD;

public class Cliente
{
    [Key]
    public int ClientID { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    
}
