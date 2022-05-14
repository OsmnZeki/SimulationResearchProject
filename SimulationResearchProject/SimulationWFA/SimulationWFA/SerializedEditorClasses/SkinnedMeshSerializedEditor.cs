using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulationSystem.ECS.Entegration;

namespace SimulationWFA.SerializedEditorClasses
{
    public class SkinnedMeshSerializedEditor : SerializedEditorAbstract
    {
        public SkinnedMeshSerializedEditor(HierarchySimButton hierarchySimButton, Panel inspectorPanel)
        {
            name = "Skinned Mesh Renderer Serialized";
            size = new Vector2(30, 20);
            point = new Vector2(50, 20);
            simButton = hierarchySimButton;
            controls = new List<Control>();
            removeComponentButton = new RemoveComponentButton();
            panel = inspectorPanel;
        }

        public override void RemoveComponentButton()
        {
            base.RemoveComponentButton();
        }

        /*public override void removeComponentButton_Click(object sender, EventArgs e)
        {
            base.removeComponentButton_Click(sender, e);
        }*/

        public override void SetComponentInPanel(SerializedComponent serializedCompItem)
        {
            base.SetComponentInPanel(serializedCompItem);
        }
    }
}
