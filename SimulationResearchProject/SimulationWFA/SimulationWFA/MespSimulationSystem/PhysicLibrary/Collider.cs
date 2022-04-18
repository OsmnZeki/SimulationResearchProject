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
        public Bounds bound;
        public bool isTrigger;

        public abstract bool IsIntersectWith(Bounds bound, out Contact contact);
        public abstract void Update(Vector3 centerPos);
        public abstract void DrawGizmos();
    }
}
