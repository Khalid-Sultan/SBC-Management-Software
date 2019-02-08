using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Xps.Packaging;

namespace SBCManagementSoftware
{
    public class XpsDetails
    {
        public XpsResource resource { get; set; }
        public Uri sourceURI { get; set; }
        public Uri destURI { get; set; }
    }
}
