using DataAccess.Models;
using MediatR;

namespace DataAccess.Commands
{
    public class GetOrdersQuery : IRequest<List<Order>>
    {
        public Guid OrderId { get; set; } = Guid.Empty;
    }
}
