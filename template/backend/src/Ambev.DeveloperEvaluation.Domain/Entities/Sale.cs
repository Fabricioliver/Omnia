using System;
using System.Collections.Generic;
using System.Linq;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale transaction, which may contain multiple items,
/// be associated with a customer and a branch, and support cancellation.
/// </summary>
public class Sale
{
    /// <summary>
    /// Gets or sets the unique identifier of the sale.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the sale number, used as a human-readable reference.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date and time when the sale occurred.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the customer who made the purchase.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Navigation property for the customer.
    /// </summary>
    public Customer Customer { get; set; } = null!;

    /// <summary>
    /// Gets or sets the identifier of the branch where the sale occurred.
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// Navigation property for the branch.
    /// </summary>
    public Branch Branch { get; set; } = null!;

    /// <summary>
    /// Indicates whether the sale was cancelled.
    /// </summary>
    public bool IsCancelled { get; private set; }

    /// <summary>
    /// Collection of items sold in this sale.
    /// </summary>
    public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();

    /// <summary>
    /// Gets the total amount of the sale, calculated from all items.
    /// </summary>
    public decimal TotalAmount => Items.Sum(i => i.TotalItem);

    /// <summary>
    /// Cancels the sale. Once cancelled, it cannot be undone.
    /// </summary>
    public void Cancel()
    {
        if (IsCancelled)
            throw new InvalidOperationException("This sale is already cancelled.");

        IsCancelled = true;
    }

    /// <summary>
    /// Adds an item to the sale.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public void AddItem(SaleItem item)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot add items to a cancelled sale.");

        Items.Add(item);
    }
	
	public void ApplyAutomaticDisccount()
	{
		var totalItems = Items.Sum(i => i.Quantity);

		if (totalItems > 20)
			throw new InvalidOperationException("Sales of more than 20 items are not permitted.");

		decimal percentual = 0;

		if (totalItems >= 10)
			percentual = 0.20m;
		else if (totalItems >= 4)
			percentual = 0.10m;

		foreach (var item in Items)
		{
			var calculateDdiscount = item.UnitPrice * item.Quantity * percentual;
			item.Discount = calculateDdiscount;
		}
	}
	
	
	
	
	
}
