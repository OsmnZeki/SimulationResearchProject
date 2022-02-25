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

namespace SimulationWFA
{
    public partial class SimulationProject : Form
    {
        public SimulationProject()
        {
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
                //Directory.GetParent(Directory.GetCurrentDirectory());
                foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory()))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(item);
                    var node = projectsTreeView.Nodes.Add(directoryInfo.Name, directoryInfo.Name, 0, 0);
                    node.Tag = directoryInfo;
                    node.ForeColor = Color.FromArgb(255, 255, 255);
                }

                foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory()))
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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.ToString() == "New")
            {
                MessageBox.Show("New");
            }
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simulateButton_Click(object sender, EventArgs e)
        {

        }

        private void projectsTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
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
                string nodePath = null;
                //Process proc = Process.Start(((FileInfo)e.Node.Tag).FullName);
                //proc.WaitForInputIdle();

                //while (proc.MainWindowHandle == IntPtr.Zero)
                //{
                //    Thread.Sleep(1000);
                //    proc.Refresh();
                //}

                //SetParent(proc.MainWindowHandle, this.Handle);
                nodePath = ((FileInfo)e.Node.Tag).FullName;
                MessageBox.Show(nodePath);
                e.Node.BackColor = Color.FromArgb(255, 255, 255);
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = ((FileInfo)e.Node.Tag).FullName;
                info.UseShellExecute = true;
                Process process = Process.Start(info);

                Thread.Sleep(2000);

                //SetParent(process.MainWindowHandle, this.Handle);

                //open file
            }
        }

        private void addObjectButton_Click(object sender, EventArgs e)
        {
            var simObject = SimObject.NewSimObject();
            HierarchySimButton hierarchyButton = new HierarchySimButton();
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorCreateSimObjEvent { simObject = simObject });
            hierarchyButton.Location = new Point(10, 30); //new System.Drawing.Point(hieararchyPanel.Location.X + 5, hieararchyPanel.Location.Y + 40);
            hierarchyButton.Name = "hierarchyButton";
            hierarchyButton.Size = new System.Drawing.Size(75, 23);
            hierarchyButton.Text = simObject.objectData.name;
            hierarchyButton.BackColor = Color.White;
            hierarchyButton.simObject = simObject;
            hierarchyButton.BringToFront();
            hierarchyButton.Click += new System.EventHandler(hierarchyButton_Click);
            hieararchyPanel.Controls.Add(hierarchyButton);

        }
        private void hierarchyButton_Click(object sender, EventArgs e)
        {
            Control[] control = Controls.Find("hierarchyButton", true);
            HierarchySimButton simButton = (HierarchySimButton)control[0];

            Panel componentPanel = new Panel();
            componentPanel.Location = new Point(5, 30);
            componentPanel.Name = control[0].Name + "Panel";
            componentPanel.Size = new System.Drawing.Size(180, 200);
            componentPanel.BackColor = Color.DarkSalmon;

            SerializedEditor serializedEditor = new SerializedEditor();

            foreach (var item in simButton.simObject.objectData.GetSerializedComponents())
            {
                serializedEditor.SetSerializedItemOnEditor(item, componentPanel, inspectorPanel, simButton.simObject.objectData.GetSerializedComponents().Length);
            }

            Button addComponentButton = new Button();
            addComponentButton.Location = new Point(40, 180);
            addComponentButton.Size = new Size(100, 20);
            addComponentButton.BackColor = Color.White;
            addComponentButton.Text = "Add Component";
            addComponentButton.Name = "AddComponentButton";
            addComponentButton.Click += (sender2, e2) => addComponentButton_Click(sender2, e2, componentPanel); // new System.EventHandler(addComponentButton_Click);
            addComponentButton.BringToFront();
            componentPanel.Controls.Add(addComponentButton);

            inspectorPanel.Controls.Add(componentPanel);
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



        private void addComponentButton_Click(object sender, EventArgs e, Panel panel)
        {
            Button[] buttons = new Button[SerializedComponentPool.SerializedCompTypes.Count];
            int idx = 0;
            foreach (var item in SerializedComponentPool.SerializedCompTypes)
            {
                buttons[idx] = new Button();
                buttons[idx].Location = new Point(40, 140 - (idx * 20));
                buttons[idx].Size = new Size(100, 20);
                buttons[idx].Text = item.Value.GetName();
                buttons[idx].BackColor = Color.White;
                buttons[idx].BringToFront();
                panel.Controls.Add(buttons[idx]);
                buttons[idx].Click += (sender2, e2) => componentsButton_Click(sender2, e2, item.Key);

                idx++;
            }
        }

        private void componentsButton_Click(object sender, EventArgs e, int idx)
        {
            Control[] control = Controls.Find("hierarchyButton", true);
            HierarchySimButton simButton = (HierarchySimButton)control[0];

            EditorEventListenSystem.eventManager.SendEvent(new OnEditorAddCompSimObjEvent {
                simObject = simButton.simObject,
                serializedComponent = SerializedComponentPool.ReturnNewComponentFromList(idx),
            });

        }

        private void Hierarchy_Click(object sender, EventArgs e)
        {

        }

        #endregion

        //private void refresh_Click(object sender, EventArgs e)
        //{

        //}


    }



}
