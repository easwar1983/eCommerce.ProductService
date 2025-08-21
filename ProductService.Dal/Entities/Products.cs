using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Dal.Entities;

[Index(nameof(ProductName), IsUnique = true)]

public class Products
{
    [Key]
    public Guid ProductId { get; set; }

    [Required]
    public string ProductName { get; set; }
    public string Category { get; set; }
    public double? UnitPrice { get; set; }
    public int? QuantityInStock { get; set; }
}
