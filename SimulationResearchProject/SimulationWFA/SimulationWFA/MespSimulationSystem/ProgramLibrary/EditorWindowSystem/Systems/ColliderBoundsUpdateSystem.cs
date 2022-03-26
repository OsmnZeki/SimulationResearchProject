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

        public override void Awake()
        {
            foreach (var b in boxColliderFilter)
            {
                ref var boxColliderComp = ref boxColliderFilter.Get1(b);
                ref var transformComp = ref boxColliderFilter.Get2(b);

                var entity = boxColliderFilter.GetEntity(b);

                var center = transformComp.transform.position;
                if (entity.HasComponent<RigidbodyComp>()) center = entity.GetComponent<RigidbodyComp>().rigidbody.position;

                boxColliderComp.boxCollider.bounds.Size = transformComp.transform.scale;
                boxColliderComp.boxCollider.bounds.UpdateCenter(center);
                boxColliderComp.boxCollider.bounds.UpdateBounds();
            }
        }

        public override void Update()
        {
            foreach (var b in boxColliderFilter)
            {
                ref var boxColliderComp = ref boxColliderFilter.Get1(b);
                ref var transformComp = ref boxColliderFilter.Get2(b);

                var color = new Vector3(0, 1, 0);
                MespDebug.DrawBox(boxColliderComp.boxCollider.bounds, color);
                
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
                if (entity.HasComponent<RigidbodyComp>()) center = entity.GetComponent<RigidbodyComp>().rigidbody.position;

                boxColliderComp.boxCollider.bounds.UpdateCenter(center);
                boxColliderComp.boxCollider.bounds.UpdateBounds();
            }
        }


    }
}
