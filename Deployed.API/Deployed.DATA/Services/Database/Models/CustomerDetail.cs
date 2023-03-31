using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployed.Data.Services.Database.Models
{
    public class CustomerDetail
    {
        [Key]
        public int CustomerDetailsId { get; set; }
        public int CustomerId { get; set; } 
        public string? MetaKey { get; set; }
        public string? MetaValue { get; set; }

        public Customer Customer { get; set; }
    }
}
