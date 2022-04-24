﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Policy;
using Dalak.Ecs;
using RenderLibrary.Transform;
using SimulationSystem.Components;
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

        public void RemoveSerializedComp(Type t)
        {
            foreach (var comp in serializedComponentList)
            {
                if (comp.GetType() == t) serializedComponentList.Remove(comp);
                if (serializedComponentList.Count == 0)
                {
                    break;
                }
            }
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
        
        public Transform GetTransform()
        {
            return entity.GetComponent<TransformComp>().transform;
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

        public void InjectAllSerializedComponents(World world)
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

        public void CreateEntity(World world)
        {
            entity = world.NewEntity();
            entity.SetName(objectData.name);
        }


        public static SimObject NewSimObject(string name = "Empty")
        {
            SimObject newSimObject = new SimObject();
            newSimObject.parent = Hiearchy;
            Hiearchy.child.Add(newSimObject);

            newSimObject.objectData.name = name;

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

        private static void SearchDFS<T>(SimObject rootSimObj,List<SimObject> simObjList)
        {
            if (rootSimObj != SimObject.Hiearchy)
            {
                foreach (var item in rootSimObj.objectData.GetSerializedComponents())
                {
                    if (item.GetType() == typeof(T))
                    {
                        simObjList.Add(rootSimObj);
                        break;
                    }
                }
            }

            foreach (var child in rootSimObj.child)
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