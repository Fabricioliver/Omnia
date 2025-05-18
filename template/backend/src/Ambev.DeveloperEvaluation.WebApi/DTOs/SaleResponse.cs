using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.DTOs;

/// <summary>
/// Represents the response returned when querying a sale.
/// </summary>
public class SaleResponse
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string BranchName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }

    public List<SaleItemResponse> Items { get; set; } = new();
}
