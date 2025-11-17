using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public List<string> Items { get; set; }
        public string Status { get; set; }
    }
}
