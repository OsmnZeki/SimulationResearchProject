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
        readonly Filter<RigidbodyComp, BoxColliderComp> rigidBodyFilter = null;
        readonly Filter<BoxColliderComp>.Exclude<RigidbodyComp> onlyBoxColliderFilter = null;


        public override void Update()
        {
           
        }

        public override void FixedUpdate()
        {
            foreach(var c in rigidBodyFilter)
            {
                ref var rigidbodyComp = ref rigidBodyFilter.Get1(c);
                ref var checkingBoxColliderComp = ref rigidBodyFilter.Get2(c);

                var checkingEntity = rigidBodyFilter.GetEntity(c);
                

                foreach (var b in onlyBoxColliderFilter)
                {
                    var otherEntity = onlyBoxColliderFilter.GetEntity(b);
                    if (checkingEntity == otherEntity) continue;

                    ref var otherBoxColliderComp = ref onlyBoxColliderFilter.Get1(b);

                    if (checkingBoxColliderComp.boxCollider.IsIntersectWith(otherBoxColliderComp.boxCollider.bounds,out var contactData))
                    {
                        Console.WriteLine("Carpisti");
                        contactData.rigidBody = rigidbodyComp.rigidbody;
                        contactData.otherRigidbody = null;

                        checkingBoxColliderComp.boxCollider.collisionContact = contactData;
                        otherBoxColliderComp.boxCollider.collisionContact = contactData;

                        MespDebug.DrawLine(otherBoxColliderComp.boxCollider.bounds.Center, otherBoxColliderComp.boxCollider.bounds.Center + contactData.normal, new Vector3(0, 0, 0));
                    }
                    else
                    {
                        Console.WriteLine("Carpisma Olmadi");
                    }
                }
            }

            for(int i = 0; i < rigidBodyFilter.NumberOfEntities; i++)
            {
                ref var rigidbodyComp = ref rigidBodyFilter.Get1(i);
                ref var checkingBoxColliderComp = ref rigidBodyFilter.Get2(i);

                var checkingEntity = rigidBodyFilter.GetEntity(i);

                for(int j=i+1; j < rigidBodyFilter.NumberOfEntities; j++)
                {
                    ref var otherRigidbodyComp = ref rigidBodyFilter.Get1(i);
                    ref var otherBoxColliderComp = ref rigidBodyFilter.Get2(i);

                    var otherEntity = rigidBodyFilter.GetEntity(i);

                    if (checkingBoxColliderComp.boxCollider.IsIntersectWith(otherBoxColliderComp.boxCollider.bounds, out var contactData))
                    {
                        Console.WriteLine("Carpisti");
                        contactData.rigidBody = rigidbodyComp.rigidbody;
                        contactData.otherRigidbody = otherRigidbodyComp.rigidbody;

                        checkingBoxColliderComp.boxCollider.collisionContact = contactData;
                        otherBoxColliderComp.boxCollider.collisionContact = contactData;

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
