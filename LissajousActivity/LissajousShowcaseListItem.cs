using NotImplementedLab.Controls;
using NotImplementedLab.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LissajousActivity
{
    public class LissajousShowcaseListItem : ShowcaseListItem
    {
        public LissajousShowcaseListItem() : base("Lissajous", "", new LissajousPlayground(), new LissajousInfo())
        { }
    }
}
