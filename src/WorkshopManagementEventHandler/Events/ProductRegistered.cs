namespace Pitstop.WorkshopManagementEventHandler.Events;

public class ProductRegistered : Event {
    public readonly string ProductId;
    public readonly string Name;
    public readonly double Price;

    public ProductRegistered(Guid messageId, string productId, string name, double price) : base(messageId) {
        this.ProductId = productId;
        this.Name = name;
        this.Price = price;
    }
}
