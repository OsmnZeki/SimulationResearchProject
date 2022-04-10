using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MespSimulationSystem.Math;

namespace PhysicLibrary
{
    public class SphereCollider : Collider
    {
        public SphereBounds sphereBound;

        public SphereCollider()
        {
            sphereBound = new SphereBounds();
        }

        public override bool IsIntersectWith(Bounds bound, out Contact contact)
        {
            contact = new Contact();
            var result = this.sphereBound.IsIntersectWith(bound);

            if (!result) return result;

            var distanceVec = this.sphereBound.Center - bound.Center;
            contact.contactNormal = distanceVec.normalized();
            SphereBounds otherBound = bound as SphereBounds;
            contact.penetration = sphereBound.radius + otherBound.radius - distanceVec.Length();

            return result;
        }
    }
}
