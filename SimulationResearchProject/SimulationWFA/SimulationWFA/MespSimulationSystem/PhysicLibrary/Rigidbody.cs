using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using SimulationSystem.Timer;

namespace PhysicLibrary
{
    public class Rigidbody
    {
        public Vector3 velocity;
        public Vector3 angularVelocity;
        public float mass;

        public bool useGravity;

        public Vector3 position;
        public Vector3 rotation;
        public Vector3 inertiaTensor = Vector3.One;

        public Vector3 linearAcceleration;
        public Vector3 angularAcceleration;

        public void AddTorque(Vector3 torque)
        {
            torque *= Physics.NewtonFactor;
            angularAcceleration += torque / inertiaTensor;
        }
        public void AddForce(Vector3 force)
        {
            force *= Physics.NewtonFactor;
            linearAcceleration += force / mass;
        }
    }
}
