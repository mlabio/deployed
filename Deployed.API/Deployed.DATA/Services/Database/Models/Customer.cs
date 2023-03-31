using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployed.Data.Services.Database.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public bool IsActive { get; set; }
        public string? CustomerType { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public ICollection<Dependent> Dependents { get; set; }
        public ICollection<CustomerDetail> CustomerDetails { get; set; }
    }
}
