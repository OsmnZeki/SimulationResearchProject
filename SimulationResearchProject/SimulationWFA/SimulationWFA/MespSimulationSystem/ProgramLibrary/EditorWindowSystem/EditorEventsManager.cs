using System.Collections.Generic;
using SimulationSystem;
using SimulationSystem.ECS.Entegration;

namespace SimulationWFA.MespSimulationSystem.ProgramLibrary.EditorWindowSystem
{
    public interface IEditorEvent {}

    public struct OnEditorCreateSimObjEvent : IEditorEvent
    {
        public SimObject simObject;
    }

    public struct OnEditorAddCompSimObjEvent : IEditorEvent
    {
        public SimObject simObject;
        public SerializedComponent serializedComponent;
    }
    
    public struct OnEditorChangeCompSimObjEvent : IEditorEvent
    {
        public SimObject simObject;
    }
    
    public class EditorEventsManager
    {
        public List<IEditorEvent> events;

        public EditorEventsManager()
        {
            events = new List<IEditorEvent>();
        }
        
        public void SendEvent(IEditorEvent e)
        {
            events.Add(e);
        }

        public bool ListenEvent<T>(out T eventData) where T : IEditorEvent
        {
            foreach (var e in events)
            {
                if (e is T ee)
                {
                    eventData = ee;
                    events.Remove(e);
                    return true;
                }
            }

            eventData = default;
            return false;
        }

        
    }
}