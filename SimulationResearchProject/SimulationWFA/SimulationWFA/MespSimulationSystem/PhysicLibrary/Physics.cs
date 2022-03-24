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

        private static Vector3 ComputeGravityForce(Rigidbody rigidbody)
        {
            return new Vector3(0, rigidbody.mass * Gravity, 0);
        }

        public static void Simulate(Rigidbody rigidbody, float time)
        {
            if (rigidbody.useGravity)
            {
                Vector3 gravityForce = ComputeGravityForce(rigidbody);
                rigidbody.linearAcceleration += gravityForce / rigidbody.mass;
            }

            
            rigidbody.velocity += rigidbody.linearAcceleration * time;
            rigidbody.position += rigidbody.velocity*time;
            rigidbody.linearAcceleration = Vector3.Zero;

            rigidbody.angularVelocity += rigidbody.angularAcceleration * time;
            rigidbody.rotation += rigidbody.angularVelocity * time;
            rigidbody.angularAcceleration = Vector3.Zero;
        }

    }
}
