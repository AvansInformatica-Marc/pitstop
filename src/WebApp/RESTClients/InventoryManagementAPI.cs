namespace WebApp.RESTClients;

public class InventoryManagementAPI : IInventoryManagementAPI {
    private IInventoryManagementAPI _restClient;

    public InventoryManagementAPI(IConfiguration config, HttpClient httpClient) {
        string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("InventoryManagementAPI");
        httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api");
        _restClient = RestService.For<IInventoryManagementAPI>(
            httpClient,
            new RefitSettings {
                ContentSerializer = new NewtonsoftJsonContentSerializer()
            }
        );
    }

    public async Task<List<Product>> GetProducts() {
        return await _restClient.GetProducts();
    }

    public async Task<Product> GetProductById([AliasAs("id")] string productId) {
        try {
            return await _restClient.GetProductById(productId);
        } catch (ApiException ex) {
            if (ex.StatusCode == HttpStatusCode.NotFound) {
                return null;
            } else {
                throw;
            }
        }
    }

    public async Task RegisterProduct(RegisterProduct command) {
        await _restClient.RegisterProduct(command);
    }
}
