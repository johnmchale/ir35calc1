using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;

namespace ir35calc1.Models
{
    public class ContractorGroup
    {

        [Column(TypeName = "decimal(18, 2)")]
        public DayRate DayRateAmount { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public Contractor Yearly { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public Contractor Monthly { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public Contractor Weekly { get; set; }
        
    }
}
