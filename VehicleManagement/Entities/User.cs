using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagement.Entities
{
    public partial class User
    {
        public User()
        {
            userRoles = new HashSet<UserInRole>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(250)]
        public string LastName { get; set; }
        [StringLength(250)]
        public string ImageName { get; set; }
        [StringLength(250)]
        public string PhoneNumber { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        [Required]
        [StringLength(250)]
        public string Email { get; set; }
        [Required]
        [StringLength(250)]
        public string Username { get; set; }

        [JsonIgnore]
        [Required]
        [MaxLength(128)]
        public byte[] PasswordHash { get; set; }

        [JsonIgnore]
        [Required]
        [MaxLength(128)]
        public byte[] PasswordSalt { get; set; }

        [JsonIgnore]
        public bool IsAdmin { get; set; }

        [JsonIgnore]
        public bool IsEmployee { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }


        public string user_firebase_token { get; set; }
        public virtual ICollection<UserInRole> userRoles { get; set; }

    }
}
