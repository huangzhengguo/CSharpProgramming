using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter9
{
    struct Vector : IFormattable
    {
        public double x, y, z;

        public Vector(double x,double y,double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
            {
                return ToString();
            }

            string formatUpper = format.ToUpper();
            switch (formatUpper)
            {
                case "N":
                    return "||" + Norm().ToString() + "||";
                case "VE":
                    return string.Format("({0:E}, {1:E}, {2:E})", x, y, z);
                case "IJK":
                    StringBuilder sb = new StringBuilder(x.ToString(), 30);
                    sb.AppendFormat("i + ");
                    sb.AppendFormat(y.ToString());
                    sb.AppendFormat("j + ");
                    sb.AppendFormat(z.ToString());
                    sb.AppendFormat("k");

                    return sb.ToString();
                default:
                    return ToString();
            }
        }

        public override string ToString()
        {
            return string.Format("({0},{1},{2})", x.ToString(), y.ToString(), z.ToString());
        }

        public double Norm()
        {
            return x * x + y * y + z * z;
        }
    }
}
