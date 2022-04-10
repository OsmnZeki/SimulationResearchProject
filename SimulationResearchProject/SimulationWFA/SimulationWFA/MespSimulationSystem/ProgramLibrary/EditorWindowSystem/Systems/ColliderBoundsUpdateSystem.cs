using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Dalak.Ecs;
using ProgramLibrary;
using SimulationSystem.Components;
using SimulationSystem.ECSComponents;

namespace SimulationSystem
{
    public class ColliderBoundsUpdateSystem : Dalak.Ecs.System
    {
        Filter<BoxColliderComp,TransformComp> boxColliderFilter = null;
        Filter<SphereColliderComp,TransformComp> sphereColliderFilter = null;

        public override void Awake()
        {
            foreach (var b in boxColliderFilter)
            {
                ref var boxColliderComp = ref boxColliderFilter.Get1(b);
                ref var transformComp = ref boxColliderFilter.Get2(b);

                var entity = boxColliderFilter.GetEntity(b);

                var center = transformComp.transform.position;
                if (entity.HasComponent<ParticleComp>()) center = entity.GetComponent<ParticleComp>().particle.position;

                boxColliderComp.boxCollider.bounds.Size = transformComp.transform.scale;
                boxColliderComp.boxCollider.bounds.UpdateCenter(center);
                boxColliderComp.boxCollider.bounds.UpdateBounds();
            }

            foreach(var s in sphereColliderFilter)
            {
                ref var sphereCollider = ref sphereColliderFilter.Get1(s);
                ref var transformComp = ref sphereColliderFilter.Get2(s);

                var entity = sphereColliderFilter.GetEntity(s);

                var center = transformComp.transform.position;
                if (entity.HasComponent<ParticleComp>()) center = entity.GetComponent<ParticleComp>().particle.position;

                sphereCollider.sphereCollider.sphereBound.UpdateCenter(center);
            }
        }

        public override void FixedUpdate()
        {
            foreach(var b in boxColliderFilter)
            {
                ref var boxColliderComp = ref boxColliderFilter.Get1(b);
                ref var transformComp = ref boxColliderFilter.Get2(b);

                var entity = boxColliderFilter.GetEntity(b);

                var center = transformComp.transform.position;
                if (entity.HasComponent<ParticleComp>()) center = entity.GetComponent<ParticleComp>().particle.position;

                boxColliderComp.boxCollider.bounds.UpdateCenter(center);
                boxColliderComp.boxCollider.bounds.UpdateBounds();
            }

            foreach (var s in sphereColliderFilter)
            {
                ref var sphereCollider = ref sphereColliderFilter.Get1(s);
                ref var transformComp = ref sphereColliderFilter.Get2(s);

                var entity = sphereColliderFilter.GetEntity(s);

                var center = transformComp.transform.position;
                if (entity.HasComponent<ParticleComp>()) center = entity.GetComponent<ParticleComp>().particle.position;

                sphereCollider.sphereCollider.sphereBound.UpdateCenter(center);
            }
        }

        public override void PostRender()
        {
            foreach (var b in boxColliderFilter)
            {
                ref var boxColliderComp = ref boxColliderFilter.Get1(b);
                ref var transformComp = ref boxColliderFilter.Get2(b);

                var color = new Vector3(0, 1, 0);
                MespDebug.DrawWireBox(boxColliderComp.boxCollider.bounds, color);
            }

            foreach (var s in sphereColliderFilter)
            {
                ref var sphereCollider = ref sphereColliderFilter.Get1(s);
                ref var transformComp = ref sphereColliderFilter.Get2(s);

                MespDebug.DrawWireSphere(sphereCollider.sphereCollider.sphereBound.Center, sphereCollider.sphereCollider.sphereBound.radius, 32);
            }
        }
    }


}
