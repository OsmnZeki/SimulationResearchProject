using System;
using Dalak.Ecs;
using SimulationSystem.EditorEvents;
using TheSimulation.SerializedComponent;

namespace SimulationSystem.Systems
{
    class EditorEventListenSystem : Dalak.Ecs.System
    {
        public static EditorEventsManager eventManager = new EditorEventsManager();

        public override void Update()
        {
            ListenEditorEvents(world);
        }

        private void ListenEditorEvents(World world)
        {

            if (eventManager.ListenEvent<OnEditorCreateSimObjEvent>(out var createData))
            {
                var entity = world.NewEntity();
                createData.simObject.entity = entity;
                createData.simObject.AddAllSerializedComponents(world);
            }

           if (eventManager.ListenEvent<OnEditorAddCompSimObjEvent>(out var addCompData))
            {
                addCompData.simObject.AddNewSerializedComponent(world, addCompData.serializedComponent);

            }

            if (eventManager.ListenEvent<OnEditorRefresh>(out var changeData))
            {
                Console.WriteLine("Refresh");
                var allSimObj = SimObject.FindObjectsOfType<TransformSerialized>();

                foreach(var simObj in allSimObj)
                {
                    simObj.RemoveAllComponents();
                    simObj.AddAllSerializedComponents(world);
                }
            }
        }
    }
}
