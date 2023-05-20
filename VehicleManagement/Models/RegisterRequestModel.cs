using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleManagement.Models
{
    public class RegisterRequestModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string gender { get; set; }
        public string birthday { get; set; }
        public int city_id { get; set; }
        public int area_id { get; set; }
        public string phone { get; set; }
        public int income_range { get; set; }
    }
}
