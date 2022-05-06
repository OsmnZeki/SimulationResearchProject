using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulationSystem.ECS.Entegration;

namespace SimulationWFA.SerializedEditorClasses
{
    public abstract class SerializedEditorAbstract
    {
        public string name { get; set; }
        public Vector2 size;
        public Vector2 point;
        public List<Control> controls;
        public HierarchySimButton simButton;
        public RemoveComponentButton removeComponentButton;
        public Panel panel;
        public virtual void SetComponentInPanel(SerializedComponent serializedCompItem) { }
        public virtual void RemoveComponentButton() { }
        public virtual void removeComponentButton_Click(object sender, EventArgs e) { }

    }
}
