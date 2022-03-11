using System;
using System.Collections.Generic;
using SimulationSystem.ECS.Entegration;
using SimulationWFA.MespSimulationSystem.ProgramLibrary.ECSEntegration.SerializedComponent;
using TheSimulation.SerializedComponent;

namespace SimulationWFA.MespSimulationSystem.ProgramLibrary
{
    public static class SerializedComponentPool
    {
        public static Dictionary<int, SerializedComponent> SerializedCompTypes = new Dictionary<int, SerializedComponent>() {

            {0, new TransformSerialized()},
            {1, new MeshRendererSerialized()}
           // {2, new TestSystemSerialized()}

        };


        public static SerializedComponent ReturnNewComponentFromList(int idx)
        {
            return  SerializedCompTypes[idx];
        }
    }
}