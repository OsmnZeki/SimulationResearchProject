using System;
using System.Collections.Generic;
using System.Numerics;
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
    }
    
    
    public class SimObject
    {
        public static SimObject Hiearchy = new SimObject();
        
        public SimObjectData objectData;
        public SimObject parent;
        public List<SimObject> child;


        public static SimObject NewSimObject()
        {
            SimObject newSimObject = new SimObject();
            newSimObject.objectData = new SimObjectData();
            newSimObject.objectData.serializedComponentList = new List<SerializedComponent>();
            newSimObject.child = new List<SimObject>();

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

        /*public static int GetSimObjectCount()
        {
            
        }*/
        
        
        
    }
    


    
}