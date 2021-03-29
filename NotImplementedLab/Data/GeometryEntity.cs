using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using NotImplementedLab.Controls;

namespace NotImplementedLab.Data
{
    public abstract class GeometryEntity
    {
        internal FrameworkElement _fwe;
        internal TextBlock _lbl;
        internal List<GeometryEntity> Dependants = new List<GeometryEntity>();
        public abstract void Initialize(GeometryCanvas canvas);
        public virtual void Update() { }
        public bool Initialized=false;
    }
}
