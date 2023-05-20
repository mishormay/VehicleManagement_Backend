using System.ComponentModel.DataAnnotations;
using VehicleManagement.Entities;

namespace VehicleManagement.ViewModels
{
    public class CreateUserViewModel
    {
        public User user { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
