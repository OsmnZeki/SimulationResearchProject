﻿using Dalak.Ecs;

namespace SimulationSystem.ECS.Entegration
{
    public abstract class SerializedComponent
    {
        public bool add = true;
        public abstract void AddComponent(Entity entity,World world);
        public abstract string GetName();
        
        
        public static T ReturnSerialized<T>(T data) where T : struct
        {
            return data;
        }
    }
    
    public abstract class SerializedComponent<T> : SerializedComponent where T : struct
    {
        public T t;
        public override void AddComponent(Entity entity, World world)
        {
            entity.AddComponent<T>() = t;
        }

        
    }

    public abstract class SerializedTag<T> : SerializedComponent where T : struct
    {
        public override void AddComponent(Entity entity, World world)
        {
            entity.AddComponent<T>();
        }
    }
    
}