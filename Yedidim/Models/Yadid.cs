using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MongoDB.Bson;

namespace YedideyChabad.Models
{
    public class Yadid : YadidPartner
    {
        [Key]
        public ObjectId _id { get; set; }
        
        public ObjectId? OwnerUnique { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "שם פרטי")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "שם משפחה")]
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return LastName + " " + FirstName;
            }
        }
                
        //public int? Gender { get; set; }
                
        [MaxLength(50)]
        [Display(Name = "שם הורה")]
        public string DescendantName { get; set; }
                
        [MaxLength(50)]
        [Display(Name = "תאריך לידה עברי")]
        public string Birthday_Jewish { get; set; }

        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "תאריך לידה לועזי")]
        public DateTime? Birthday_Greg { get; set; }

        [Display(Name = "מס' נייד")]
        public int? PhoneCellular { get; set; }
        
        [MaxLength(50)]
        [Display(Name = "דואר אלקטרוני")]
        public string Email { get; set; }
        [Display(Name = "תפקיד בעבודה")]
        public string PositionAtWork { get; set; }
        [Display(Name = "תחביב")]
        public string Hobby { get; set; }
        [Display(Name = "אישיות")]
        public string Personalty { get; set; }
        [Display(Name = "הערות")]
        public string Comments { get; set; }

        [Display(Name = "תאריך נישואין לועזי")]
        public int? MerigeDate_Greg { get; set; }
        [Display(Name = "תאריך נישואין עברי")]
        public string MerigeDate_Jewish { get; set; }
        [Display(Name = "מס' ילדים")]
        public int? NumOfChildren { get; set; }
        [Display(Name = "מס' ילדים לפעילות")]
        public int? NumOfChildrenForActivities { get; set; }
        public virtual ICollection<Child> Children { get; set; }
        [Display(Name = "מסורת")]
        public string Tradition { get; set; }
        [Display(Name = "הערות")]
        public string FamilyComments { get; set; }
        [Display(Name = "רחוב")]
        public string Home_Street { get; set; }
        [Display(Name = "מספר בית")]
        public int? Home_Number { get; set; }
        [Display(Name = "כניסה")]
        public int? Home_Entrance { get; set; }
        [Display(Name = "מצב כלכלי")]
        public string Home_EconomicStatus { get; set; }
        [Display(Name = "הערות")]
        public string Home_Comments { get; set; }
        [Display(Name = "טלפון בית")]
        public int? Home_Phone { get; set; }
    }
}