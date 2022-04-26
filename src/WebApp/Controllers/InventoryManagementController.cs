namespace PitStop.WebApp.Controllers;

public class InventoryManagementController : Controller {
    private readonly IInventoryManagementAPI _inventoryManagementAPI;
    private readonly Microsoft.Extensions.Logging.ILogger _logger;
    private ResiliencyHelper _resiliencyHelper;

    public InventoryManagementController(IInventoryManagementAPI inventoryManagementAPI, ILogger<InventoryManagementController> logger) {
        _inventoryManagementAPI = inventoryManagementAPI;
        _logger = logger;
        _resiliencyHelper = new ResiliencyHelper(_logger);
    }

    [HttpGet]
    public async Task<IActionResult> Index() {
        return await _resiliencyHelper.ExecuteResilient(async () => {
            var model = new InventoryManagementViewModel {
                Products = await _inventoryManagementAPI.GetProducts()
            };
            return View(model);
        }, View("Offline", new InventoryManagementOfflineViewModel()));
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id) {
        return await _resiliencyHelper.ExecuteResilient(async () => {
            var model = new InventoryManagementDetailsViewModel {
                Product = await _inventoryManagementAPI.GetProductById(id)
            };
            return View(model);
        }, View("Offline", new InventoryManagementOfflineViewModel()));
    }

    [HttpGet]
    public IActionResult New() {
        var model = new InventoryManagementNewViewModel {
            Product = new Product()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] InventoryManagementNewViewModel inputModel) {
        if (ModelState.IsValid) {
            return await _resiliencyHelper.ExecuteResilient(async () => {
                RegisterProduct cmd = inputModel.MapToRegisterProduct();
                await _inventoryManagementAPI.RegisterProduct(cmd);
                return RedirectToAction("Index");
            }, View("Offline", new InventoryManagementOfflineViewModel()));
        } else {
            return View("New", inputModel);
        }
    }
}
