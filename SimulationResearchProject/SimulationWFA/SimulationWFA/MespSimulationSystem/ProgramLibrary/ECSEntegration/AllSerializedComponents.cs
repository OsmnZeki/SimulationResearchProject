using System;
using System.Collections.Generic;
using SimulationSystem.ECS.Entegration;
using TheSimulation.SerializedComponent;

namespace SimulationWFA.MespSimulationSystem.ProgramLibrary
{
    public static class AllSerializedComponents
    {
        public static Dictionary<int, SerializedComponent> SerializedCompTypes = new Dictionary<int, SerializedComponent>() {
            
            {0, new MeshRendererSerialized()},
            {1, new TransformSerialized()},

        };


        public static SerializedComponent ReturnNewComponentFromList(int idx)
        {
            return  SerializedCompTypes[idx];
        }
    }
}