﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineCommercialAutomation.Models.Classes
{
    public class Current
    {
        [Key]
        public int CurrentID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string CurrentName { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string CurrentSurname { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(14)]
        public string CurrentCity { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string CurrentMail { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string CurrentPassword { get; set; }
        public bool CurrentStatus { get; set; }

        public ICollection<SalesTransaction> SalesTransactions { get; set; }
    }
}