using System.ComponentModel.DataAnnotations;

namespace WpfApp1.Models;

public class Product
{
    [Key]
    public Guid Uid { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть больше 0")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Количество не может быть отрицательным")]
    public int Quantity { get; set; }

    public DateTime CreatedDate { get; set; }
}
