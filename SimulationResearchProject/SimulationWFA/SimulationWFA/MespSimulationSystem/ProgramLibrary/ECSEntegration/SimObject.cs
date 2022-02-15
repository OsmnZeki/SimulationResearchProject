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
    public struct SimObjectData
    {
        public string name;
        public List<SerializedComponent> serializedComponentList;
        //TODO: aynı serializedlar listeye eklenebilir !
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
            objectData.serializedComponentList.Add(serializedComponent);
        }

        public void AddAllSerializedComponents(World world)
        {
            foreach (var serializedComponent in objectData.serializedComponentList)
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

            newSimObject.objectData.serializedComponentList = new List<SerializedComponent>();
            newSimObject.objectData.name = "Empty SimObject";

            newSimObject.objectData.serializedComponentList.Add(new TransformSerialized() {
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
                foreach (var item in simObject.objectData.serializedComponentList)
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