using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MespSimulationSystem.Math;

namespace PhysicLibrary
{
    public class CollisionContact
    {
        public Rigidbody rigidBody;
        public Rigidbody otherRigidbody;
        private Vector3 normalRef;

        public Vector3 normal {
            get {
                return normalRef;
            }
            set {
                normalRef = value;
                normalRef.Normalize();
            }
        }

    }
}
