using AutoMapper;
using Deployed.Data.Services.Database.Models;
using System.Dynamic;

namespace Deployed.Models.Response
{
    public class CustomerResponseModel
    {
        public int CustomerId { get; set; }
        public string CustomerType { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string Birthday { get; set; }

        public List<DependentResponseModel> Dependents { get; set; }
    }
}
