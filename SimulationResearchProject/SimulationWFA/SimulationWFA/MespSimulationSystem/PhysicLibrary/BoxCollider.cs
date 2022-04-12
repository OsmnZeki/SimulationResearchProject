using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MespSimulationSystem.Math;
using ProgramLibrary;

namespace PhysicLibrary
{
    public enum BoxColliderNormalDirection
    {
        UP,
        RIGHT,
        DOWN,
        LEFT,
        FORWARD,
        BACKWARD,
    };

    public class BoxCollider : Collider
    { 

        public static Vector3[] potentialNormals = new Vector3[] {

            new Vector3(0,1,0),
            new Vector3(1,0,0),
            new Vector3(0,-1,0),
            new Vector3(-1,0,0),
            new Vector3(0,0,1),
            new Vector3(0,0,-1),

        };

        public BoxCollider()
        {
            bound = new BoxBounds();
        }

        public override bool IsIntersectWith(Bounds bounds, out Contact contact)
        {
            contact = new Contact();
            var result = this.bound.IsIntersectWith(bound);

            if (!result) return result;

            if (bound.boundType == BoundType.Box)
            {
                BoxBounds otherBound = bound as BoxBounds;

                //var distanceVec = this.bound.Center - bound.Center;
                var differenceVector = otherBound.Center - bound.Center;
                var normalName = GetNormal(differenceVector);
                contact.contactNormal = potentialNormals[(int)normalName];

                //contact.penetration = (bound as SphereBounds).radius + otherBound.radius - distanceVec.Length();
                return true;
            }



            return result;
        }

        BoxColliderNormalDirection GetNormal(Vector3 target)
        {
            float max = 0.0f;
            int best_match = -1;
            for (int i = 0; i <6; i++)
            {
                float dot_product = Vector3.Dot(target.normalized(), potentialNormals[i]);
                if (dot_product > max)
                {
                    max = dot_product;
                    best_match = i;
                }
            }
            return (BoxColliderNormalDirection)best_match;
        }

        public override void Update(Vector3 centerPos)
        {
            bound.UpdateCenter(centerPos);
            bound.UpdateBounds();
        }

        public override void DrawGizmos()
        {
            var color = new Vector3(0, 1, 0);
            MespDebug.DrawWireBox((bound as BoxBounds), color);
        }
    }
}
