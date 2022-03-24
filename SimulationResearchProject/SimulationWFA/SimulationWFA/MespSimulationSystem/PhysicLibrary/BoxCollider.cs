using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhysicLibrary
{
    public class BoxCollider : Collider
    {
        public Vector3 center;
        public Vector3 size = Vector3.One;
    }
}
