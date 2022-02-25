using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimulationSystem.ECS.Entegration;

namespace SimulationWFA
{
    public class SerializedEditor
    {

        public SerializedEditor()
        {


        }

        public List<SimTextBox> SerializedTexts = new List<SimTextBox>();

        public List<SimTextBox[]> SerializedCompTexts = new List<SimTextBox[]>();

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
            SerializedEditor.ResetItem(resetButton.item);
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
                                serializedFieldTexs[j].Location = new Point((j * 30), i * 20);
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
                            SerializedCompTexts.Add(serializedFieldTexs);

                            resButton[i] = new ResetButton();
                            resButton[i].Location = new Point(100, i * 20);
                            resButton[i].Size = new Size(50, 20);
                            resButton[i].Text = "Reset";
                            resButton[i].item = serializedCompItem;
                            resButton[i].simPosText = serializedFieldTexs;
                            resButton[i].Click += new System.EventHandler(resetButton_Click);
                            resButton[i].BringToFront();
                            componentPanel.Controls.Add(resButton[i]);

                        }

                        break;
                    }



                default:
                    break;
            }



        }

        private static Vector3 InitializeItemVector(int idx, string text, dynamic obj)
        {
            Vector3 itemVec = new Vector3(obj.X, obj.Y, obj.Z);

            switch (idx)
            {
                case 0:
                    itemVec.X = Int32.Parse(text);
                    break;
                case 1:
                    itemVec.Y = Int32.Parse(text);
                    break;
                case 2:
                    itemVec.Z = Int32.Parse(text);
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
                    fields[0].SetValue(textBox.serializedItem, InitializeItemVector(textBox.textId, textBox.Text, obj));
                    break;


                default:
                    break;
            }
        }

        public static void ResetItem(SerializedComponent serializedItem)
        {
            var type = serializedItem.GetType();
            switch (type.Name)
            {
                case "TransformSerialized":
                    var fields = type.GetFields();
                    fields[0].SetValue(serializedItem, new Vector3(0, 0, 0));
                    break;


                default:
                    break;
            }
        }
    }
}
