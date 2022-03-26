using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using PhysicLibrary;
using ProgramLibrary;
using RenderLibrary.IO;
using SimulationSystem.ECSComponents;

namespace SimulationSystem
{
    public class CollisionDetectionSystem : Dalak.Ecs.System
    {
        readonly Filter<RigidbodyComp, BoxColliderComp> checkingFilter = null;
        readonly Filter<BoxColliderComp> boxColliderFilter = null;


        public override void Update()
        {
           
        }

        public override void FixedUpdate()
        {
            foreach(var c in checkingFilter)
            {
                ref var rigidbodyComp = ref checkingFilter.Get1(c);
                ref var checkingBoxColliderComp = ref checkingFilter.Get2(c);

                var checkingEntity = checkingFilter.GetEntity(c);
                

                foreach (var b in boxColliderFilter)
                {
                    var otherEntity = boxColliderFilter.GetEntity(b);
                    if (checkingEntity == otherEntity) continue;

                    ref var otherBoxColliderComp = ref boxColliderFilter.Get1(b);

                    if (checkingBoxColliderComp.boxCollider.IsIntersectWith(otherBoxColliderComp.boxCollider.bounds,out var contactData))
                    {
                        MespDebug.DrawLine(otherBoxColliderComp.boxCollider.bounds.Center, otherBoxColliderComp.boxCollider.bounds.Center + contactData.normal, new Vector3(0, 0, 0));
                    }
                    else
                    {
                        Console.WriteLine("Carpisma Olmadi");
                    }
                }
            }
        }

    }
}
