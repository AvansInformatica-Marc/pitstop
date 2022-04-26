namespace Pitstop.WebApp.Models;

public class Product {
    public string ProductId { get; set; }

    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Price")]
    public double Price { get; set; }
}
