using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.IO;
using SimulationSystem;
using System.Dynamic;
using SimulationWFA.MespSimulationSystem.ProgramLibrary;
using SimulationSystem.Systems;
using SimulationSystem.EditorEvents;
using System.Numerics;
using System.Collections.Generic;
using SimulationSystem.ECS.Entegration;
using ProgramLibrary;

namespace SimulationWFA
{
    public partial class SimulationProject : Form
    {
        SerializedEditor serializedEditor = new SerializedEditor();
        public List<HierarchySimButton> hierarchySimButList = new List<HierarchySimButton>();
        int hierarchyHeight = 0;
        int lastHierarchyButtonIdx = -1;
        public SimulationProject()
        {
            lastHierarchyButtonIdx = -1;
            hierarchyHeight = 0;
            InitializeComponent();
            ShowProjectFiles();
            Task.Run(() => RunEditorWindow());
        }

        private void ShowProjectFiles() //program açıldığı anda projectste istenilen path içindeki dosyaları gosterir.
        {
            Cursor.Current = Cursors.WaitCursor;
            projectsTreeView.Nodes.Clear();
            if (folderBrowserDialog1.SelectedPath == "")
            {
                foreach (var item in Directory.GetDirectories(SimPath.FindDirectoryPath("SimulationResearchProject")))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(item);
                    var node = projectsTreeView.Nodes.Add(directoryInfo.Name, directoryInfo.Name, 0, 0);
                    node.Tag = directoryInfo;
                    node.ForeColor = Color.FromArgb(255, 255, 255);
                }

                foreach (var item in Directory.GetFiles(SimPath.FindDirectoryPath("SimulationResearchProject")))
                {
                    FileInfo fileInfo = new FileInfo(item);
                    var node = projectsTreeView.Nodes.Add(fileInfo.Name, fileInfo.Name, 1, 1);
                    node.Tag = fileInfo;
                    node.ForeColor = Color.FromArgb(255, 255, 255);
                }

                Cursor.Current = Cursors.Default;
            }

        }
        private void RunEditorWindow() //Editor window oluşturulup çalıştırılır.
        {
            EditorWindowSystem editorWindow = new EditorWindowSystem();
            editorWindow.CreateEditorWindow();
        }

        #region ClickEvents

        private void openToolStripMenuItem_Click(object sender, EventArgs e) //Penceredeki open tool'u
        {
            if (sender.ToString() == "New")
            {
                MessageBox.Show("New");
            }
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)//Penceredeki exit tool'u
        {
            Close();
        } 

        private void simulateButton_Click(object sender, EventArgs e) //Editor windowda suan kapalı olan simulate butonu için
        {

        } 

