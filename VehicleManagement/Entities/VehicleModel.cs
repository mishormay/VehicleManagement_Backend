using System.ComponentModel.DataAnnotations;

namespace VehicleManagement.Entities
{
    public class VehicleModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ModelYear { get; set; }
        public int ManufacturerId { get; set; }
        public virtual VehicleManufacturer Manufacturer { get; set; }
    }
}
