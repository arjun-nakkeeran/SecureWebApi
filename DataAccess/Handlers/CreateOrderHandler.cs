using DataAccess.Commands;
using DataAccess.Models;
using DataAccess.Services;
using MediatR;
using System.Text.Json;

namespace DataAccess.Handlers
{
    internal class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Guid orderId = Guid.NewGuid();
            Order newOrder = new Order
            {
                Id = orderId,
                CustomerName = request.CustomerName,
                Items = request.Items,
            };
            var eventData = JsonSerializer.Serialize(newOrder);
            await File.AppendAllLinesAsync(EventStoreConfig.EventStoreFilePath, [eventData], cancellationToken);
            return orderId;
        }
    }
}
