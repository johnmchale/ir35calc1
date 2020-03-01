using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ir35calc1.Models
{
    public class DayRate
    {

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(200, 2000)]
        [Display(Name = "''Daily Rate''", Prompt = "enter amount")]
        public Decimal? Amount { get; set; }

    }
}
