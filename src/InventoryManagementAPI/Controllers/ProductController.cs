namespace Pitstop.Application.InventoryManagementAPI.Controllers;

using Pitstop.InventoryManagementAPI.DataAccess;
using Pitstop.InventoryManagementAPI.Commands;
using Pitstop.InventoryManagementAPI.Events;
using Pitstop.InventoryManagementAPI.Model;
using Pitstop.InventoryManagementAPI.Mappers;

[Route("/api/[controller]")]
public class InventoryController : Controller {
    IMessagePublisher _messagePublisher;
    InventoryManagementDBContext _dbContext;

    public InventoryController(InventoryManagementDBContext dbContext, IMessagePublisher messagePublisher) {
        _dbContext = dbContext;
        _messagePublisher = messagePublisher;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync() {
        return Ok(await _dbContext.Products.ToListAsync());
    }

    [HttpGet]
    [Route("{productId}", Name = "GetByProductId")]
    public async Task<IActionResult> GetByProductId(string productId) {
        var customer = await _dbContext.Products.FirstOrDefaultAsync(c => c.ProductId == productId);
        if (customer == null) {
            return NotFound();
        }
        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterProduct command) {
        try {
            if (ModelState.IsValid) {
                // insert product
                Product product = command.MapToProduct();
                _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync();

                // send event
                ProductRegistered e = command.MapToProductRegistered();
                await _messagePublisher.PublishMessageAsync(e.MessageType, e, "");

                // return result
                return CreatedAtRoute("GetByProductId", new { productId = product.ProductId }, product);
            }
            return BadRequest();
        } catch (DbUpdateException) {
            ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists " +
                "see your system administrator.");
            return StatusCode(StatusCodes.Status500InternalServerError);
            throw;
        }
    }
}
