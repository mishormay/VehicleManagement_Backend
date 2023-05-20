using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VehicleManagement.Entities
{
    public class VehicleManufacturer
    {
        public VehicleManufacturer()
        {
            _vehicleModels = new HashSet<VehicleModel>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
     
        public virtual ICollection<VehicleModel> _vehicleModels { get; set; }
       
    }
}
