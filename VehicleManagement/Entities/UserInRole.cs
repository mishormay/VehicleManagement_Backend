using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Entities
{
    public partial class UserInRole
    {
        public UserInRole()
        {

        }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int RoleId { get; set; }

        public virtual User user { get; set; }
        public virtual UserRole userRole { get; set; }


    }
}
