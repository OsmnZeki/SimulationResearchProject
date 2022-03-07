using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
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

        public List<SimTextBox> SerializedTexts = new List<SimTextBox>();

        public List<SimTextBox[]> TransformSerializedCompTexts = new List<SimTextBox[]>();
        public List<Label[]> MeshSerializedCompLabels = new List<Label[]>();


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

        public void SetSerializedItemOnEditor(SerializedComponent serializedCompItem, Panel componentPanel, Panel inspectorPanel, int totalCompLenght)
        {
            var type = serializedCompItem.GetType();
            switch (type.Name)
            {
                case "TransformSerialized":
                    {
                        SimTextBox serializedText = new SimTextBox();
                        serializedText.Location = new Point(0, 0);
                        serializedText.Text = serializedCompItem.GetName();
                        serializedText.BackColor = Color.Red;
                        serializedText.Size = new Size(150, 60);
                        serializedText.BringToFront();
                        SerializedTexts.Add(serializedText);
                        componentPanel.Controls.Add(SerializedTexts[SerializedTexts.Count - 1]);


                        var fields = type.GetFields();
                        string[] vecValues = new string[3];

                        ResetButton[] resButton = new ResetButton[3];

                        for (int i = 0; i < 3; i++)
                        {
                            Vector3 fieldVal = (Vector3)fields[i].GetValue(serializedCompItem);
                            vecValues[0] = fieldVal.X.ToString();
                            vecValues[1] = fieldVal.Y.ToString();
                            vecValues[2] = fieldVal.Z.ToString();

                            SimTextBox[] serializedFieldTexs = new SimTextBox[3];
                            for (int j = 0; j < 3; j++)
                            {
                                serializedFieldTexs[j] = new SimTextBox();
                                serializedFieldTexs[j].Location = new Point((j * 30), i * 20 + 20);
                                serializedFieldTexs[j].Text = vecValues[j];
                                serializedFieldTexs[j].BackColor = Color.Yellow;
                                serializedFieldTexs[j].Size = new Size(30, 60);
                                serializedFieldTexs[j].textId = j;
                                serializedFieldTexs[j].fieldId = i;
                                serializedFieldTexs[j].serializedItem = serializedCompItem;
                                serializedFieldTexs[j].TextChanged += simulationProject_TextChanged;
                                serializedFieldTexs[j].BringToFront();
                                componentPanel.Controls.Add(serializedFieldTexs[j]);
                            }
                            TransformSerializedCompTexts.Add(serializedFieldTexs);

                            resButton[i] = new ResetButton();
                            resButton[i].Location = new Point(100, i * 20 + 20);
                            resButton[i].Size = new Size(50, 20);
                            resButton[i].Text = "Reset";
                            resButton[i].BackColor = Color.White;
                            resButton[i].fieldId = i;
                            resButton[i].item = serializedCompItem;
                            resButton[i].simPosText = serializedFieldTexs;
                            resButton[i].Click += new System.EventHandler(resetButton_Click);
                            resButton[i].BringToFront();
                            componentPanel.Controls.Add(resButton[i]);

                        }

                        break;
                    }
                case "MeshRendererSerialized":
                    {
                        //CubeMesh cubeMesh = new CubeMesh();
                        //LitMaterial litMaterial = LitMaterial.gold;
                        //litMaterial.SetShader(ShaderPool.GetShaderByType(ShaderPool.ShaderType.LitShader));
                        //MeshRendererSerialized meshRendererSerialized =  serializedCompItem as MeshRendererSerialized;
                        //meshRendererSerialized.mesh = cubeMesh;
                        //meshRendererSerialized.material = litMaterial;
                        SimTextBox serializedText = new SimTextBox();
                        serializedText.Location = new Point(0, 100);
                        serializedText.Text = serializedCompItem.GetName();
                        serializedText.BackColor = Color.Red;
                        serializedText.Size = new Size(150, 60);
                        SerializedTexts.Add(serializedText);
                        componentPanel.Controls.Add(SerializedTexts[SerializedTexts.Count - 1]);

                        var fields = type.GetFields();
                        string[] meshValues = new string[2];
                        meshValues[0] = "Mesh";
                        meshValues[1] = "Material";
                        Label[] meshRendLabels = new Label[2];
                        ComboBox[] meshComboBoxes = new ComboBox[2];
                        string[] meshButtonValues = new string[2];
                        meshButtonValues[0] = "Add Mesh";
                        meshButtonValues[1] = "Add Material";

                        string[] matFiles = Directory.GetFiles(SimPath.MaterialsPath, "*.mat", SearchOption.AllDirectories);
                        string[] meshFiles = Directory.GetFiles(SimPath.MeshesPath, "*.mesh", SearchOption.AllDirectories);
                        //FileInfo fileInfo = new FileInfo(files[0]);

                        for (int i = 0; i < 2; i++)
                        {
                            meshRendLabels[i] = new Label();
                            meshRendLabels[i].Location = new Point(0, 120 + (i * 20));
                            meshRendLabels[i].Text = meshValues[i];
                            meshRendLabels[i].BackColor = Color.Yellow;
                            meshRendLabels[i].Size = new Size(60, 20);
                            componentPanel.Controls.Add(meshRendLabels[i]);

                            meshComboBoxes[i] = new ComboBox();
                            meshComboBoxes[i].Location = new Point(60, 120 + (i * 20));
                            meshComboBoxes[i].Text = meshButtonValues[i];
 //new EventHandler(meshComboBoxes_Changed);
                            meshComboBoxes[i].BackColor = Color.White;

                            if (i == 1)
                            {
                                meshComboBoxes[i].TextChanged += (sender, e) => matComboBoxes_Changed(sender, e, serializedCompItem);

                                for (int j = 0; j < matFiles.Length; j++)
                                {
                                    FileInfo fileInfo = new FileInfo(matFiles[j]);
                                    meshComboBoxes[i].Items.Add(fileInfo.Name.ToString());

                                }
                            }
                            else
                            {
                                meshComboBoxes[i].TextChanged += (sender, e) => meshComboBoxes_Changed(sender, e, serializedCompItem);
                                for (int j = 0; j < meshFiles.Length; j++)
                                {
                                    FileInfo fileInfo = new FileInfo(meshFiles[j]);
                                    meshComboBoxes[i].Items.Add(fileInfo.Name.ToString());
                                }
                            }
                            componentPanel.Controls.Add(meshComboBoxes[i]);
                        }
                        MeshSerializedCompLabels.Add(meshRendLabels);

                        break;
                    }



                default:
                    break;
            }

            inspectorPanel.Controls.Add(componentPanel);

        }

        private void matComboBoxes_Changed(object sender, EventArgs e, SerializedComponent serializedCompItem)
        {
            MeshRendererSerialized meshRendererSerialized = serializedCompItem as MeshRendererSerialized;
            dynamic obj = sender;
            string filename = obj.Text;
            UnlitMaterial unlitMaterial = AssetUtils.LoadFromAsset<UnlitMaterial>(filename);
            meshRendererSerialized.material = unlitMaterial;
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {


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