        private void OpenProjectFolder(TreeNodeMouseClickEventArgs e) //Seçilen folder'ın açılması için
        {
            //e.Node.BackColor = Color.FromArgb(255, 255, 255);
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = ((FileInfo)e.Node.Tag).FullName;
            info.UseShellExecute = true;
            Process process = Process.Start(info);
            Thread.Sleep(2000);
            //SetParent(process.MainWindowHandle, this.Handle);
        }  
        private void projectsTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) //Project files kısmında iç içe dosyalarda gezmek için
        {
            if (e.Node.Tag == null)
            {
                //return
            }

            else if (e.Node.Tag.GetType() == typeof(DirectoryInfo))
            {
                e.Node.Nodes.Clear();
                foreach (var item in Directory.GetDirectories(((DirectoryInfo)e.Node.Tag).FullName))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(item);
                    var node = e.Node.Nodes.Add(directoryInfo.Name, directoryInfo.Name, 0, 0);
                    node.Tag = directoryInfo;
                    node.ForeColor = Color.FromArgb(255, 255, 255);
                }

                foreach (var item in Directory.GetFiles(((DirectoryInfo)e.Node.Tag).FullName))
                {
                    FileInfo fileInfo = new FileInfo(item);
                    var node = e.Node.Nodes.Add(fileInfo.Name, fileInfo.Name, 1, 1);
                    node.Tag = fileInfo;
                    node.ForeColor = Color.FromArgb(255, 255, 255);
                }
                e.Node.Expand();
            }
            else
            {
                Task.Run(() => OpenProjectFolder(e));
            }
        } 

        private void addObjectButton_Click(object sender, EventArgs e) //Hierarchy'e obje ekler.
        {
            var simObject = SimObject.NewSimObject();
            HierarchySimButton hierarchyButton = new HierarchySimButton();
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorCreateSimObjEvent { simObject = simObject });
            hierarchyButton.Location = new Point(10, 30 + hierarchyHeight);
            hierarchyButton.Name = "hierarchyButton";
            hierarchyButton.Size = new System.Drawing.Size(75, 23);
            hierarchyButton.Text = simObject.objectData.name;
            hierarchyButton.BackColor = Color.White;
            hierarchyButton.simObject = simObject;
            hierarchyButton.id = hierarchyHeight / 30;
            hierarchyButton.componentPanel.Location = new Point(5, 30);
            hierarchyButton.componentPanel.Name = "ComponentPanel";
            hierarchyButton.componentPanel.Size = new System.Drawing.Size(180, 400);
            hierarchyButton.componentPanel.BackColor = Color.AliceBlue;
            hierarchyButton.componentPanel.AutoScroll = true;
            hierarchyButton.BringToFront();
            hierarchyButton.Click += (sender2, e2) => hierarchyButton_Click(sender2, e2, hierarchyButton.id); //new System.EventHandler(hierarchyButton_Click);
            hieararchyPanel.Controls.Add(hierarchyButton);
            hierarchyHeight += 30;
        } 

        private void hierarchyButton_Click(object sender, EventArgs e, int idx) //Hierarchy' deki objeye tıklandığında girer
        {

            Control[] control = Controls.Find("hierarchyButton", true);
            HierarchySimButton simButton = (HierarchySimButton)control[idx];

            if (hierarchySimButList.Contains(simButton) == false)
            {

                if (lastHierarchyButtonIdx != -1)
                {
                    HierarchySimButton simButtonOld = (HierarchySimButton)control[lastHierarchyButtonIdx];
                    simButtonOld.componentPanel.Visible = false;
                }
                //Panel componentPanel = new Panel();
                simButton.componentPanel.Location = new Point(5, 30);
                simButton.componentPanel.Name = "ComponentPanel";
                simButton.componentPanel.Size = new System.Drawing.Size(300, 500);
                simButton.componentPanel.BackColor = Color.AliceBlue;
                simButton.componentPanel.VerticalScroll.Enabled = true;

                Button addComponentButton = new Button();
                addComponentButton.Location = new Point(100, 550);
                addComponentButton.Size = new Size(100, 20);
                addComponentButton.BackColor = Color.White;
                addComponentButton.Text = "Add Component";
                addComponentButton.Name = "AddComponentButton";
                addComponentButton.Click += (sender2, e2) => addComponentButton_Click(sender2, e2, simButton); // new System.EventHandler(addComponentButton_Click);
                addComponentButton.BringToFront();
                simButton.componentPanel.Controls.Add(addComponentButton);



                foreach (var item in simButton.simObject.objectData.GetSerializedComponents())
                {
                    simButton.serializedComponentList.Add(item.GetName());
                    serializedEditor.SetSerializedItemOnEditor(item, simButton.componentPanel, inspectorPanel, simButton.simObject.objectData.GetSerializedComponents().Length);
                }

                hierarchySimButList.Add(simButton);

            }
            else
            {
                if (lastHierarchyButtonIdx != -1)
                {
                    HierarchySimButton simButtonOld = (HierarchySimButton)control[lastHierarchyButtonIdx];
                    simButtonOld.componentPanel.Visible = false;
                    simButtonOld.componentPanel.Enabled = false;
                }

                simButton.componentPanel.Visible = true;
                simButton.componentPanel.Enabled = true;
            }
            lastHierarchyButtonIdx = idx;
        }

        private void addComponentButton_Click(object sender, EventArgs e, HierarchySimButton simButton) //Inspectorde component eklemeyen button
        {
            Button[] buttons = new Button[SerializedComponentPool.SerializedCompTypes.Count];
            int idx = 0;
            foreach (var item in SerializedComponentPool.SerializedCompTypes)
            {
                buttons[idx] = new Button();
                buttons[idx].Location = new Point(100, (idx * 20) + simButton.componentPanel.TotalInspectorPanelHeight);
                buttons[idx].Size = new Size(100, 20);
                buttons[idx].Text = SerializedComponentPool.SerializedCompNames[item.Key];
                buttons[idx].BackColor = Color.White;
                buttons[idx].BringToFront();
                simButton.componentPanel.Controls.Add(buttons[idx]);
                buttons[idx].Click += (sender2, e2) => componentsButton_Click(sender2, e2, simButton, item.Key, buttons);

                idx++;
            }

        }

        private void componentsButton_Click(object sender, EventArgs e, HierarchySimButton simButton, int idx, Button[] buttons) //Inspectorde componentleri göstermeye yarayan button
        {
            foreach (var button in buttons)
            {
                button.Visible = false;
            }
            SerializedComponent serializedComponent = SerializedComponentPool.ReturnNewComponentFromList(idx);



            if (simButton.serializedComponentList.Contains(serializedComponent.GetName()) == false)
            {
                serializedEditor.SetSerializedItemOnEditor(serializedComponent, simButton.componentPanel, inspectorPanel, simButton.simObject.objectData.GetSerializedComponents().Length);
                simButton.serializedComponentList.Add(serializedComponent.GetName());
            }
            else
            {
                MessageBox.Show("You cannot add one more " + serializedComponent.GetName() + " Component");
            }

            EditorEventListenSystem.eventManager.SendEvent(new OnEditorAddCompSimObjEvent {
                simObject = simButton.simObject,
                serializedComponent = serializedComponent,
            });

            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh { });
        }

        #endregion

    }



}
