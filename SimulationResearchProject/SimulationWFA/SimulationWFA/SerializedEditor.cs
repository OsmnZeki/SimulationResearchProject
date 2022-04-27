using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProgramLibrary;
using RenderLibrary.Graphics.PreparedModels;
using RenderLibrary.Graphics.RenderData;
using RenderLibrary.Graphics.Rendering;
using RenderLibrary.Shaders;
using SimulationSystem.Components;
using SimulationSystem.ECS.Entegration;
using SimulationSystem.SharedData;
using SimulationSystem.Systems;
using SimulationWFA.MespSimulationSystem.ProgramLibrary;
using SimulationWFA.MespUtils;
using TheSimulation.SerializedComponent;

namespace SimulationWFA
{
    public class SerializedEditor
    {

        public SerializedEditor()
        {


        }

        public List<SimTextBox> serializedTexts = new List<SimTextBox>();

        public List<SimTextBox[]> transformSerializedCompTexts = new List<SimTextBox[]>();
        public List<Label[]> meshSerializedCompLabels = new List<Label[]>();

        private Vector2 serializedComponentName = new Vector2(150, 60);
        private Vector2 vectorLocation = new Vector2(30, 20);
        private Vector2 vectorSize = new Vector2(30, 20);
        private Vector2 resetButtonLocation = new Vector2(100, 20);
        private Vector2 resetButtonSize = new Vector2(50, 20);

        //public void GetType(SerializedComponent serializedComponent)
        //{
        //    var type = serializedComponent.GetType();
        //    switch (type.Name)
        //    {
        //        case "TransformSerialized":
        //            var fields = type.GetFields();
        //            dynamic obj = fields[0].GetValue(serializedComponent);
        //            float a = obj.X;
        //            break;


        //        default:
        //            break;
        //    }

        //}

        private void simulationProject_TextChanged(object sender, EventArgs e, HierarchySimButton simButton)
        {
            SimTextBox textBox = sender as SimTextBox;
            if (Int32.TryParse(textBox.Text, out int result) == false)
            {
                textBox.Text = "0";
            }
            SerializedEditor.SetItem(textBox, simButton);
            //posText[textBox.TabIndex - 1].Text = textBox.Text;
            //float a = float.Parse(posText[textBox.TabIndex - 1].Text);
            //Console.WriteLine(a);
            //parameters[0].pos.X = a;
        }
        private void resetButton_Click(object sender, EventArgs e)
        {
            ResetButton resetButton = sender as ResetButton;
            for (int i = 0; i < resetButton.simPosText.Length; i++)
            {
                resetButton.simPosText[i].Text = "0";
            }
            SerializedEditor.ResetItem(resetButton.item, resetButton.fieldId);
        }

        public void SetSerializedItemOnEditor(SerializedComponent serializedCompItem, HierarchySimButton simButton, Panel inspectorPanel, int totalCompLenght)
        {
            int idx = 0;
            var type = serializedCompItem.GetType();
            FieldInfo[] fields = type.GetFields();

            List<Control> vec3Controls = new List<Control>();
            List<Control> meshControls = new List<Control>();
            List<Control> singleControls = new List<Control>();
            PrepareSerializedCompName(simButton, serializedCompItem, vec3Controls, meshControls, singleControls);

            foreach (var field in fields)
            {
                switch (field.FieldType.Name)
                {
                    case "Vector3":
                        {
                            PrepareVector3Case(field, idx, serializedCompItem, simButton, vec3Controls);
                            break;
                        }
                    case "Mesh":
                        {
                            PrepareMeshCase(simButton, serializedCompItem, meshControls);
                            break;
                        }
                    case "Material":
                        {
                            PrepareMaterialCase(simButton, serializedCompItem, vec3Controls, meshControls);
                            break;
                        }
                    case "Single":
                        PrepareFloatCase(simButton, serializedCompItem, field, singleControls);
                        break;

                    default:
                        break;
                }
                idx++;
            }

            simButton.componentPanel.TotalInspectorPanelHeight += 20;
            inspectorPanel.Controls.Add(simButton.componentPanel);

        }

        private void PrepareFloatCase(HierarchySimButton simButton, SerializedComponent serializedCompItem, FieldInfo field, List<Control> floatControls)
        {
            Label label = new Label();
            label.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight);
            label.Size = new Size(90, 20);
            label.Text = field.Name;
            label.BackColor = Color.AliceBlue;
            label.BringToFront();
            floatControls.Add(label);
            simButton.componentPanel.Controls.Add(label);

            object o = field.GetValue(serializedCompItem);

