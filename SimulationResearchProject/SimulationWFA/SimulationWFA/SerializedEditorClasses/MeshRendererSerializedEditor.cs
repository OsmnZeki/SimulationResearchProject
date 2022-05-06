﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProgramLibrary;
using RenderLibrary.Graphics.RenderData;
using RenderLibrary.Graphics.Rendering;
using SimulationSystem.Components;
using SimulationSystem.ECS.Entegration;
using SimulationSystem.Systems;
using SimulationWFA.MespUtils;
using TheSimulation.SerializedComponent;

namespace SimulationWFA.SerializedEditorClasses
{
    public class MeshRendererSerializedEditor : SerializedEditorAbstract
    {
        public MeshRendererSerializedEditor(HierarchySimButton hierarchySimButton, Panel inspectorPanel)
        {
            name = "Mesh Renderer Serialized";
            size = new Vector2(30, 20);
            point = new Vector2(50, 20);
            simButton = hierarchySimButton;
            controls = new List<Control>();
            removeComponentButton = new RemoveComponentButton();
            panel = inspectorPanel;
        }
        public override void SetComponentInPanel(SerializedComponent serializedCompItem)
        {

            SimTextBox serializedText = new SimTextBox();
            serializedText.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight);
            serializedText.Text = name;
            serializedText.BackColor = Color.Red;
            serializedText.Size = new Size(150, 60 + simButton.componentPanel.TotalInspectorPanelHeight);
            serializedText.BringToFront();
            simButton.componentPanel.Controls.Add(serializedText);
            controls.Add(serializedText);
            simButton.componentPanel.TotalInspectorPanelHeight += serializedText.Size.Height;

            Label meshRendLabels = new Label();
            string[] meshFiles = Directory.GetFiles(SimPath.MeshesPath, "*.mesh", SearchOption.AllDirectories);
            meshRendLabels.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight);
            meshRendLabels.Text = "Mesh";
            meshRendLabels.BackColor = Color.Yellow;
            meshRendLabels.Size = new Size(60, 20);
            simButton.componentPanel.Controls.Add(meshRendLabels);
            controls.Add(meshRendLabels);

            ComboBox meshComboBoxes = new ComboBox();
            meshComboBoxes = new ComboBox();
            meshComboBoxes.Location = new Point(60, simButton.componentPanel.TotalInspectorPanelHeight);
            meshComboBoxes.Text = "Add Mesh";
            meshComboBoxes.TextChanged += (sender, e) => meshComboBoxes_Changed(sender, e, serializedCompItem);
            meshComboBoxes.BackColor = Color.White;

            for (int j = 0; j < meshFiles.Length; j++)
            {
                FileInfo fileInfo = new FileInfo(meshFiles[j]);
                meshComboBoxes.Items.Add(fileInfo.Name.ToString());
            }
            simButton.componentPanel.Controls.Add(meshComboBoxes);
            controls.Add(meshComboBoxes);
            simButton.componentPanel.TotalInspectorPanelHeight += meshRendLabels.Height;

            Label matRendLabels = new Label();
            string[] matFiles = Directory.GetFiles(SimPath.MaterialsPath, "*.mat", SearchOption.AllDirectories);
            matRendLabels.Location = new Point(0, simButton.componentPanel.TotalInspectorPanelHeight);
            matRendLabels.Text = "Material";
            matRendLabels.BackColor = Color.Yellow;
            matRendLabels.Size = new Size(60, 20);
            simButton.componentPanel.Controls.Add(matRendLabels);
            controls.Add(matRendLabels);

            ComboBox matComboBoxes = new ComboBox();
            matComboBoxes = new ComboBox();
            matComboBoxes.Location = new Point(60, simButton.componentPanel.TotalInspectorPanelHeight);
            matComboBoxes.Text = "Add Material";
            matComboBoxes.TextChanged += (sender, e) => matComboBoxes_Changed(sender, e, serializedCompItem);
            matComboBoxes.BackColor = Color.White;

            for (int j = 0; j < matFiles.Length; j++)
            {
                FileInfo fileInfo = new FileInfo(matFiles[j]);
                matComboBoxes.Items.Add(fileInfo.Name.ToString());
            }
            simButton.componentPanel.Controls.Add(matComboBoxes);
            controls.Add(matComboBoxes);
            simButton.componentPanel.TotalInspectorPanelHeight += matRendLabels.Height;

            RemoveComponentButton removeComponentButton = new RemoveComponentButton();
            removeComponentButton.Location = new Point(50, simButton.componentPanel.TotalInspectorPanelHeight);
            removeComponentButton.Size = new Size(110, 20);
            removeComponentButton.Text = "RemoveComponent";
            removeComponentButton.BackColor = Color.White;
            removeComponentButton.Click += (sender, e) => removeComponentButton_Click(sender, e);  //new System.EventHandler(removeComponentButton_Click);
            removeComponentButton.BringToFront();
            simButton.componentPanel.Controls.Add(removeComponentButton);
            simButton.componentPanel.TotalInspectorPanelHeight += 30;

            controls.Add(removeComponentButton);
        }

        private void matComboBoxes_Changed(object sender, EventArgs e, SerializedComponent serializedCompItem)
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

        private void meshComboBoxes_Changed(object sender, EventArgs e, SerializedComponent serializedCompItem)
        {
            MeshRendererSerialized meshRendererSerialized = serializedCompItem as MeshRendererSerialized;
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

        public override void RemoveComponentButton()
        {
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorFunction {
                editorFunction = () => {
                    simButton.simObject.entity.RemoveComponent<MeshRendererComp>();
                    simButton.simObject.objectData.RemoveSerializedComp(typeof(MeshRendererSerialized));
                }

            });

            foreach (var control in controls)
            {
                simButton.componentPanel.Controls.Remove(control);
            }
            simButton.componentPanel.TotalInspectorPanelHeight -= 90;
            simButton.componentPanel.Controls.Remove(removeComponentButton);
            controls.Clear();
        }

        public override void removeComponentButton_Click(object sender, EventArgs e)
        {
            RemoveComponentButton();
        }


    }
}
