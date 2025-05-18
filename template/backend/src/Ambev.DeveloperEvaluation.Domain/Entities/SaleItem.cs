using System;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents an item within a sale, including product, quantity,
/// pricing, and discount information.
/// </summary>
public class SaleItem
{
    /// <summary>
    /// Gets or sets the unique identifier of the item.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the related sale.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Navigation property for the related sale.
    /// </summary>
    public Sale Sale { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Navigation property for the product.
    /// </summary>
    public Product Product { get; set; } = null!;

    /// <summary>
    /// Gets or sets the quantity of the product in the sale.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the discount applied to this item.
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// Gets the total price for this item (quantity * unit price - discount).
    /// </summary>
    public decimal TotalItem => (UnitPrice * Quantity) - Discount;
}
