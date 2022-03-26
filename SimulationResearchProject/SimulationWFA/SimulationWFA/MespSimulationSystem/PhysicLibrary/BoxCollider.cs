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
        public BoxBounds bounds;
        public BoxCollider()
        {
            bounds = new BoxBounds();
        }

    }
}
