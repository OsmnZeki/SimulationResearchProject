
using System.Drawing;

namespace SimulationWFA
{
    partial class SimulationProject
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimulationProject));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findAndReplaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.codeDesignerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutSimulationProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ımageList1 = new System.Windows.Forms.ImageList(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.projectsPanel = new System.Windows.Forms.Panel();
            this.projectsTreeView = new System.Windows.Forms.TreeView();
            this.projectsLabel = new System.Windows.Forms.Label();
            this.hieararchyPanel = new System.Windows.Forms.Panel();
            this.Hierarchy = new System.Windows.Forms.Button();
            this.addObjectButton = new System.Windows.Forms.Button();
            this.hierarchyLabel = new System.Windows.Forms.Label();
            this.simulationWindowPanel = new System.Windows.Forms.Panel();
            this.simulationWindowLabel = new System.Windows.Forms.Label();
            this.simulateButton = new System.Windows.Forms.Button();
            this.inspectorLabel = new System.Windows.Forms.Label();
            this.inspectorPanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.projectsPanel.SuspendLayout();
            this.hieararchyPanel.SuspendLayout();
            this.simulationWindowPanel.SuspendLayout();
            this.inspectorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Azure;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            resources.ApplyResources(this.exitToolStripMenuItem1, "exitToolStripMenuItem1");
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.findAndReplaceToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            resources.ApplyResources(this.undoToolStripMenuItem, "undoToolStripMenuItem");
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            resources.ApplyResources(this.redoToolStripMenuItem, "redoToolStripMenuItem");
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            resources.ApplyResources(this.cutToolStripMenuItem, "cutToolStripMenuItem");
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            resources.ApplyResources(this.pasteToolStripMenuItem, "pasteToolStripMenuItem");
            // 
            // findAndReplaceToolStripMenuItem
            // 
            this.findAndReplaceToolStripMenuItem.Name = "findAndReplaceToolStripMenuItem";
            resources.ApplyResources(this.findAndReplaceToolStripMenuItem, "findAndReplaceToolStripMenuItem");
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.codeDesignerToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            // 
            // codeDesignerToolStripMenuItem
            // 
            this.codeDesignerToolStripMenuItem.Name = "codeDesignerToolStripMenuItem";
            resources.ApplyResources(this.codeDesignerToolStripMenuItem, "codeDesignerToolStripMenuItem");
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutSimulationProjectToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // aboutSimulationProjectToolStripMenuItem
            // 
            this.aboutSimulationProjectToolStripMenuItem.Name = "aboutSimulationProjectToolStripMenuItem";
            resources.ApplyResources(this.aboutSimulationProjectToolStripMenuItem, "aboutSimulationProjectToolStripMenuItem");
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ımageList1
            // 
            this.ımageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ımageList1.ImageStream")));
            this.ımageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ımageList1.Images.SetKeyName(0, "folder.png");
            this.ımageList1.Images.SetKeyName(1, "coding.png");
            // 
            // projectsPanel
            // 
            resources.ApplyResources(this.projectsPanel, "projectsPanel");
            this.projectsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(27)))), ((int)(((byte)(29)))));
            this.projectsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.projectsPanel.Controls.Add(this.projectsTreeView);
            this.projectsPanel.Controls.Add(this.projectsLabel);
            this.projectsPanel.Name = "projectsPanel";
            // 
            // projectsTreeView
            // 
            resources.ApplyResources(this.projectsTreeView, "projectsTreeView");
            this.projectsTreeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(53)))), ((int)(((byte)(54)))));
            this.projectsTreeView.ImageList = this.ımageList1;
            this.projectsTreeView.Name = "projectsTreeView";
            this.projectsTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.projectsTreeView_NodeMouseDoubleClick);
            // 
            // projectsLabel
            // 
            resources.ApplyResources(this.projectsLabel, "projectsLabel");
            this.projectsLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.projectsLabel.Name = "projectsLabel";
            // 
            // hieararchyPanel
            // 
            resources.ApplyResources(this.hieararchyPanel, "hieararchyPanel");
            this.hieararchyPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(27)))), ((int)(((byte)(29)))));
            this.hieararchyPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.hieararchyPanel.Controls.Add(this.Hierarchy);
            this.hieararchyPanel.Controls.Add(this.addObjectButton);
            this.hieararchyPanel.Controls.Add(this.hierarchyLabel);
            this.hieararchyPanel.Name = "hieararchyPanel";
            // 
            // Hierarchy
            // 
            this.Hierarchy.ForeColor = System.Drawing.SystemColors.ControlLight;
            resources.ApplyResources(this.Hierarchy, "Hierarchy");
            this.Hierarchy.Name = "Hierarchy";
            // 
            // addObjectButton
            // 
            resources.ApplyResources(this.addObjectButton, "addObjectButton");
            this.addObjectButton.Name = "addObjectButton";
            this.addObjectButton.UseVisualStyleBackColor = true;
            this.addObjectButton.Click += new System.EventHandler(this.addObjectButton_Click);
            // 
            // hierarchyLabel
            // 
            resources.ApplyResources(this.hierarchyLabel, "hierarchyLabel");
            this.hierarchyLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.hierarchyLabel.Name = "hierarchyLabel";
            // 
            // simulationWindowPanel
            // 
            this.simulationWindowPanel.AllowDrop = true;
            resources.ApplyResources(this.simulationWindowPanel, "simulationWindowPanel");
            this.simulationWindowPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(27)))), ((int)(((byte)(29)))));
            this.simulationWindowPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.simulationWindowPanel.Controls.Add(this.simulationWindowLabel);
            this.simulationWindowPanel.Controls.Add(this.simulateButton);
            this.simulationWindowPanel.Name = "simulationWindowPanel";
            // 
            // simulationWindowLabel
            // 
            resources.ApplyResources(this.simulationWindowLabel, "simulationWindowLabel");
            this.simulationWindowLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.simulationWindowLabel.Name = "simulationWindowLabel";
            // 
            // simulateButton
            // 
            resources.ApplyResources(this.simulateButton, "simulateButton");
            this.simulateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.simulateButton.Name = "simulateButton";
            this.simulateButton.UseVisualStyleBackColor = false;
            this.simulateButton.Click += new System.EventHandler(this.simulateButton_Click);
            // 
            // inspectorLabel
            // 
            resources.ApplyResources(this.inspectorLabel, "inspectorLabel");
            this.inspectorLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.inspectorLabel.Name = "inspectorLabel";
            // 
            // inspectorPanel
            // 
            resources.ApplyResources(this.inspectorPanel, "inspectorPanel");
            this.inspectorPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(27)))), ((int)(((byte)(29)))));
            this.inspectorPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.inspectorPanel.Controls.Add(this.inspectorLabel);
            this.inspectorPanel.Name = "inspectorPanel";
            // 
            // SimulationProject
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.Controls.Add(this.simulationWindowPanel);
            this.Controls.Add(this.inspectorPanel);
            this.Controls.Add(this.hieararchyPanel);
            this.Controls.Add(this.projectsPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SimulationProject";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.projectsPanel.ResumeLayout(false);
            this.projectsPanel.PerformLayout();
            this.hieararchyPanel.ResumeLayout(false);
            this.hieararchyPanel.PerformLayout();
            this.simulationWindowPanel.ResumeLayout(false);
            this.simulationWindowPanel.PerformLayout();
            this.inspectorPanel.ResumeLayout(false);
            this.inspectorPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findAndReplaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem codeDesignerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutSimulationProjectToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ImageList ımageList1;
        private System.Windows.Forms.Panel projectsPanel;
        private System.Windows.Forms.TreeView projectsTreeView;
        private System.Windows.Forms.Label projectsLabel;
        private System.Windows.Forms.Panel hieararchyPanel;
        private System.Windows.Forms.Label hierarchyLabel;
        private System.Windows.Forms.Panel simulationWindowPanel;
        private System.Windows.Forms.Label simulationWindowLabel;
        private System.Windows.Forms.Button simulateButton;
        private System.Windows.Forms.Button addObjectButton;
        private System.Windows.Forms.Button Hierarchy;
        private System.Windows.Forms.Label inspectorLabel;
        private System.Windows.Forms.Panel inspectorPanel;
    }
}

