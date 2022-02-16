﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Policy;
using Dalak.Ecs;
using SimulationSystem.ECS.Entegration;
using TheSimulation.SerializedComponent;

namespace SimulationSystem
{

    [Serializable]
    public class SimObjectData
    {
        public string name;
        private List<SerializedComponent> serializedComponentList;

        public SimObjectData()
        {
            name = "Empty SimObject";
            serializedComponentList = new List<SerializedComponent>();
        }

        public void AddSerializedComponent<T>(T serializedComponent) where T : SerializedComponent
        {
            foreach(var comp in serializedComponentList)
            {
                if (comp.GetType() == typeof(T)) return;
            }

            serializedComponentList.Add(serializedComponent);
        }

        public SerializedComponent[] GetSerializedComponents()
        {
            return serializedComponentList.ToArray();
        }
    }
    
    
    public class SimObject
    {
        public static SimObject Hiearchy = new SimObject();
        
        public SimObjectData objectData;
        private SimObject parent;
        private List<SimObject> child;
        public Entity entity;

        public SimObject()
        {
            child = new List<SimObject>();
            objectData = new SimObjectData();
        }
        
        
        public void SetParent(SimObject newParent)
        {
            parent.child.Remove(this);

            parent = newParent;
            newParent.child.Add(this);
        }

        public void AddNewSerializedComponent(World world, SerializedComponent serializedComponent)
        {
            objectData.AddSerializedComponent(serializedComponent);
        }

        public void AddAllSerializedComponents(World world)
        {
            foreach (var serializedComponent in objectData.GetSerializedComponents())
            {
                if(!serializedComponent.add){continue;}
                serializedComponent.AddComponent(entity,world);
            }
        }

        public void RemoveAllComponents()
        {
            entity.RemoveAllComponents();
        }
        

        public static SimObject NewSimObject()
        {
            SimObject newSimObject = new SimObject();
            newSimObject.parent = Hiearchy;
            Hiearchy.child.Add(newSimObject);

            newSimObject.objectData = new SimObjectData();

            newSimObject.objectData.AddSerializedComponent(new TransformSerialized() {
                pos = Vector3.Zero,
                rotation = Vector3.Zero,
                scale = Vector3.One,
            });

            return newSimObject;
        } 

        public static SimObject[] GetChildren(SimObject parentObject)
        {
            return parentObject.child.ToArray();
        }

        private static void SearchDFS<T>(SimObject simObject,List<SimObject> simObjList)
        {
            if (simObject != SimObject.Hiearchy)
            {
                foreach (var item in simObject.objectData.GetSerializedComponents())
                {
                    if (item.GetType() == typeof(T))
                    {
                        simObjList.Add(simObject);
                        break;
                    }
                }
            }

            foreach (var child in simObject.child)
            {
                SearchDFS<T>(child,simObjList);
            }
        }

        public static SimObject[] FindObjectsOfType<T>()
        {
            List<SimObject> simObjList = new List<SimObject>();
            SearchDFS<T>(Hiearchy,simObjList);
            return simObjList.ToArray();
        }
    }
}