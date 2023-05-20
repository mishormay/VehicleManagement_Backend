using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleManagement.Entities
{
    public partial class AppSettings
    {
        [Key]
        public int Id { get; set; }
        [StringLength(250)]
        public string Email { get; set; }
        [StringLength(250)]
        public string Website { get; set; }
        [StringLength(50)]
        public string AppVersion { get; set; }
        [Column("AboutUS")]
        public string AboutUs { get; set; }
        public string HeaderImages { get; set; }
        public string PrivacyPolicy { get; set; }
        public string UserTerms { get; set; }
        [StringLength(250)]
        public string FacebookUrl { get; set; }
        [StringLength(250)]
        public string TwitterUrl { get; set; }
        [StringLength(250)]
        public string YoutubeUrl { get; set; }
        [StringLength(250)]
        public string InstagramUrl { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
    }
}
