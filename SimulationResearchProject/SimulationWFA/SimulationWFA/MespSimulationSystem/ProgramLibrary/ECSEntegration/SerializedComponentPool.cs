using System;
using System.Collections.Generic;
using SimulationSystem.Components;
using SimulationSystem.ECS.Entegration;
using SimulationWFA.MespSimulationSystem.ProgramLibrary.ECSEntegration.SerializedComponent;
using TheSimulation.SerializedComponent;

namespace SimulationWFA.MespSimulationSystem.ProgramLibrary
{
    public static class SerializedComponentPool
    {
        public static Dictionary<int, Type> SerializedCompTypes = new Dictionary<int, Type>() {

            {0,  typeof(TransformSerialized)},
            {1, typeof(MeshRendererSerialized)}
           // {2, new TestSystemSerialized()}
        };

        public static string[] SerializedCompNames = {

            "TransformSerialized",
            "MeshRendererSerialized"

        };

        public static Type GetSerializedComponent(string serializedCompName)
        {
            switch (serializedCompName)     
            {
                case "Transform Serialized":
                    return typeof(TransformSerialized);
                    break;
                case "Mesh Serialized":
                    return typeof(MeshRendererSerialized);
                    break;
                default:
                    break;
            }

            return null;
        }

        public static Type GetComponentForRemove(string serializedCompName)
        {
            switch (serializedCompName)
            {
                case "Transform Serialized":
                    return typeof(TransformComp);
                    break;
                case "Mesh Serialized":
                    return typeof(MeshRendererComp);
                    break;
                default:
                    break;
            }

            return null;
        }
        public static SerializedComponent ReturnNewComponentFromList(int idx)
        {
            return Activator.CreateInstance(SerializedCompTypes[idx]) as SerializedComponent;
        }
    }
}