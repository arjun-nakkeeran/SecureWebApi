using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Commands
{
    public class CreateOrderCommand: IRequest<Guid>
    {
        public string CustomerName  { get; set; }
        public List<string> Items { get; set; }
    }
}
