#pragma warning disable CS8618
namespace CRUDelicious.Models;
using System.ComponentModel.DataAnnotations;

public class Dish
{
    [Key]
    public int DishId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [Display(Name = "Name of Dish: ")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Chef is required.")]
    [Display(Name = "Chef's Name: ")]
    public string Chef { get; set; }

    [Required(ErrorMessage = "Tastiness is required.")]
    [Display(Name = "Tastiness: ")]
    [Range(1,5, ErrorMessage ="Choose a Tastiness Rating between 1-5")]
    public int Tastiness { get; set; }

    [Required(ErrorMessage = "Calories is required.")]
    [Display(Name = "# of Calories: ")]
    [Range(1,int.MaxValue, ErrorMessage ="Enter a value greater than 0")]
    public int? Calories { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    [Display(Name = "Description: ")]
    public string Text { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}