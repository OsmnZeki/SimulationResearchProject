﻿using System;
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

        private void insanButton_Click(object sender, EventArgs e)
        {
            Panel panel = new Panel();
            panel.Location = inspectorPanel.Location;
            panel.Name = "InsanPanel";
            panel.Size = new System.Drawing.Size(228, 200);
            panel.TabIndex = 0;
            panel.BackColor = Color.LightBlue;
            TextBox textBox1 = new TextBox();
            textBox1.Location = new Point(10, 10);
            textBox1.Text = "I am a TextBox5";
            textBox1.Size = new Size(200, 30);
            CheckBox checkBox1 = new CheckBox();
            checkBox1.Location = new Point(10, 50);
            checkBox1.Text = "Check Me";
            checkBox1.Size = new Size(200, 30);
            panel.Controls.Add(textBox1);
            panel.Controls.Add(checkBox1);

            Controls.Add(panel);
            //Insan insan = new Insan();

            //classLabel.Text = insan.ToString();
            //feature1.Text = "Name: ";
            //feature2.Text = "Surname: ";
            //feature3.Text = "Age: ";
            //feature4.Text = "Weight: ";
            //textBox1.Text = insan.Name;
            //textBox2.Text = insan.Surname;
            //textBox3.Text = insan.Age.ToString();
            //textBox4.Text = insan.Weight.ToString();
            //classesPanel.Visible = true;
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
                hierarchyButton.Click += (sender2, e2) => hierarchyButton_Click(sender2, e2);//new System.EventHandler(hierarchyButton_Click);
                hieararchyPanel.Controls.Add(hierarchyButton);
        }
        TextBox[] posText = new TextBox[3];
        dynamic parameters = new ExpandoObject();
        private void hierarchyButton_Click(object sender, EventArgs e)
        {
            Panel panel = new Panel();
            panel.Location = new Point(5, 30);
            Control[] control = Controls.Find("hierarchyButton", true);
            HierarchySimButton simButton = (HierarchySimButton)control[0];
            TextBox[] textBox = new TextBox[simButton.simObject.objectData.GetSerializedComponents().Length];

            int idx = 0;

            parameters = simButton.simObject.objectData.GetSerializedComponents();

            foreach (var item in simButton.simObject.objectData.GetSerializedComponents())
            {
                Console.WriteLine(item.GetType());
                textBox[idx] = new TextBox();
                textBox[idx].Location = new Point(0, 0);
                textBox[idx].Text = item.GetName();
                textBox[idx].BackColor = Color.Red;
                textBox[idx].Size = new Size(150, 60);
                textBox[idx].BringToFront();
                panel.Controls.Add(textBox[idx]);
                idx++;
            }

            for (int i = 0; i < posText.Length; i++)
            {
                posText[i] = new TextBox();
                posText[i].Location = new Point((i * 30), +20);
                posText[i].Text = parameters[0].pos.X.ToString();
                posText[i].BackColor = Color.Yellow;
                posText[i].Size = new Size(30, 60);
                posText[i].TextChanged += simulationProject_TextChanged;
                posText[i].BringToFront();
                panel.Controls.Add(posText[i]);
            }
            panel.Name = control[0].Name + "Panel";
            panel.Size = new System.Drawing.Size(180, 200);
            panel.BackColor = Color.DarkSalmon;

            Button addComponentButton = new Button();
            addComponentButton.Location = new Point(40, 180);
            addComponentButton.Size = new Size(100, 20);
            addComponentButton.BackColor = Color.White;
            addComponentButton.Text = "Add Component";
            addComponentButton.Name = "AddComponentButton";
            addComponentButton.Click += (sender2, e2) => addComponentButton_Click(sender2, e2, panel); // new System.EventHandler(addComponentButton_Click);
            addComponentButton.BringToFront();
            panel.Controls.Add(addComponentButton);

            inspectorPanel.Controls.Add(panel);
           
        }

        private void simulationProject_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            posText[textBox.TabIndex - 1].Text = textBox.Text;
            float a = float.Parse(posText[textBox.TabIndex - 1].Text);
            Console.WriteLine(a);
            parameters[0].pos.X = a;
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

        #endregion

        private void refresh_Click(object sender, EventArgs e)
        {
            EditorEventListenSystem.eventManager.SendEvent(new OnEditorRefresh {


            });
        }


    }



}
