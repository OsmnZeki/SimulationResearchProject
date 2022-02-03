using System;
using Dalak.Ecs;
using RenderLibrary.IO;
using SimulationSystem.Systems;
using SimulationWFA.MespSimulationSystem.ProgramLibrary.EditorWindowSystem;

namespace SimulationSystem
{
    public class EditorWindowSystem
    {
        public static EditorEventsManager eventManager = new EditorEventsManager();
        
        public void CreateEditorWindow()
        {
            Screen screen = new Screen();
            screen.Create(800, 600);
            if (screen.screenAdress == IntPtr.Zero) return;

            SimulationLifecyleMethods editorWindowLifecycle = new SimulationLifecyleMethods(new ECSEditorController());

            editorWindowLifecycle.Awake();
            editorWindowLifecycle.Start();
            
            while (!screen.ShouldClose())
            {
                screen.ProcessWindowInput();
                editorWindowLifecycle.Update();
                editorWindowLifecycle.LateUpdate();
                
                screen.Update();
                editorWindowLifecycle.Render();
                screen.NewFrame();
                
                ListenEditorEvents(editorWindowLifecycle.ecsController.world);
            }
            
            editorWindowLifecycle.OnSimulationQuit();
            screen.Terminate();
        }

        private void ListenEditorEvents(World world) {

            if (eventManager.ListenEvent<OnEditorCreateSimObjEvent>(out var createData))
            {
                var entity = world.NewEntity();
                createData.simObject.entity = entity;
                createData.simObject.AddAllSerializedComponents(world);
            }
            
            if (eventManager.ListenEvent<OnEditorAddCompSimObjEvent>(out var addCompData))
            {
                addCompData.simObject.AddNewComponent(world,addCompData.serializedComponent);
               
            }
            
            if(eventManager.ListenEvent<OnEditorChangeCompSimObjEvent>(out var changeData))
            {
                changeData.simObject.RemoveAllComponents();
                changeData.simObject.AddAllSerializedComponents(world);
            }
        }
    }
}