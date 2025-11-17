using DataAccess.Commands;
using DataAccess.Models;
using DataAccess.Services;
using MediatR;
using System.Text.Json;

namespace DataAccess.Handlers
{
    internal class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Guid>
    {
        public async Task<Guid> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            Order newOrder = new Order
            {
                Id = request.OrderId,
                CustomerName = request.CustomerName,
                Items = request.Items,
            };
            var eventData = JsonSerializer.Serialize(newOrder);
            await File.AppendAllLinesAsync(EventStoreConfig.EventStoreFilePath, [eventData], cancellationToken);
            return newOrder.Id;
        }
    }
}
