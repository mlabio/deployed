using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployed.Data.Services.Database.Models
{
    public class Dependent
    {
        [Key]
        public int DependentId { get; set; }
        public int CustomerId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public Customer Customer { get; set; }
        public ICollection<DependentDetail> DependentDetails { get; set; }
    }
}
