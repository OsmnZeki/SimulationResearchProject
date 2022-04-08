using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhysicLibrary
{
    public abstract class Collider
    {
        public float restitution = 1f;

        public abstract bool IsIntersectWith(Bounds bound, out Contact contact);
    }
}
