using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VehicleManagement.Entities;

namespace VehicleManagement.Models
{
    public class LoginRequestModel
    {
        [Required]
        public string email { get; set; }

        [Required]
        public string Password { get; set; }
     
    }

    public class forgotPasswrodRequestModel
    {
        public string email { get; set; }
    }

    public class ChangeUserPasswordModel
    {
        public User user { get; set; }
        public string NewPassword { get; set; }
    }
}
