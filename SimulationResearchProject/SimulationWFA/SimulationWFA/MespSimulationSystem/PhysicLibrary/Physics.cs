﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using SimulationSystem.ECSComponents;

namespace PhysicLibrary
{
    public static class Physics
    {
        private static int countOfPhysicActivity=0;
        private static float increasedTime = 0;

        public static Vector3 Gravity =  new Vector3(0,-10f,0);
        public static int DecimalPrecision = 2;

        public static List<Entity> colliderEntityList = new List<Entity>();

        public static int CalculatePhyicsLoopCount(float deltaTime,float fixedTime)
        {
            increasedTime += deltaTime;
            countOfPhysicActivity = (int)(increasedTime / fixedTime);
            increasedTime -= (countOfPhysicActivity * fixedTime);
            return countOfPhysicActivity;
        }


        public static bool Raycast(Ray ray,float distance,out Entity hit, bool isInfinite = false)
        {
            var smallestDistance = float.MaxValue;
            hit = Entity.Null;

            for(int i = 0; i < colliderEntityList.Count; i++)
            {
                ref var colliderComp = ref colliderEntityList[i].GetComponent<ColliderComp>();

                if(colliderComp.collider.IsIntersectWith(ray, distance,out Vector3 hitPoint, isInfinite))
                {

                    var tempDist = (hitPoint - ray.origin).Length();

                    if(tempDist < smallestDistance)
                    {
                        smallestDistance = tempDist;
                        hit = colliderEntityList[i];
                    }
                }
            }

            if(hit == Entity.Null)
            {
                return false;
            }

            return true;


        }

    }
}
