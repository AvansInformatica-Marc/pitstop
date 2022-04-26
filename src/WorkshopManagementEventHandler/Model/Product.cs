namespace Pitstop.WorkshopManagementEventHandler.Model;

public class Product {
    public string ProductId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public Product(string productId, string name, double price) {
        this.ProductId = productId;
        this.Name = name;
        this.Price = price;
    }
}
