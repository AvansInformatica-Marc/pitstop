namespace Pitstop.InventoryManagementAPI.Mappers;

using Pitstop.InventoryManagementAPI.Commands;
using Pitstop.InventoryManagementAPI.Events;
using Pitstop.InventoryManagementAPI.Model;

public static class Mappers {
    public static ProductRegistered MapToProductRegistered(this RegisterProduct command) => new ProductRegistered(
        System.Guid.NewGuid(),
        command.ProductId,
        command.Name,
        command.Price
    );

    public static Product MapToProduct(this RegisterProduct command) => new Product(
        command.ProductId,
        command.Name,
        command.Price
    );
}
