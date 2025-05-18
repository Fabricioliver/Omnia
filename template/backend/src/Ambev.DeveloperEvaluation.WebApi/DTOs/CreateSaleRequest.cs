using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.DTOs;

/// <summary>
/// Represents a request to create a new sale.
/// </summary>
public class CreateSaleRequest
{
    /// <summary>
    /// Customer identifier.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Branch identifier.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// List of items in the sale.
    /// </summary>
    public List<SaleItemRequest> Items { get; set; } = new();
}
