using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Entities
{
    public partial class UserRole
    {
        public UserRole()
        {
            userRoles = new HashSet<UserInRole>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string RoleName { get; set; }
        public virtual ICollection<UserInRole> userRoles { get; set; }
    }
}
