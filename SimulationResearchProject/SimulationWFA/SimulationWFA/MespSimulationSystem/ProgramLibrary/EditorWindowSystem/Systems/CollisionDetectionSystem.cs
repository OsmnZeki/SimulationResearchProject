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
        readonly Filter<ParticleComp, BoxColliderComp> boxRigidFilter = null;
        readonly Filter<BoxColliderComp>.Exclude<ParticleComp> onlyBoxColliderFilter = null;

        readonly Filter<ParticleComp, SphereColliderComp> sphereRigidFilter = null;
        readonly Filter<SphereColliderComp>.Exclude<ParticleComp> onlySphereColliderFilter = null;


        List<Contact> contactList = new List<Contact>();
        public override void Awake()
        {
            var contactHolderEntity = world.NewEntity();
            contactHolderEntity.AddComponent<ContactHolderComp>() = new ContactHolderComp() {

                contactList = contactList,
            };
        }

        public override void FixedUpdate()
        {
            contactList.Clear();

           /* foreach(var s in sphereRigidFilter)
            {
                ref var particleComp = ref sphereRigidFilter.Get1(s);
                ref var sphereColliderComp = ref sphereRigidFilter.Get2(s);                

                foreach (var o in onlySphereColliderFilter)
                {
                    ref var otherSphereColliderComp = ref onlySphereColliderFilter.Get1(o);

                    if (sphereColliderComp.sphereCollider.IsIntersectWith(otherSphereColliderComp.sphereCollider.sphereBound,out var contact))
                    {
                        contact.particles[0] = particleComp.particle;
                        contact.particles[1] = null;

                        contact.restitution = Math.Min(sphereColliderComp.sphereCollider.restitution, otherSphereColliderComp.sphereCollider.restitution);
                        contactList.Add(contact);
                    }
                }
            }*/

            for(int i = 0; i < sphereRigidFilter.NumberOfEntities-1; i++)
            {
                ref var particleComp = ref sphereRigidFilter.Get1(i);
                ref var sphereColliderComp = ref sphereRigidFilter.Get2(i);

                for(int j=i+1; j < sphereRigidFilter.NumberOfEntities; j++)
                {
                    ref var otherParticleComp = ref sphereRigidFilter.Get1(j);
                    ref var otherSphereColliderComp = ref sphereRigidFilter.Get2(j);


                    if (sphereColliderComp.sphereCollider.IsIntersectWith(otherSphereColliderComp.sphereCollider.sphereBound, out var contact))
                    {
                        contact.particles[0] = particleComp.particle;
                        contact.particles[1] = otherParticleComp.particle;

                        contact.restitution = Math.Min(sphereColliderComp.sphereCollider.restitution, otherSphereColliderComp.sphereCollider.restitution);
                        contactList.Add(contact);
                    }
                }
            }
        }

    }
}
