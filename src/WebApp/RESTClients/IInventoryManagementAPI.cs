namespace WebApp.RESTClients;

public interface IInventoryManagementAPI {
    [Get("/inventory")]
    Task<List<Product>> GetProducts();

    [Get("/inventory/{id}")]
    Task<Product> GetProductById([AliasAs("id")] string productId);

    [Post("/inventory")]
    Task RegisterProduct(RegisterProduct command);
}
