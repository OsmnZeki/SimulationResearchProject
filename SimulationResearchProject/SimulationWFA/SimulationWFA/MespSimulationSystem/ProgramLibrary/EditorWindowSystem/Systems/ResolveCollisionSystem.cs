using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using PhysicLibrary;
using SimulationSystem.ECSComponents;

namespace SimulationSystem
{
    public class ResolveCollisionSystem : Dalak.Ecs.System
    {
        readonly Filter<ContactHolderComp> contactHolderFilter = null;
        readonly Filter<ContactResolverComp> contactResolverFilter = null;


        public override void Awake()
        {
            var entity = world.NewEntity();
            entity.AddComponent<ContactResolverComp>() = new ContactResolverComp() {
                contactResolver = new ContactResolver(),
            };
        }

        public override void FixedUpdate()
        {
            foreach(var h in contactHolderFilter)
            {
                foreach (var r in contactResolverFilter)
                {
                    ContactResolver contactResolver = contactResolverFilter.Get1(r).contactResolver;
                    var contactlist = contactHolderFilter.Get1(h).contactList;

                    int usedContacts = contactlist.Count;
                    contactResolver.iteration = usedContacts * 2;
                    contactResolver.ResolveContact(contactlist.ToArray(), usedContacts, Timer.Time.fixedDeltaTime);
                }
            }

            
            
        }

    }
}
