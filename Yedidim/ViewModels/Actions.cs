using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedideyChabad.ViewModels
{
    public class ActionsMenu
    {
        public bool UseruName { get; set; }
        public bool CollapseExpandDetails { get; set; }
        public bool ShowChildBreadcrumb { get; set; }
        public bool ShowSubmit { get; set; }

        public ActionsMenu()
        {
            this.UseruName = false;
            this.CollapseExpandDetails = false;
            this.ShowChildBreadcrumb = false;
            this.ShowSubmit = false;
        }
    }
}