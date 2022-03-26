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
            xPoints.X = Center.X - size.X/2;
            xPoints.Y = Center.X + size.X/2;
                                         
            yPoints.X = Center.Y - size.Y/2;
            yPoints.Y = Center.Y + size.Y/2;
                                         
            zPoints.X = Center.Z - size.Z/2;
            zPoints.Y = Center.Z + size.Z/2;
        }

        //AABB
        public override bool IsIntersectWith(Vector3 point)
        {
            return (point.X >= xPoints.X && point.X <= xPoints.Y) &&
                    (point.Y >= yPoints.X && point.Y <= yPoints.Y) &&
                    (point.Z >= zPoints.X && point.Z <= zPoints.Y);
        }

        public override bool IsIntersectWith(Bounds bound)
        {
            if(bound.boundType == BoundType.Box)
            {
                BoxBounds boxBound = bound as BoxBounds;
                return (boxBound.xPoints.X <= xPoints.Y && boxBound.xPoints.Y >= xPoints.X) &&
                        (boxBound.yPoints.X <= yPoints.Y && boxBound.yPoints.Y >= yPoints.X) &&
                        (boxBound.zPoints.X <= zPoints.Y && boxBound.zPoints.Y >= zPoints.X);
            }

            return false;
        }

        
    }
}
