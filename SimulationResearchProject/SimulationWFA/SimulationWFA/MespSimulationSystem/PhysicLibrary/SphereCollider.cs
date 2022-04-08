using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


            contact.contactNormal = this.sphereBound.Center - bound.Center;
            SphereBounds otherBound = bound as SphereBounds;
            contact.penetration = sphereBound.radius + otherBound.radius - contact.contactNormal.Length();

            return result;
        }
    }
}
