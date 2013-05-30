using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace YedideyChabad.Models
{
    public class YadidPartner
    {
        [MaxLength(50)]
        [Display(Name = "שם בן/בת זוג")]
        public string PartnerFirstName { get; set; }

        [MaxLength(50)]
        [Display(Name = "שם משפחה")]
        public string PartnerLastName { get; set; }
        
        //public int? PartnerGender { get; set; }        
        [Display(Name = "שם הורה")]
        public string PartnerDescendantName { get; set; }
        
        [Display(Name = "תאריך לידה עברי")]
        public string PartnerBirthday_Jewish { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "תאריך לידה לועזי")]
        public DateTime? PartnerBirthday_Greg { get; set; }
        
        [Display(Name = "מס' נייד")]
        public int? PartnerPhoneCellular { get; set; }
        
        [Display(Name = "דואר אלקטרוני")]
        public string PartnerEmail { get; set; }
        
        [Display(Name = "תפקיד בעבודה")]
        public string PartnerPositionAtWork { get; set; }
        
        [Display(Name = "תחביב")]
        public string PartnerHobby { get; set; }
        
        [Display(Name = "אישיות")]
        public string PartnerPersonalty { get; set; }
         
        [Display(Name = "הערות")]
        public string PartnerComments { get; set; }
    }
}