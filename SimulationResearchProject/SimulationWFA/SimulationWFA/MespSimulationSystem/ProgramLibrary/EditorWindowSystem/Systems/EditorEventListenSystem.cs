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
                createData.simObject.CreateEntity(world);
                createData.simObject.InjectAllSerializedComponents(world);
                return;
            }

           if (eventManager.ListenEvent<OnEditorAddCompSimObjEvent>(out var addCompData))
            {
                addCompData.simObject.AddNewSerializedComponent(world, addCompData.serializedComponent);
                return;
            }

            if (eventManager.ListenEvent<OnEditorFunction>(out var functionData))
            {
                functionData.editorFunction();
            }

            if (eventManager.ListenEvent<OnEditorRefresh>(out var changeData))
            {
                var allSimObj = SimObject.FindObjectsOfType<TransformSerialized>();

                foreach(var simObj in allSimObj)
                {
                    simObj.RemoveAllComponents();
                    simObj.InjectAllSerializedComponents(world);
                }
                return;
            }

            
        }
    }
}
