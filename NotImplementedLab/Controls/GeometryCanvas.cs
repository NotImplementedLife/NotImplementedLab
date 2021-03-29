using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using NotImplementedLab.Data;
using NotImplementedLab.Data.GeometryEntities;

namespace NotImplementedLab.Controls
{
    public class GeometryCanvas : Canvas
    {
        public GeometryCanvas() : base()
        {
            Background = Brushes.Snow;
            Loaded += GeometryCanvas_Loaded;            
        }        

        public List<GeometryEntity> Entities = new List<GeometryEntity>();

        private void GeometryCanvas_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            for (int i = 0, cnt = Entities.Count; i < cnt; i++)
                if (!Entities[i].Initialized)
                {
                    Entities[i].Initialize(this);
                    Entities[i].Initialized = true;
                }
        }
    }
}
