using DataAccess.Commands;
using DataAccess.Models;
using DataAccess.Services;
using MediatR;
using System.Text.Json;

namespace DataAccess.Handlers
{
    internal class GetOrdersHandler : IRequestHandler<GetOrdersQuery, List<Order>>
    {
        public async Task<List<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            if (!File.Exists(EventStoreConfig.EventStoreFilePath))
            {
                // Handle the missing file scenario
                return new List<Order>(); // Return an empty list
            }

            var orders = await File.ReadAllLinesAsync(EventStoreConfig.EventStoreFilePath, cancellationToken);
            var orderList = orders
                  .Select(line => JsonSerializer.Deserialize<Order>(line))
                  .Where(order => order != null)
                  .Where(order => request.OrderId == Guid.Empty || order?.Id == request.OrderId)
                  .Cast<Order>()
                  .ToList();

            return orderList;
        }
    }
}
