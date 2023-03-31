using Deployed.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployed.Application.Infrastracture.Interfaces
{
    public interface ICustomerDependentService
    {
        Task AddCustomerDetails(int customerId, CustomerDetailModel model);
        Task RemoveDependents(int customerId);
        Task RemoveCustomerDetails(int customerId);
        void AddDependents(int customerId, List<DependentModel> model);
    }
}
