using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagement.Entities
{
    public partial class PasswordReset
    {
        [Key]
        [StringLength(250)]
        public string Token { get; set; }
        [Required]
        [StringLength(250)]
        public string Email { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
    }
}
