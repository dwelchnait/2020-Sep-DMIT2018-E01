using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChinookSystem.ViewModels
{
    public class SelectionList
    {
        //use this class to carry data for a drop down list
        //a drop down list needs a value and a display string
        public int ValueId { get; set; }
        public string DisplayText { get; set; }
    }
}
