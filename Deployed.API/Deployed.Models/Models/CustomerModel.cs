using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployed.Models.Models
{
    public class CustomerModel
    {
        public string CustomerType { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public bool IsActive { get; set; }
        public CustomerDetailModel CustomerDetail { get; set; }
        public List<DependentModel> Dependents { get; set; } = new List<DependentModel>();
    }

    public class CustomerDetailModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string Birthday { get; set; }
    }
}
