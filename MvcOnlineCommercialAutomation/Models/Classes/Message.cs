using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOnlineCommercialAutomation.Models.Classes
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string MessageSender { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string MessageReceiver { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string MessageSubject { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(2000)]
        public string MessageContent { get; set; }

        [Column(TypeName = "Smalldatetime")]
        public DateTime MessageDate { get; set; }
    }
}