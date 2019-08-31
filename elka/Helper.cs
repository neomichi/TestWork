using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace ConsoleApplication1
{
    public static class Helper 
    {
        public static double GetLen(Vector3d v1, Vector3d v2)
        {
            var len = Math.Sqrt(
                (v1.X - v2.X) * (v1.X - v2.X)
                + (v1.Y - v2.Y) * (v1.Y - v2.Y)
                + (v1.Z - v2.Z) * (v1.Z - v2.Z));
            return len;
        }

        public static Vector3d GetCenterLine(Vector3d v1, Vector3d v2,double center=2.0)
        {
            return new Vector3d()
            {
                X = (v1.X + v2.X) / center,
                Y = (v1.Y + v2.Y) / center,
                Z = (v1.Z + v2.Z) / center,
            };
        }


        public static Vector3d Plus(Vector3d v1, double c)
        {
            v1.X += c;
            v1.Y += c;
            v1.Z += c;
            return v1;
        }

        public static Vector3d Proz(Vector3d v1, double c)
        {
            v1.X *= c;
            v1.Y *= c;
            v1.Z *= c;
            return v1;
        }


        public static Vector3d GetTurn(Vector3d v1, int angle)
        {
            var v2 = new Vector3d
            {
                X = v1.Z*Math.Cos(angle) - v1.Z*Math.Sin(angle),
                Z = v1.Z*Math.Sin(angle) + v1.Z*Math.Cos(angle)
            };
            return v2;
        }

    }
}
