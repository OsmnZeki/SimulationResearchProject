using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.IO;


namespace SimulationWFA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ShowProjectFiles();

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sender.ToString() == "New")
            {
                MessageBox.Show("New");
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void simulateButton_Click(object sender, EventArgs e)
        {

        }


        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private void ShowProjectFiles()
        {
            Cursor.Current = Cursors.WaitCursor;
            projectsTreeView.Nodes.Clear();
            if (folderBrowserDialog1.SelectedPath == "")
            {
                foreach (var item in Directory.GetDirectories("D://SimulationResearchProject"/*folderBrowserDialog1.SelectedPath*/))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(item);
                    var node = projectsTreeView.Nodes.Add(directoryInfo.Name, directoryInfo.Name, 0, 0);
                    node.Tag = directoryInfo;
                    node.ForeColor = Color.FromArgb(255, 255, 255);
                }

                foreach (var item in Directory.GetFiles("D://SimulationResearchProject"))
                {
                    FileInfo fileInfo = new FileInfo(item);
                    var node = projectsTreeView.Nodes.Add(fileInfo.Name, fileInfo.Name, 1, 1);
                    node.Tag = fileInfo;
                    node.ForeColor = Color.FromArgb(255, 255, 255);
                }

                Cursor.Current = Cursors.Default;
            }

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

        private void Form1_Load(object sender, EventArgs e)
        {

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            MessageBox.Show("TextChanged");
        }
    }



}
