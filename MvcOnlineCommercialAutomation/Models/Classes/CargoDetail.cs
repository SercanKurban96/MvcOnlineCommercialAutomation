using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineCommercialAutomation.Models.Classes
{
    public class CargoDetail
    {
        [Key]
        public int CargoDetailID { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(300)]
        public string Description { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string TrackingCode { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Employee { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Receiver { get; set; }
        public DateTime CargoDate { get; set; }
    }
}