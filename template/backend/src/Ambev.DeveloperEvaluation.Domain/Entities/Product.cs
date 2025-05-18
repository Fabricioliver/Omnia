using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a product that can be sold in a sale.
/// </summary>
public class Product
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Collection of sale items in which this product appears.
    /// </summary>
    public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
}
