using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry.TwoPointsOnSphere
{
    public class RotationInfo
    {
        public char Axis;
        public double Value;
        public RotationInfo(char axis, double value)
        {
            Axis = axis;
            Value = value;
        }
    }
}
