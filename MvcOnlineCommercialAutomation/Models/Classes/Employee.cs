using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineCommercialAutomation.Models.Classes
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        [Display(Name = "Personel Adı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string EmployeeName { get; set; }

        [Display(Name = "Personel Soyadı")]
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string EmployeeSurname { get; set; }

        [Display(Name = "Görsel")]
        [Column(TypeName = "Varchar")]
        [StringLength(250)]
        public string EmployeeImage { get; set; }

        [Display(Name = "Personel Hakkında")]
        [Column(TypeName = "Varchar")]
        [StringLength(500)]
        public string EmployeeDetail { get; set; }

        [Display(Name = "Personel Adres")]
        [Column(TypeName = "Varchar")]
        [StringLength(200)]
        public string EmployeeAddress { get; set; }

        [Display(Name = "Personel Telefon")]
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string EmployeePhone { get; set; }

        public ICollection<SalesTransaction> SalesTransactions { get; set; }
        public int Departmentid { get; set; }
        public virtual Department Department { get; set; }
    }
}