            SimTextBox simTextBox = new SimTextBox();
            simTextBox.Location = new Point(label.Size.Width , simButton.componentPanel.TotalInspectorPanelHeight);
            simTextBox.Text = o.ToString();
            simTextBox.BackColor = Color.Yellow;
            simTextBox.Size = new Size(30,20);
            simTextBox.textId = 0;
            simTextBox.serializedItem = serializedCompItem;
            simTextBox.TextChanged += (sender2, e2) => simulationProject_TextChanged(sender2, e2, simButton); 
            simTextBox.BringToFront();
            floatControls.Add(simTextBox);
            simButton.componentPanel.Controls.Add(simTextBox);

            simButton.componentPanel.TotalInspectorPanelHeight += label.Size.Height;
        }

        private void PrepareSerializedCompName(HierarchySimButton simButton, SerializedComponent serializedCompItem, List<Control> vec3Controls, List<Control> meshControls, List<Control> floatControls)
        {
            SimTextBox serializedText = new SimTextBox();
            serializedText.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight);
            serializedText.Text = serializedCompItem.GetName();
            serializedText.BackColor = Color.Red;
            serializedText.Size = new Size((int)serializedComponentName.X, (int)serializedComponentName.Y + simButton.componentPanel.TotalInspectorPanelHeight);
            serializedText.BringToFront();
            serializedTexts.Add(serializedText);
            switch (serializedText.Text)
            {
                case "Transform Serialized":
                    vec3Controls.Add(serializedText);
                    break;
                case "Mesh Serialized":
                    meshControls.Add(serializedText);
                    break;
                case "Camera Serialized":
                    floatControls.Add(serializedText);
                        break;
                default:
                    break;
            }
            simButton.componentPanel.Controls.Add(serializedTexts[serializedTexts.Count - 1]);
            simButton.componentPanel.TotalInspectorPanelHeight += serializedText.Size.Height;
        }

        private void PrepareMaterialCase(HierarchySimButton simButton, SerializedComponent serializedCompItem, List<Control> vec3Controls, List<Control> meshControls)
        {
            Label matRendLabels = new Label();
            string[] matFiles = Directory.GetFiles(SimPath.MaterialsPath, "*.mat", SearchOption.AllDirectories);
            matRendLabels.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight);
            matRendLabels.Text = "Material";
            matRendLabels.BackColor = Color.Yellow;
            matRendLabels.Size = new Size(60, 20);
            simButton.componentPanel.Controls.Add(matRendLabels);
            meshControls.Add(matRendLabels);

            ComboBox matComboBoxes = new ComboBox();
            matComboBoxes = new ComboBox();
            matComboBoxes.Location = new Point(60, simButton.componentPanel.TotalInspectorPanelHeight);
            matComboBoxes.Text = "Add Material";
            matComboBoxes.TextChanged += (sender, e) => matComboBoxes_Changed(sender, e, serializedCompItem, simButton);
            matComboBoxes.BackColor = Color.White;

            for (int j = 0; j < matFiles.Length; j++)
            {
                FileInfo fileInfo = new FileInfo(matFiles[j]);
                matComboBoxes.Items.Add(fileInfo.Name.ToString());
            }
            simButton.componentPanel.Controls.Add(matComboBoxes);
            meshControls.Add(matComboBoxes);
            simButton.componentPanel.TotalInspectorPanelHeight += matRendLabels.Height;

            RemoveComponentButton removeComponentButton = new RemoveComponentButton();
            removeComponentButton.Location = new Point(50, simButton.componentPanel.TotalInspectorPanelHeight);
            removeComponentButton.Size = new Size(110, 20);
            removeComponentButton.Text = "RemoveComponent";
            removeComponentButton.BackColor = Color.White;
            removeComponentButton.Click += (sender, e) => removeComponentButton_Click(sender, e, simButton, serializedCompItem, meshControls);  //new System.EventHandler(removeComponentButton_Click);
            removeComponentButton.BringToFront();
            simButton.componentPanel.Controls.Add(removeComponentButton);
            simButton.componentPanel.TotalInspectorPanelHeight += 20;

            meshControls.Add(removeComponentButton);
        }

        private void PrepareMeshCase(HierarchySimButton simButton, SerializedComponent serializedCompItem, List<Control> meshControls)
        {
            Label meshRendLabels = new Label();
            string[] meshFiles = Directory.GetFiles(SimPath.MeshesPath, "*.mesh", SearchOption.AllDirectories);
            meshRendLabels.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight);
            meshRendLabels.Text = "Mesh";
            meshRendLabels.BackColor = Color.Yellow;
            meshRendLabels.Size = new Size(60, 20);
            simButton.componentPanel.Controls.Add(meshRendLabels);
            meshControls.Add(meshRendLabels);

            ComboBox meshComboBoxes = new ComboBox();
            meshComboBoxes = new ComboBox();
            meshComboBoxes.Location = new Point(60, simButton.componentPanel.TotalInspectorPanelHeight);
            meshComboBoxes.Text = "Add Mesh";
            meshComboBoxes.TextChanged += (sender, e) => meshComboBoxes_Changed(sender, e, serializedCompItem, simButton);
            meshComboBoxes.BackColor = Color.White;

            for (int j = 0; j < meshFiles.Length; j++)
            {
                FileInfo fileInfo = new FileInfo(meshFiles[j]);
                meshComboBoxes.Items.Add(fileInfo.Name.ToString());
            }
            simButton.componentPanel.Controls.Add(meshComboBoxes);
            meshControls.Add(meshComboBoxes);
            simButton.componentPanel.TotalInspectorPanelHeight += meshRendLabels.Height;
        }

        private void PrepareVector3Case(FieldInfo field, int idx, SerializedComponent serializedCompItem, HierarchySimButton simButton, List<Control> vec3Controls)
        {
            ResetButton[] resButton = new ResetButton[3];
            Label[] fieldName = new Label[3];
            string[] vecValues = new string[3];

            Vector3 fieldValue = (Vector3)field.GetValue(serializedCompItem);
            vecValues[0] = fieldValue.X.ToString();
            vecValues[1] = fieldValue.Y.ToString();
            vecValues[2] = fieldValue.Z.ToString();

            SimTextBox[] serializedFieldTexs = new SimTextBox[3];

            fieldName[idx] = new Label();
            fieldName[idx].Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight);
            fieldName[idx].Size = new Size(30, 20);
            fieldName[idx].Text = field.Name;
            fieldName[idx].BackColor = Color.AliceBlue;
            fieldName[idx].BringToFront();
            vec3Controls.Add(fieldName[idx]);
            simButton.componentPanel.Controls.Add(fieldName[idx]);

            resButton[idx] = new ResetButton();
            resButton[idx].Location = new Point((int)resetButtonLocation.X + fieldName[idx].Size.Width, simButton.componentPanel.TotalInspectorPanelHeight);
            resButton[idx].Size = new Size((int)resetButtonSize.X, (int)resetButtonSize.Y);
            resButton[idx].Text = "Reset";
            resButton[idx].BackColor = Color.White;
            resButton[idx].fieldId = idx;
            resButton[idx].item = serializedCompItem;
            resButton[idx].simPosText = serializedFieldTexs;
            resButton[idx].Click += new System.EventHandler(resetButton_Click);
            resButton[idx].BringToFront();
            vec3Controls.Add(resButton[idx]);
            simButton.componentPanel.Controls.Add(resButton[idx]);

            for (int i = 0; i < 3; i++)
            {
                serializedFieldTexs[i] = new SimTextBox();
                serializedFieldTexs[i].Location = new Point((i * (int)vectorLocation.X + fieldName[idx].Size.Width), simButton.componentPanel.TotalInspectorPanelHeight);
                serializedFieldTexs[i].Text = vecValues[i];
                serializedFieldTexs[i].BackColor = Color.Yellow;
                serializedFieldTexs[i].Size = new Size((int)vectorSize.X, (int)vectorSize.Y);
                serializedFieldTexs[i].textId = i;
                serializedFieldTexs[i].fieldId = idx;
                serializedFieldTexs[i].serializedItem = serializedCompItem;
                serializedFieldTexs[i].TextChanged += (sender2, e2) => simulationProject_TextChanged(sender2, e2, simButton);
                serializedFieldTexs[i].BringToFront();
                vec3Controls.Add(serializedFieldTexs[i]);
                simButton.componentPanel.Controls.Add(serializedFieldTexs[i]);
            }
            simButton.componentPanel.TotalInspectorPanelHeight += (int)vectorSize.Y;

            if (idx == 2)
            {
                RemoveComponentButton removeComponentButton = new RemoveComponentButton();
                removeComponentButton.Location = new Point(50, simButton.componentPanel.TotalInspectorPanelHeight);
                removeComponentButton.Size = new Size(110, 20);
                removeComponentButton.Text = "RemoveComponent";
                removeComponentButton.BackColor = Color.White;
                removeComponentButton.Click += (sender, e) => removeComponentButton_Click(sender, e, simButton, serializedCompItem, vec3Controls);  //new System.EventHandler(removeComponentButton_Click);
                removeComponentButton.BringToFront();
                simButton.componentPanel.Controls.Add(removeComponentButton);
                simButton.componentPanel.TotalInspectorPanelHeight += 20;
            }
        }

        private void removeComponentButton_Click(object sender, EventArgs e, HierarchySimButton simButton, SerializedComponent serializedCompItem, List<Control> deletedControl)
        {
            /*
            Type serializedType = SerializedComponentPool.GetSerializedComponent(serializedCompItem.GetName());
            Type compType = SerializedComponentPool.GetComponentForRemove(serializedCompItem.GetName());

            if (serializedType != null && compType != null)
            {
                RemoveComponentButton removeComponentButton = sender as RemoveComponentButton;
                EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                    editorFunction = () => {
                        simButton.simObject.entity.RemoveComponent<TransformComp>(); //TODO: DÜZELT
                        simButton.simObject.objectData.RemoveSerializedComp(serializedType);
                    }

                });

                foreach (var control in vec3Controls)
                {
                    simButton.componentPanel.Controls.Remove(control);
                }
                simButton.componentPanel.TotalInspectorPanelHeight -= 120;
                simButton.componentPanel.Controls.Remove(removeComponentButton);
                vec3Controls.Clear();
            }

            else
            {
                MessageBox.Show("There is an error while removing component");
            }
            */
            //Geçiçi
            RemoveComponentButton removeComponentButton = sender as RemoveComponentButton;
            switch (serializedCompItem.GetName())
            {
                case "Transform Serialized":
                    EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                        editorFunction = () => {
                            simButton.simObject.entity.RemoveComponent<TransformComp>(); //TODO: DÜZELT
                            simButton.simObject.objectData.RemoveSerializedComp(typeof(TransformSerialized));
                        }

                    });

                    foreach (var control in deletedControl)
                    {
                        simButton.componentPanel.Controls.Remove(control);
                    }
                    simButton.componentPanel.TotalInspectorPanelHeight -= 120;
                    simButton.componentPanel.Controls.Remove(removeComponentButton);
                    deletedControl.Clear();
                    break;
                case "Mesh Serialized":
                    EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                        editorFunction = () => {
                            simButton.simObject.entity.RemoveComponent<MeshRendererComp>(); //TODO: DÜZELT
                            simButton.simObject.objectData.RemoveSerializedComp(typeof(MeshRendererSerialized));
                        }

                    });

                    foreach (var control in deletedControl)
                    {
                        simButton.componentPanel.Controls.Remove(control);
                    }
                    simButton.componentPanel.TotalInspectorPanelHeight -= 100;
                    simButton.componentPanel.Controls.Remove(removeComponentButton);
                    deletedControl.Clear();

                    break;
                default:
                    break;
            }
        }

        private void matComboBoxes_Changed(object sender, EventArgs e, SerializedComponent serializedCompItem, HierarchySimButton simButton)
        {
            MeshRendererSerialized meshRendererSerialized = serializedCompItem as MeshRendererSerialized;
            dynamic obj = sender;
            string filename = obj.Text;

            EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                editorFunction = () => {
                    Material material = AssetUtils.LoadFromAsset<Material>(filename);
                    meshRendererSerialized.material = material;
                }

            });
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                refreshedSimObj = simButton.simObject
            });

        }

        private void meshComboBoxes_Changed(object sender, EventArgs e, SerializedComponent serializedComponent, HierarchySimButton simButton)
        {

            MeshRendererSerialized meshRendererSerialized = serializedComponent as MeshRendererSerialized;
            dynamic obj = sender;
            string filename = obj.Text;

            EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                editorFunction = () => {
                    Mesh mesh = AssetUtils.LoadFromAsset<Mesh>(filename);
                    meshRendererSerialized.mesh = mesh;
                }

            });
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                refreshedSimObj = simButton.simObject
            });
        }


        private static Vector3 InitializeItemVector(int idx, string text, dynamic obj)
        {
            Vector3 itemVec = new Vector3(obj.X, obj.Y, obj.Z);
            int result = 0;
            switch (idx)
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
        public static void SetItem(SimTextBox textBox, HierarchySimButton simButton)
        {
            var type = textBox.serializedItem.GetType();
            switch (type.Name)
            {
                case "TransformSerialized":
                    var fields = type.GetFields();
                    dynamic obj = fields[textBox.fieldId].GetValue(textBox.serializedItem);
                    fields[textBox.fieldId].SetValue(textBox.serializedItem, InitializeItemVector(textBox.textId, textBox.Text, obj));
                    EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {
                        refreshedSimObj = simButton.simObject
                    });
                    break;


                default:
                    break;
            }
        }

        public static void ResetItem(SerializedComponent serializedItem, int fieldId)
        {
            var type = serializedItem.GetType();
            switch (type.Name)
            {
                case "TransformSerialized":
                    var fields = type.GetFields();
                    fields[fieldId].SetValue(serializedItem, new Vector3(0, 0, 0));
                    break;


                default:
                    break;
            }
        }
    }
}
