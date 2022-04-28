using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulationSystem.Components;
using SimulationSystem.ECS.Entegration;
using SimulationSystem.Systems;
using TheSimulation.SerializedComponent;

namespace SimulationWFA.SerializedEditorClasses
{
    public class TransformSerializedEditor : SerializedEditorAbstract
    {
        public Vector2 size = new Vector2(30, 20);
        public Vector2 point = new Vector2(50, 20);
        public TransformSerializedEditor(HierarchySimButton hierarchySimButton, Panel inspectorPanel)
        {
            name = "Transform Serialized";
            simButton = hierarchySimButton;
            controls = new List<System.Windows.Forms.Control>();
            removeComponentButton = new RemoveComponentButton();
            panel = inspectorPanel;
        }

        public override void SetComponentInPanel(SerializedComponent serializedCompItem)
        {
            ResetButton[] resButton = new ResetButton[3];
            Label[] fieldName = new Label[3];
            string[] vecValues = new string[3];
            var type = serializedCompItem.GetType();
            FieldInfo[] field = type.GetFields();


            SimTextBox[] serializedFieldTexs = new SimTextBox[3];

            for (int idx = 0; idx < 3; idx++)
            {
                Vector3 fieldValue = (Vector3)field[idx].GetValue(serializedCompItem);
                vecValues[0] = fieldValue.X.ToString();
                vecValues[1] = fieldValue.Y.ToString();
                vecValues[2] = fieldValue.Z.ToString();

                fieldName[idx] = new Label();
                fieldName[idx].Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight);
                fieldName[idx].Size = new Size((int)size.X, (int)size.Y);
                fieldName[idx].Text = field[idx].Name;
                fieldName[idx].BackColor = Color.AliceBlue;
                fieldName[idx].BringToFront();
                simButton.componentPanel.Controls.Add(fieldName[idx]);

                resButton[idx] = new ResetButton();
                resButton[idx].Location = new Point((int)point.X * 2 + fieldName[idx].Size.Width, simButton.componentPanel.TotalInspectorPanelHeight);
                resButton[idx].Size = new Size((int)point.X, (int)point.Y);
                resButton[idx].Text = "Reset";
                resButton[idx].BackColor = Color.White;
                resButton[idx].fieldId = idx;
                resButton[idx].item = serializedCompItem;
                resButton[idx].simPosText = serializedFieldTexs;
                resButton[idx].Click += new System.EventHandler(resetButton_Click);
                resButton[idx].BringToFront();
                simButton.componentPanel.Controls.Add(resButton[idx]);

                for (int i = 0; i < 3; i++)
                {
                    serializedFieldTexs[i] = new SimTextBox();
                    serializedFieldTexs[i].Location = new Point((i * 30 + fieldName[idx].Size.Width), simButton.componentPanel.TotalInspectorPanelHeight);
                    serializedFieldTexs[i].Text = vecValues[i];
                    serializedFieldTexs[i].BackColor = Color.Yellow;
                    serializedFieldTexs[i].Size = new Size(30, 20);
                    serializedFieldTexs[i].textId = i;
                    serializedFieldTexs[i].fieldId = idx;
                    serializedFieldTexs[i].serializedItem = serializedCompItem;
                    serializedFieldTexs[i].TextChanged += (sender2, e2) => simulationProject_TextChanged(sender2, e2);
                    serializedFieldTexs[i].BringToFront();
                    simButton.componentPanel.Controls.Add(serializedFieldTexs[i]);
                }
                simButton.componentPanel.TotalInspectorPanelHeight += (int)20;

                if (idx == 2)
                {
                    RemoveComponentButton();
                }
            }

            panel.Controls.Add(simButton.componentPanel);
        }

        private void simulationProject_TextChanged(object sender, EventArgs e)
        {
            SimTextBox textBox = sender as SimTextBox;
            if (Int32.TryParse(textBox.Text, out int result) == false)
            {
                textBox.Text = "0";
            }
            SetItem(textBox);
        }

        private void SetItem(SimTextBox textBox)
        {
            var type = textBox.serializedItem.GetType();
            var fields = type.GetFields();
            dynamic obj = fields[textBox.fieldId].GetValue(textBox.serializedItem);
            fields[textBox.fieldId].SetValue(textBox.serializedItem, InitializeItemVector(textBox.textId, textBox.Text, obj));
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                refreshedSimObj = simButton.simObject
            });
        }

        private Vector3 InitializeItemVector(int textId, string text, dynamic obj)
        {
            Vector3 itemVec = new Vector3(obj.X, obj.Y, obj.Z);
            int result = 0;
            switch (textId)
            {
                case 0:
                    itemVec.X = Int32.TryParse(text, out result) ? result : 0;
                    break;
                case 1:
                    itemVec.Y = Int32.TryParse(text, out result) ? result : 0;
                    break;
                case 2:
                    itemVec.Z = Int32.TryParse(text, out result) ? result : 0;
                    break;
                default:
                    break;
            }

            return itemVec;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetButton resetButton = sender as ResetButton;
            for (int i = 0; i < resetButton.simPosText.Length; i++)
            {
                resetButton.simPosText[i].Text = "0";
            }
            ResetItem(resetButton.item, resetButton.fieldId);
        }

        private void ResetItem(SerializedComponent item, int fieldId)
        {
            var type = item.GetType();
            var fields = type.GetFields();
            fields[fieldId].SetValue(item, new Vector3(0, 0, 0));
        }

        public override void RemoveComponentButton()
        {
            removeComponentButton.Location = new Point(50, simButton.componentPanel.TotalInspectorPanelHeight);
            removeComponentButton.Size = new Size(110, 20);
            removeComponentButton.Text = "RemoveComponent";
            removeComponentButton.BackColor = Color.White;
            removeComponentButton.Click += (sender, e) => removeComponentButton_Click(sender, e);  //new System.EventHandler(removeComponentButton_Click);
            removeComponentButton.BringToFront();
            simButton.componentPanel.Controls.Add(removeComponentButton);
            simButton.componentPanel.TotalInspectorPanelHeight += 20;
        }

        public override void removeComponentButton_Click(object sender, EventArgs e)
        {
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                editorFunction = () => {
                    simButton.simObject.entity.RemoveComponent<TransformComp>(); //TODO: DÜZELT
                    simButton.simObject.objectData.RemoveSerializedComp(typeof(TransformSerialized));
                }

            });

            foreach (var control in controls)
            {
                simButton.componentPanel.Controls.Remove(control);
            }
            simButton.componentPanel.TotalInspectorPanelHeight -= 120;
            simButton.componentPanel.Controls.Remove(removeComponentButton);
        }
    }
}
