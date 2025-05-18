using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.ORM.Seed;

public static class DbInitializer
{
    public static void Seed(DefaultContext context)
    {
        if (!context.Customers.Any())
        {
            context.Customers.Add(new Customer
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Cliente Teste",
                DocumentNumber = "12345678900"
            });
        }

        if (!context.Branches.Any())
        {
            context.Branches.Add(new Branch
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Loja Central",
                Region = "Centro-Oeste"
            });
        }

        if (!context.Products.Any())
        {
            context.Products.AddRange(
                new Product
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Produto A",
                    UnitPrice = 30m
                },
                new Product
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    Name = "Produto B",
                    UnitPrice = 50m
                });
        }
		
		
		if (!context.Sales.Any())
		{
			var saleId = Guid.Parse("55555555-5555-5555-5555-555555555555");

			var sale = new Sale
			{
				Id = saleId,
				SaleNumber = "SALE001",
				Date = DateTime.UtcNow,
				CustomerId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
				BranchId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
				//IsCancelled = false,
				Items = new List<SaleItem>
				{
					new SaleItem
					{
						Id = Guid.NewGuid(),
						ProductId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
						Quantity = 5,
						UnitPrice = 30,
						Discount = 5
					},
					new SaleItem
					{
						Id = Guid.NewGuid(),
						ProductId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
						Quantity = 2,
						UnitPrice = 50,
						Discount = 0
					}
				}
			};

			context.Sales.Add(sale);
		}
        context.SaveChanges();
    }
}
