using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a branch/store where a sale can occur.
/// </summary>
public class Branch
{
    /// <summary>
    /// Gets or sets the unique identifier of the branch.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the branch.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets an optional location or region name.
    /// </summary>
    public string Region { get; set; } = string.Empty;

    /// <summary>
    /// Collection of sales associated with this branch.
    /// </summary>
    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
