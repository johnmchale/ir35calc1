using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ir35calc1.Models
{
    public class Contractor
    {
        [Column(TypeName = "decimal(18, 0)")]
        public Decimal GrossPay { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public Decimal EmployersNi { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public Decimal EmployeesNi { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public Decimal EmployeesNiBandOne { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public Decimal EmployeesNiBandTwo { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public Decimal EmployeesNiBandThree { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public Decimal GeneralExpenses { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public Decimal DeemedAmount { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public Decimal Paye { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public Decimal PayeBandOne { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public Decimal PayeBandTwo { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public Decimal PayeBandThree { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public Decimal TakeHomePay { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public Decimal TakeHomePayPerc { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public Decimal TotalTaxPaid { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public Decimal TotalTaxPaidPerc { get; set; }
        

    }
}
