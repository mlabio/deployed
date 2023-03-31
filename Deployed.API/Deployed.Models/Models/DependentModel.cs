using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployed.Models.Models
{
    public class DependentModel
    {
        public bool IsActive { get; set; }
        public DependentDetailModel DependentDetail { get; set; }
    }

    public class DependentDetailModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
    }
}
