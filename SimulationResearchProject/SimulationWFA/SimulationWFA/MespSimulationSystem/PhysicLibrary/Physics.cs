// ref from https://www.toptal.com/game/video-game-physics-part-i-an-introduction-to-rigid-body-dynamics

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PhysicLibrary
{
    public class Physics
    {
        private int countOfPhysicActivity=0;
        private float increasedTime = 0;

        public static float Gravity = -9.81f;
        public static float NewtonFactor = 100f;

        public int CalculatePhyicsLoopCount(float deltaTime,float fixedTime)
        {
            increasedTime += deltaTime;
            countOfPhysicActivity = (int)(increasedTime / fixedTime);
            increasedTime -= (countOfPhysicActivity * fixedTime);
            return countOfPhysicActivity;
        }

        public static Vector3 ComputeGravityForce(Rigidbody rigidbody)
        {
            return new Vector3(0, rigidbody.mass * Gravity, 0);
        }

        public static void Simulate(Rigidbody rigidbody, float time)
        {
            var linearAcceleration = rigidbody.totalForce / rigidbody.mass;
            rigidbody.velocity += linearAcceleration * time;
            rigidbody.position += rigidbody.velocity*time;
            rigidbody.totalForce = Vector3.Zero;


            var anqularAcceleration = rigidbody.totalTorque / rigidbody.inertiaTensor;
            rigidbody.angularVelocity += anqularAcceleration * time;
            rigidbody.rotation += rigidbody.angularVelocity * time;
            rigidbody.totalTorque = Vector3.Zero;
        }

    }
}
