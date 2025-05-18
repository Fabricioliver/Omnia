using System;

namespace Ambev.DeveloperEvaluation.WebApi.DTOs;

/// <summary>
/// Represents an item in a returned sale.
/// </summary>
public class SaleItemResponse
{
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalItem { get; set; }
}
