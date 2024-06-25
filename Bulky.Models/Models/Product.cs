using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bulky.Models;

public class Product
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Description { get; set; }
    [Required]
    public string ISBN { get; set; }
    [Required]
    public string Author { get; set; }
    [Required]
    [Display(Name = "List Price")]
    public double ListPrice { get; set; }
    [Required]
    [Display(Name = "Price for 1 - 50")]
    public double Price { get; set; }
    [Required]
    [Display(Name = "Price for 50+")]
    public double Price50 { get; set; }
    [Required]
    [Display(Name = "Price for 100+")]
    public double Price100 { get; set; }
    [ForeignKey("CategoryId")]
    [ValidateNever]
    public int CategoryId { get; set; }
    [ValidateNever]
    public Category? Category { get; set; }
    [ValidateNever]
    public string? ImgUrl { get; set; }
}
