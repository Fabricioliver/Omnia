using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.WebApi.Services;

public class DomainEventLogger
{
    private readonly ILogger<DomainEventLogger> _logger;

    public DomainEventLogger(ILogger<DomainEventLogger> logger)
    {
        _logger = logger;
    }

    public void LogSaleCreated(Guid saleId)
    {
        _logger.LogInformation("Event: SaleCreated | SaleId: {SaleId}", saleId);
    }

    public void LogSaleCancelled(Guid saleId)
    {
        _logger.LogInformation("Event: SaleCancelled | SaleId: {SaleId}", saleId);
    }

    public void LogSaleDeleted(Guid saleId)
    {
        _logger.LogInformation("Event: SaleDeleted | SaleId: {SaleId}", saleId);
    }
}
