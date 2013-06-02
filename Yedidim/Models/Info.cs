using System.ComponentModel.DataAnnotations;

namespace YedideyChabad.Models
{
    public class Info
    {
        [Display(Name = "ידידם")]
        public int? Members { get; set; }
        [Display(Name = "מנהלים")]
        public int? Users { get; set; }
    }
}