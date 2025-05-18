using System;

namespace Ambev.DeveloperEvaluation.WebApi.DTOs;

/// <summary>
/// Represents an item in a sale request.
/// </summary>
public class SaleItemRequest
{
    /// <summary>
    /// Product identifier.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Quantity to be sold.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Discount applied to the item.
    /// </summary>
    //public decimal Discount { get; set; }
	//the discount is now automatic, commented to avoid conflicts.
}
