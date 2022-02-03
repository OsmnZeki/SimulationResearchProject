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

namespace SimulationWFA
{
    public partial class Form1 : Form
    {
        public Form1()
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
            hierarchyButton.Location = new System.Drawing.Point(hieararchyPanel.Location.X + 5, hieararchyPanel.Location.Y + 40);
            hierarchyButton.Name = "hierarchyButton";
            hierarchyButton.Size = new System.Drawing.Size(75, 23);
            hierarchyButton.Text = simObject.objectData.name;
            hierarchyButton.BackColor = Color.White;
            hierarchyButton.simObject = simObject;
            hierarchyButton.Click += new System.EventHandler(hierarchyButton_Click);
            hieararchyPanel.SendToBack();
            Controls.Add(hierarchyButton);

        }

        private void hierarchyButton_Click(object sender, EventArgs e)
        {
            Panel panel = new Panel();
            panel.Location = new Point(inspectorPanel.Location.X + 5, inspectorPanel.Location.Y + 40); ;
            Control[] control = Controls.Find("hierarchyButton", true);
            HierarchySimButton simButton = (HierarchySimButton)control[0];
            TextBox[] textBox = new TextBox[simButton.simObject.objectData.serializedComponentList.Count];
            TextBox[] posText = new TextBox[3];
            int idx = 0;
            dynamic parameters = new ExpandoObject();
            parameters = simButton.simObject.objectData.serializedComponentList;

            foreach (var item in simButton.simObject.objectData.serializedComponentList)
            {
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
                posText[i].Location = new Point((i * 30), + 20);
                posText[i].Text = parameters[0].pos.X.ToString();
                posText[i].BackColor = Color.Yellow;
                posText[i].Size = new Size(30, 60);
                posText[i].BringToFront();
                panel.Controls.Add(posText[i]);
            }
            // panel.SendToBack();
            panel.Name = control[0].Name + "Panel";
            panel.Size = new System.Drawing.Size(180, 200);
            panel.BackColor = Color.White;
            inspectorPanel.SendToBack();

            Controls.Add(panel);

            //TextBox textBox1 = new TextBox();
            //textBox1.Location = new Point(10, 10);
            //textBox1.Text = "I am a TextBox5";
            //textBox1.Size = new Size(200, 30);
            //CheckBox checkBox1 = new CheckBox();
            //checkBox1.Location = new Point(10, 50);
            //checkBox1.Text = "Check Me";
            //checkBox1.Size = new Size(200, 30);
            //panel.Controls.Add(textBox1);
            //panel.Controls.Add(checkBox1);

            Controls.Add(panel);
        }
        
        #endregion
    }



}
