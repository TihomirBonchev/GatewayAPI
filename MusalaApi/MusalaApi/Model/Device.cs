using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusalaApi.Model
{
    public class Device
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeviceId { get; set; }

        [Required(ErrorMessage ="You should provice a Vendor.")]
        [StringLength(50)]
        public string Vendor { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "You should provice a Date.")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "You should provice a Status.")]
        [RegularExpression("(online|offline)", ErrorMessage ="Status allow values are online and offline")]
        [StringLength(7)]
        public string Status { get; set; }

        public Gateway Gateway { get; set; }

        [ForeignKey("SerialNumber")]
        [StringLength(20)]
        public string SerialNumber { get; set; }
    }
}
