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
using SimulationSystem.ECS.Entegration;
using SimulationSystem.EditorEvents;
using SimulationSystem.SharedData;
using SimulationSystem.Systems;
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

        public void GetType(SerializedComponent serializedComponent)
        {
            var type = serializedComponent.GetType();
            switch (type.Name)
            {
                case "TransformSerialized":
                    var fields = type.GetFields();
                    dynamic obj = fields[0].GetValue(serializedComponent);
                    float a = obj.X;
                    break;


                default:
                    break;
            }

        }

        private void simulationProject_TextChanged(object sender, EventArgs e)
        {
            SimTextBox textBox = sender as SimTextBox;
            if (Int32.TryParse(textBox.Text, out int result) == false)
            {
                textBox.Text = "0";
            }
            SerializedEditor.SetItem(textBox);
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

        public void SetSerializedItemOnEditor(SerializedComponent serializedCompItem, ComponentPanel componentPanel, Panel inspectorPanel, int totalCompLenght)
        {
            int idx = 0;
            var type = serializedCompItem.GetType();
            FieldInfo[] fields = type.GetFields();

            PrepareSerializedCompName(componentPanel,serializedCompItem);

            foreach (var field in fields)
            {
                switch (field.FieldType.Name)
                {
                    case "Vector3":
                        {
                            PrepareVector3Case(field, idx, serializedCompItem, componentPanel);
                            break;
                        }
                    case "Mesh":
                        {
                            PrepareMeshCase(componentPanel, serializedCompItem);
                            break;
                        }
                    case "Material":
                        {
                            PrepareMaterialCase(componentPanel, serializedCompItem);
                            break;
                        }

                    default:
                        break;
                }
                idx++;
            }

            componentPanel.TotalInspectorPanelHeight += 20;
            inspectorPanel.Controls.Add(componentPanel);

        }

        private void PrepareSerializedCompName(ComponentPanel componentPanel, SerializedComponent serializedCompItem)
        {
            SimTextBox serializedText = new SimTextBox();
            serializedText.Location = new Point(0, componentPanel.TotalInspectorPanelHeight);
            serializedText.Text = serializedCompItem.GetName();
            serializedText.BackColor = Color.Red;
            serializedText.Size = new Size((int)serializedComponentName.X, (int)serializedComponentName.Y + componentPanel.TotalInspectorPanelHeight);
            serializedText.BringToFront();
            serializedTexts.Add(serializedText);
            componentPanel.Controls.Add(serializedTexts[serializedTexts.Count - 1]);
            componentPanel.TotalInspectorPanelHeight += serializedText.Size.Height;
        }

        private void PrepareMaterialCase(ComponentPanel componentPanel, SerializedComponent serializedCompItem)
        {
            Label matRendLabels = new Label();
            string[] matFiles = Directory.GetFiles(SimPath.MaterialsPath, "*.mat", SearchOption.AllDirectories);
            matRendLabels.Location = new Point(0, componentPanel.TotalInspectorPanelHeight);
            matRendLabels.Text = "Material";
            matRendLabels.BackColor = Color.Yellow;
            matRendLabels.Size = new Size(60, 20);
            componentPanel.Controls.Add(matRendLabels);

            ComboBox matComboBoxes = new ComboBox();
            matComboBoxes = new ComboBox();
            matComboBoxes.Location = new Point(60, componentPanel.TotalInspectorPanelHeight);
            matComboBoxes.Text = "Add Material";
            matComboBoxes.TextChanged += (sender, e) => matComboBoxes_Changed(sender, e, serializedCompItem);
            matComboBoxes.BackColor = Color.White;

            for (int j = 0; j < matFiles.Length; j++)
            {
                FileInfo fileInfo = new FileInfo(matFiles[j]);
                matComboBoxes.Items.Add(fileInfo.Name.ToString());
            }
            componentPanel.Controls.Add(matComboBoxes);
            componentPanel.TotalInspectorPanelHeight += matRendLabels.Height;

        }

        private void PrepareMeshCase(ComponentPanel componentPanel, SerializedComponent serializedCompItem)
        {
            Label meshRendLabels = new Label();
            string[] meshFiles = Directory.GetFiles(SimPath.MeshesPath, "*.mesh", SearchOption.AllDirectories);
            meshRendLabels.Location = new Point(0, componentPanel.TotalInspectorPanelHeight);
            meshRendLabels.Text = "Mesh";
            meshRendLabels.BackColor = Color.Yellow;
            meshRendLabels.Size = new Size(60, 20);
            componentPanel.Controls.Add(meshRendLabels);

            ComboBox meshComboBoxes = new ComboBox();
            meshComboBoxes = new ComboBox();
            meshComboBoxes.Location = new Point(60, componentPanel.TotalInspectorPanelHeight);
            meshComboBoxes.Text = "Add Mesh";
            meshComboBoxes.TextChanged += (sender, e) => meshComboBoxes_Changed(sender, e, serializedCompItem);
            meshComboBoxes.BackColor = Color.White;

            for (int j = 0; j < meshFiles.Length; j++)
            {
                FileInfo fileInfo = new FileInfo(meshFiles[j]);
                meshComboBoxes.Items.Add(fileInfo.Name.ToString());
            }
            componentPanel.Controls.Add(meshComboBoxes);
            componentPanel.TotalInspectorPanelHeight += meshRendLabels.Height;
        }

        private void PrepareVector3Case(FieldInfo field, int idx, SerializedComponent serializedCompItem, ComponentPanel componentPanel)
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
            fieldName[idx].Location = new Point(0, componentPanel.TotalInspectorPanelHeight);
            fieldName[idx].Size = new Size(30, 20);
            fieldName[idx].Text = field.Name;
            fieldName[idx].BackColor = Color.AliceBlue;
            fieldName[idx].BringToFront();
            componentPanel.Controls.Add(fieldName[idx]);

            resButton[idx] = new ResetButton();
            resButton[idx].Location = new Point((int)resetButtonLocation.X + fieldName[idx].Size.Width, componentPanel.TotalInspectorPanelHeight);
            resButton[idx].Size = new Size((int)resetButtonSize.X, (int)resetButtonSize.Y);
            resButton[idx].Text = "Reset";
            resButton[idx].BackColor = Color.White;
            resButton[idx].fieldId = idx;
            resButton[idx].item = serializedCompItem;
            resButton[idx].simPosText = serializedFieldTexs;
            resButton[idx].Click += new System.EventHandler(resetButton_Click);
            resButton[idx].BringToFront();
            componentPanel.Controls.Add(resButton[idx]);

            for (int i = 0; i < 3; i++)
            {
                serializedFieldTexs[i] = new SimTextBox();
                serializedFieldTexs[i].Location = new Point((i * (int)vectorLocation.X + fieldName[idx].Size.Width), componentPanel.TotalInspectorPanelHeight);
                serializedFieldTexs[i].Text = vecValues[i];
                serializedFieldTexs[i].BackColor = Color.Yellow;
                serializedFieldTexs[i].Size = new Size((int)vectorSize.X, (int)vectorSize.Y);
                serializedFieldTexs[i].textId = i;
                serializedFieldTexs[i].fieldId = idx;
                serializedFieldTexs[i].serializedItem = serializedCompItem;
                serializedFieldTexs[i].TextChanged += simulationProject_TextChanged;
                serializedFieldTexs[i].BringToFront();
                componentPanel.Controls.Add(serializedFieldTexs[i]);
            }
            componentPanel.TotalInspectorPanelHeight += (int)vectorSize.Y;
        }

        private void matComboBoxes_Changed(object sender, EventArgs e, SerializedComponent serializedCompItem)
        {
            MeshRendererSerialized meshRendererSerialized = serializedCompItem as MeshRendererSerialized;
            dynamic obj = sender;
            string filename = obj.Text;

            EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                editorFunction = () => {
                    Material unlitMaterial = AssetUtils.LoadFromAsset<Material>(filename);
                    meshRendererSerialized.material = unlitMaterial;
                }
                 //   EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh { }

            });

        }

        private void meshComboBoxes_Changed(object sender, EventArgs e, SerializedComponent serializedComponent)
        {

            MeshRendererSerialized meshRendererSerialized = serializedComponent as MeshRendererSerialized;
            dynamic obj = sender;
            string filename = obj.Text;
            Mesh mesh = AssetUtils.LoadFromAsset<Mesh>(filename);
            meshRendererSerialized.mesh = mesh;
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {


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
        public static void SetItem(SimTextBox textBox)
        {
            var type = textBox.serializedItem.GetType();
            switch (type.Name)
            {
                case "TransformSerialized":
                    var fields = type.GetFields();
                    dynamic obj = fields[textBox.fieldId].GetValue(textBox.serializedItem);
                    fields[textBox.fieldId].SetValue(textBox.serializedItem, InitializeItemVector(textBox.textId, textBox.Text, obj));
                    EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {


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
