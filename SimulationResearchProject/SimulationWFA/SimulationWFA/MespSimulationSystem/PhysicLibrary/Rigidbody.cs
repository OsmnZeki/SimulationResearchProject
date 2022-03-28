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
        public float restitution = 1f;

        public bool useGravity = true;

        public Vector3 position;
        public Vector3 rotation;
        public Vector3 inertiaTensor = Vector3.One;

        public Vector3 totalForce;
        public Vector3 totalTorque;

        public float InvMass()
        {
            return 1 / mass;
        }

        public void ApplyImpulse(Vector3 impulse)
        {
            totalForce += (impulse / Time.fixedDeltaTime);
        }

        public void AddTorque(Vector3 torque)
        {
            torque *= Physics.NewtonFactor;
            totalTorque += torque;
        }
        public void AddForce(Vector3 force)
        {
            force *= Physics.NewtonFactor;
            totalForce += force;
        }

        public Vector3 GetCurrentVelocity()
        {
            var linearAcceleration = totalForce / mass;
            return velocity + linearAcceleration * Time.fixedDeltaTime;
        }
    }
}
