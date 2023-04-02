using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcOnlineCommercialAutomation.Models.Classes
{
    public class SalesTransaction
    {
        [Key]
        public int SalesID { get; set; }
        //ürün
        //cari
        //personel
        public DateTime SalesDate { get; set; }
        public int Piece { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }

        public int Productid { get; set; }
        public int Currentid { get; set; }
        public int Employeeid { get; set; }

        public virtual Product Product { get; set; }
        public virtual Current Current { get; set; }
        public virtual Employee Employee { get; set; }
    }
}