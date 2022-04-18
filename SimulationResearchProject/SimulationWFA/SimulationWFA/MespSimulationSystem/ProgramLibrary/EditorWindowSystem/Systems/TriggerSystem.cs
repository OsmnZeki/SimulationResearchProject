using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using SimulationSystem.ECSComponents;

namespace SimulationSystem
{
    public class TriggerSystem : Dalak.Ecs.System
    {
        readonly Filter<ColliderComp> colliderFilter = null;

        readonly Filter<ParticleComp, ColliderComp, TriggerComp> triggerFilter = null;
        readonly Filter<ColliderComp>.Exclude<TriggerComp> nonTriggerFilter = null;

        public override void Awake()
        {
            foreach(var c in colliderFilter)
            {
                ref var colliderComp = ref colliderFilter.Get1(c);
                if (!colliderComp.collider.isTrigger) continue;

                var entity = colliderFilter.GetEntity(c);
                entity.AddComponent<TriggerComp>() = new TriggerComp() {
                    collidedEntityList = new List<Entity>(),
                };

            }
        }

        public override void FixedUpdate()
        {
            foreach(var t in triggerFilter)
            {

                foreach(var n in nonTriggerFilter)
                {

                }
            }
        }

    }
}
