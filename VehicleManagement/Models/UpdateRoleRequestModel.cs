using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models
{
    public class UpdateRoleRequestModel
    {
        public int UserId { get; set; }
        public bool IsChecked { get; set; }
    }
}
