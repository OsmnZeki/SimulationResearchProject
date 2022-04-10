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

        public static Vector3 Gravity =  new Vector3(0,-10f,0);
        public static int DecimalPrecision = 2;
        public int CalculatePhyicsLoopCount(float deltaTime,float fixedTime)
        {
            increasedTime += deltaTime;
            countOfPhysicActivity = (int)(increasedTime / fixedTime);
            increasedTime -= (countOfPhysicActivity * fixedTime);
            return countOfPhysicActivity;
        }


        public static void Simulate(Particle rigidbody, float time)
        {
          /*  var linearAcceleration = rigidbody.totalForce / rigidbody.mass;
            rigidbody.velocity += linearAcceleration * time;
            rigidbody.position += rigidbody.velocity*time;
            rigidbody.totalForce = Vector3.Zero;


            var anqularAcceleration = rigidbody.totalTorque / rigidbody.inertiaTensor;
            rigidbody.angularVelocity += anqularAcceleration * time;
            rigidbody.rotation += rigidbody.angularVelocity * time;
            rigidbody.totalTorque = Vector3.Zero;*/
        }

    }
}
