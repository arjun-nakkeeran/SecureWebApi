using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Commands
{
    public class UpdateOrderCommand: CreateOrderCommand
    {
        public Guid OrderId { get; set; }
    }
}
