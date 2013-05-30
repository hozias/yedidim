using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

namespace YedideyChabad.Models
{
    public class Child
    {
        [Key]
        public ObjectId _id { get; set; }

        public ObjectId? OwnerUnique { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "שם פרטי")]
        public string Name { get; set; }
        
        [Display(Name = "תאריך לידה עברי")]
        public string Birthday_Jewish { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "תאריך לידה לועזי")]
        public DateTime? Birthday_Greg { get; set; }
        
        [Display(Name = "גיל")]
        public int? Age { get; set; }
        
        [Display(Name = "הערות")]
        public string Comments { get; set; }
        public ObjectId YedidId { get; set; }

    }
}