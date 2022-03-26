using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhysicLibrary
{
    public class BoxBounds : Bounds
    {
        // explanation : Vector2.x means minX, Vector2.y means maxX;

        private Vector3 size = Vector3.One;

        public Vector3 Size {
            get {
                return size;
            }
            set {
                size = value;
                UpdateBounds();
            }
        }

        public Vector2 xPoints;
        public Vector2 yPoints;
        public Vector2 zPoints;

        public BoxBounds()
        {
            this.boundType = BoundType.Box;
        }

        public override void UpdateBounds()
        {
            xPoints.X = Center.X - size.X;
            xPoints.Y = Center.X + size.X;

            yPoints.X = Center.Y - size.Y;
            yPoints.Y = Center.Y + size.Y;

            zPoints.X = Center.Z - size.Z;
            zPoints.Y = Center.Z + size.Z;
        }

        //AABB
        public override bool IsIntersectWith(Vector3 point)
        {
            return true;
        }

        public override bool IsIntersectWith(Bounds bound)
        {
            return true;
        }

        
    }
}
