using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusalaApi.Model
{
    public class Gateway
    {
        [Key]
        [StringLength(20)]
        [Required]
        public string SerialNumber { get; set; }
        [Required(ErrorMessage = "You should provide a Name.")]
        [StringLength(50)]
        public string Name { get; set; }
        [RegularExpression( @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b",ErrorMessage ="You should provide a valid IP address.")]
        [StringLength(15)]
        public string IPAddress { get; set; }
        public ICollection<Device> Devices { get; set; } = new List<Device>();
    }
}
