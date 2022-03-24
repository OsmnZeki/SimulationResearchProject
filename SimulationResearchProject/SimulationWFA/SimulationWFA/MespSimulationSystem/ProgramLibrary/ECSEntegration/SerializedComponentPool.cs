using System;
using System.Collections.Generic;
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


        public static SerializedComponent ReturnNewComponentFromList(int idx)
        {
            return Activator.CreateInstance(SerializedCompTypes[idx]) as SerializedComponent;
        }
    }
}