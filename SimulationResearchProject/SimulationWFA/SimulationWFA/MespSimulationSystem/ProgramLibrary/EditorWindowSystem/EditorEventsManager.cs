using System;
using System.Collections.Generic;
using SimulationSystem;
using SimulationSystem.ECS.Entegration;
using static SimulationSystem.EditorEvents.EditorEventsManager;

namespace SimulationSystem.EditorEvents
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
    
    public struct OnEditorRefresh : IEditorEvent
    {

    }

    public struct OnEditorFunction : IEditorEvent
    {
        public EditorFunction editorFunction;
    }
    
    public class EditorEventsManager
    {
        public List<IEditorEvent> events;

        public delegate void EditorFunction();

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