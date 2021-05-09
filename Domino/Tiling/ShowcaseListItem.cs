using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domino.Tiling
{
    public class ShowcaseListItem : NotImplementedLab.Data.ShowcaseListItem
    {
        public ShowcaseListItem() : base(
           "Domino Tiling Problem", "",                  
           typeof(Playground),   
           null           
           )
        { }
    }
}
