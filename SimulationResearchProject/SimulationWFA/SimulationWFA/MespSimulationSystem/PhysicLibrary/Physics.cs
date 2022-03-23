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

        public int CalculatePhyicsLoopCount(float deltaTime,float fixedTime)
        {
            increasedTime += deltaTime;
            countOfPhysicActivity = (int)(increasedTime / fixedTime);
            increasedTime -= (countOfPhysicActivity * fixedTime);
            return countOfPhysicActivity;
        }

        private static Vector3 ComputeGravityForce(ref Rigidbody rigidbody)
        {
            return new Vector3(0, rigidbody.mass * Gravity, 0);
        }

        public static void Simulate(ref Rigidbody rigidbody, float time)
        {
            Vector3 gravityForce = ComputeGravityForce(ref rigidbody);
            Vector3 acceleration = gravityForce / rigidbody.mass;
            rigidbody.velocity += acceleration * time;
            rigidbody.position += rigidbody.velocity*time;
        }

    }
}
