namespace Pitstop.InventoryManagementAPI.Commands;

public class RegisterProduct : Command {
    public readonly string ProductId;
    public readonly string Name;
    public readonly double Price;

    public RegisterProduct(Guid messageId, string productId, string name, double price) : base(messageId) {
        this.ProductId = productId;
        this.Name = name;
        this.Price = price;
    }
}
