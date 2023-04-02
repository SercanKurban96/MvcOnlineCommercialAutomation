﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOnlineCommercialAutomation.Models.Classes
{
    public class ToDoList
    {
        [Key]
        public int ToDoListID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string Heading { get; set; }

        [Column(TypeName = "bit")]
        public bool Status { get; set; }
    }
}