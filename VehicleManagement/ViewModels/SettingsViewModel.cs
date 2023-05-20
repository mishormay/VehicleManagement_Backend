using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using VehicleManagement.Entities;

namespace VehicleManagement.ViewModels
{
    public class SettingsViewModel
    {

        public AppSettings AppSettings { get; set; }

        [Required]
        [StringLength(250)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(250)]
        public string LastName { get; set; }
        [StringLength(250)]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(250)]
        public string Email { get; set; }

        [Required]
        [StringLength(250)]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string UserNewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("UserNewPassword", ErrorMessage = "'New Password' and 'New Password Again' do not match.")]
        public string ConfirmUserNewPassword { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal? Latitude { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal? Longitude { get; set; }


    }
}
