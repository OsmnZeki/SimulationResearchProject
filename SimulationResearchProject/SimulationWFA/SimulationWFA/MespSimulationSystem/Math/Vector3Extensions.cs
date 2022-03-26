using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MespSimulationSystem.Math
{
    public static class Vector3Extensions
    {
        public static void Normalize(this Vector3 vect)
        {
            vect = vect / vect.Length();
        }

        public static Vector3 Normalized(this Vector3 vect)
        {
            return vect / vect.Length();
        }
    }
}
