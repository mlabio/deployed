using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deployed.Data.Services.Database.Models
{
    public class DependentDetail
    {
        [Key]
        public int DependentDetailsId { get; set; }
        public int DependentId { get; set; }
        public string? MetaKey { get; set; }
        public string? MetaValue { get; set; }

        public Dependent Dependent { get; set; }
    }
}
