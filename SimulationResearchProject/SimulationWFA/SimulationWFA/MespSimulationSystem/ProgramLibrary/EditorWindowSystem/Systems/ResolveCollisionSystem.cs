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
        readonly Filter<RigidbodyComp,BoxColliderComp> boxColliderFilter = null;

        public override void FixedUpdate()
        {

            foreach(var b in boxColliderFilter)
            {
                ref var boxColliderComp = ref boxColliderFilter.Get2(b);
                if (boxColliderComp.boxCollider.collisionContact == null) continue;

                var contact = boxColliderComp.boxCollider.collisionContact;

                Rigidbody rigidBody = contact.rigidBody;
                Rigidbody otherRigidBody = new Rigidbody();
                float e = 0;
                if (contact.otherRigidbody == null)
                {
                    otherRigidBody.totalForce = Vector3.Zero;
                    otherRigidBody.restitution = 0;
                    otherRigidBody.mass = 1;
                }
                else otherRigidBody = contact.otherRigidbody;

                e = Math.Min(rigidBody.restitution, otherRigidBody.restitution);
                var velocity = rigidBody.GetCurrentVelocity();
                var otherVelocity = otherRigidBody.GetCurrentVelocity();

                Vector3 vRel = velocity - otherVelocity;

                float impulseMagnitude = -(1 + e) * Vector3.Dot(vRel, contact.normal) / (rigidBody.InvMass() + otherRigidBody.InvMass());
                Vector3 impulseDirection = contact.normal;

                Vector3 jn = impulseDirection * impulseMagnitude;
                rigidBody.ApplyImpulse(jn);
                if (contact.otherRigidbody != null) contact.otherRigidbody.ApplyImpulse(-jn);

                boxColliderComp.boxCollider.collisionContact = null;
            }
        }

    }
}
