using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedideyChabad.ViewModels
{
    public class SideMenu
    {
        public bool StartPage { get; set; }
        public bool AddMember { get; set; }
        public bool AllMembers { get; set; }
        public bool Member { get; set; }
        public bool Children { get; set; }
        public bool AddChild { get; set; }

        public SideMenu()
        {
            this.StartPage = true;
            this.AddMember = true;
            this.AllMembers = false;
            this.Member = false;
            this.Children = false;
            this.AddChild = false;
        }
    }
}