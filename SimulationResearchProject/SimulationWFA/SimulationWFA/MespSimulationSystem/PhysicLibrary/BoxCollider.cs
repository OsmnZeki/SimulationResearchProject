using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MespSimulationSystem.Math;

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
        public BoxBounds bounds;
        

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
            bounds = new BoxBounds();
        }

        public override bool IsIntersectWith(Bounds bounds, out Contact contact)
        {
            contact = new Contact();
            var result =  this.bounds.IsIntersectWith(bounds);

            if (!result) return result;


            //TODO:: contact
            return result;
        }

        BoxColliderNormalDirection GetNormal(Vector3 target)
        {
            float max = 0.0f;
            int best_match = -1;
            for (int i = 0; i <6; i++)
            {
                float dot_product = Vector3.Dot(target.Normalized(), potentialNormals[i]);
                if (dot_product > max)
                {
                    max = dot_product;
                    best_match = i;
                }
            }
            return (BoxColliderNormalDirection)best_match;
        }

    }
}
