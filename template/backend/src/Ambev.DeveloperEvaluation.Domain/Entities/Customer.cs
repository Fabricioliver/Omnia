using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a customer who can make purchases.
/// </summary>
public class Customer
{
    /// <summary>
    /// Gets or sets the unique identifier of the customer.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the full name of the customer.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets an optional document number (e.g., CPF/CNPJ).
    /// </summary>
    public string DocumentNumber { get; set; } = string.Empty;

    /// <summary>
    /// Collection of sales made by this customer.
    /// </summary>
    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
