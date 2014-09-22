﻿namespace VoronoiMap {
    sealed partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.splitPanel = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.cbCircles = new System.Windows.Forms.ComboBox();
            this.btnAnimate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.chDebug = new System.Windows.Forms.CheckBox();
            this.btnStepTo = new System.Windows.Forms.Button();
            this.nudStepTo = new System.Windows.Forms.NumericUpDown();
            this.btnInitialize = new System.Windows.Forms.Button();
            this.btnStepVoronoi = new System.Windows.Forms.Button();
            this.nudSeed = new System.Windows.Forms.NumericUpDown();
            this.chkShowEdges = new System.Windows.Forms.CheckBox();
            this.chkShowVertices = new System.Windows.Forms.CheckBox();
            this.chkShowSites = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudNumRegions = new System.Windows.Forms.NumericUpDown();
            this.btnRegen = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.nudRelax = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel)).BeginInit();
            this.splitPanel.Panel1.SuspendLayout();
            this.splitPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStepTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumRegions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRelax)).BeginInit();
            this.SuspendLayout();
            // 
            // splitPanel
            // 
            this.splitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitPanel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitPanel.IsSplitterFixed = true;
            this.splitPanel.Location = new System.Drawing.Point(0, 0);
            this.splitPanel.Name = "splitPanel";
            this.splitPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitPanel.Panel1
            // 
            this.splitPanel.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitPanel.Panel1.Controls.Add(this.nudRelax);
            this.splitPanel.Panel1.Controls.Add(this.label4);
            this.splitPanel.Panel1.Controls.Add(this.label3);
            this.splitPanel.Panel1.Controls.Add(this.cbCircles);
            this.splitPanel.Panel1.Controls.Add(this.btnAnimate);
            this.splitPanel.Panel1.Controls.Add(this.label2);
            this.splitPanel.Panel1.Controls.Add(this.chDebug);
            this.splitPanel.Panel1.Controls.Add(this.btnStepTo);
            this.splitPanel.Panel1.Controls.Add(this.nudStepTo);
            this.splitPanel.Panel1.Controls.Add(this.btnInitialize);
            this.splitPanel.Panel1.Controls.Add(this.btnStepVoronoi);
            this.splitPanel.Panel1.Controls.Add(this.nudSeed);
            this.splitPanel.Panel1.Controls.Add(this.chkShowEdges);
            this.splitPanel.Panel1.Controls.Add(this.chkShowVertices);
            this.splitPanel.Panel1.Controls.Add(this.chkShowSites);
            this.splitPanel.Panel1.Controls.Add(this.label1);
            this.splitPanel.Panel1.Controls.Add(this.nudNumRegions);
            this.splitPanel.Panel1.Controls.Add(this.btnRegen);
            // 
            // splitPanel.Panel2
            // 
            this.splitPanel.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitPanel.Size = new System.Drawing.Size(842, 600);
            this.splitPanel.SplitterDistance = 69;
            this.splitPanel.SplitterWidth = 1;
            this.splitPanel.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(331, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Step:";
            // 
            // cbCircles
            // 
            this.cbCircles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCircles.FormattingEnabled = true;
            this.cbCircles.Items.AddRange(new object[] {
            "None",
            "Circles",
            "Triangles"});
            this.cbCircles.Location = new System.Drawing.Point(609, 10);
            this.cbCircles.Name = "cbCircles";
            this.cbCircles.Size = new System.Drawing.Size(93, 21);
            this.cbCircles.TabIndex = 15;
            this.cbCircles.SelectedIndexChanged += new System.EventHandler(this.cbCircles_SelectedIndexChanged);
            // 
            // btnAnimate
            // 
            this.btnAnimate.Location = new System.Drawing.Point(708, 38);
            this.btnAnimate.Name = "btnAnimate";
            this.btnAnimate.Size = new System.Drawing.Size(122, 23);
            this.btnAnimate.TabIndex = 14;
            this.btnAnimate.Text = "Animate";
            this.btnAnimate.UseVisualStyleBackColor = true;
            this.btnAnimate.Click += new System.EventHandler(this.btnAnimate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Seed:";
            // 
            // chDebug
            // 
            this.chDebug.AutoSize = true;
            this.chDebug.Location = new System.Drawing.Point(539, 12);
            this.chDebug.Name = "chDebug";
            this.chDebug.Size = new System.Drawing.Size(64, 17);
            this.chDebug.TabIndex = 12;
            this.chDebug.Text = "Debug?";
            this.chDebug.UseVisualStyleBackColor = true;
            // 
            // btnStepTo
            // 
            this.btnStepTo.Location = new System.Drawing.Point(498, 38);
            this.btnStepTo.Name = "btnStepTo";
            this.btnStepTo.Size = new System.Drawing.Size(76, 23);
            this.btnStepTo.TabIndex = 11;
            this.btnStepTo.Text = "Step to:";
            this.btnStepTo.UseVisualStyleBackColor = true;
            this.btnStepTo.Click += new System.EventHandler(this.btnStepTo_Click);
            // 
            // nudStepTo
            // 
            this.nudStepTo.Location = new System.Drawing.Point(372, 39);
            this.nudStepTo.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudStepTo.Name = "nudStepTo";
            this.nudStepTo.Size = new System.Drawing.Size(120, 20);
            this.nudStepTo.TabIndex = 10;
            // 
            // btnInitialize
            // 
            this.btnInitialize.Location = new System.Drawing.Point(180, 38);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size(61, 23);
            this.btnInitialize.TabIndex = 8;
            this.btnInitialize.Text = "Initialize";
            this.btnInitialize.UseVisualStyleBackColor = true;
            this.btnInitialize.Click += new System.EventHandler(this.btnInitialize_Click);
            // 
            // btnStepVoronoi
            // 
            this.btnStepVoronoi.Location = new System.Drawing.Point(580, 38);
            this.btnStepVoronoi.Name = "btnStepVoronoi";
            this.btnStepVoronoi.Size = new System.Drawing.Size(122, 23);
            this.btnStepVoronoi.TabIndex = 7;
            this.btnStepVoronoi.Text = "Step Graph";
            this.btnStepVoronoi.UseVisualStyleBackColor = true;
            this.btnStepVoronoi.Click += new System.EventHandler(this.btnStepVoronoi_Click);
            // 
            // nudSeed
            // 
            this.nudSeed.Location = new System.Drawing.Point(54, 39);
            this.nudSeed.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudSeed.Name = "nudSeed";
            this.nudSeed.Size = new System.Drawing.Size(120, 20);
            this.nudSeed.TabIndex = 6;
            // 
            // chkShowEdges
            // 
            this.chkShowEdges.AutoSize = true;
            this.chkShowEdges.Checked = true;
            this.chkShowEdges.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowEdges.Location = new System.Drawing.Point(441, 12);
            this.chkShowEdges.Name = "chkShowEdges";
            this.chkShowEdges.Size = new System.Drawing.Size(92, 17);
            this.chkShowEdges.TabIndex = 5;
            this.chkShowEdges.Text = "Show Edges?";
            this.chkShowEdges.UseVisualStyleBackColor = true;
            this.chkShowEdges.CheckedChanged += new System.EventHandler(this.chkShowEdges_CheckedChanged);
            // 
            // chkShowVertices
            // 
            this.chkShowVertices.AutoSize = true;
            this.chkShowVertices.Checked = true;
            this.chkShowVertices.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowVertices.Location = new System.Drawing.Point(335, 12);
            this.chkShowVertices.Name = "chkShowVertices";
            this.chkShowVertices.Size = new System.Drawing.Size(100, 17);
            this.chkShowVertices.TabIndex = 4;
            this.chkShowVertices.Text = "Show Vertices?";
            this.chkShowVertices.UseVisualStyleBackColor = true;
            this.chkShowVertices.CheckedChanged += new System.EventHandler(this.chkShowEdges_CheckedChanged);
            // 
            // chkShowSites
            // 
            this.chkShowSites.AutoSize = true;
            this.chkShowSites.Checked = true;
            this.chkShowSites.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowSites.Location = new System.Drawing.Point(244, 12);
            this.chkShowSites.Name = "chkShowSites";
            this.chkShowSites.Size = new System.Drawing.Size(85, 17);
            this.chkShowSites.TabIndex = 3;
            this.chkShowSites.Text = "Show Sites?";
            this.chkShowSites.UseVisualStyleBackColor = true;
            this.chkShowSites.CheckedChanged += new System.EventHandler(this.chkShowEdges_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Number of Regions:";
            // 
            // nudNumRegions
            // 
            this.nudNumRegions.Location = new System.Drawing.Point(118, 10);
            this.nudNumRegions.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudNumRegions.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNumRegions.Name = "nudNumRegions";
            this.nudNumRegions.Size = new System.Drawing.Size(120, 20);
            this.nudNumRegions.TabIndex = 1;
            this.nudNumRegions.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // btnRegen
            // 
            this.btnRegen.Location = new System.Drawing.Point(708, 9);
            this.btnRegen.Name = "btnRegen";
            this.btnRegen.Size = new System.Drawing.Size(122, 23);
            this.btnRegen.TabIndex = 0;
            this.btnRegen.Text = "Regenerate Graph";
            this.btnRegen.UseVisualStyleBackColor = true;
            this.btnRegen.Click += new System.EventHandler(this.btnRegen_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Relax:";
            // 
            // nudRelax
            // 
            this.nudRelax.Location = new System.Drawing.Point(290, 39);
            this.nudRelax.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudRelax.Name = "nudRelax";
            this.nudRelax.Size = new System.Drawing.Size(39, 20);
            this.nudRelax.TabIndex = 18;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(842, 600);
            this.Controls.Add(this.splitPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Voronoi";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.splitPanel.Panel1.ResumeLayout(false);
            this.splitPanel.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel)).EndInit();
            this.splitPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudStepTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNumRegions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRelax)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitPanel;
        private System.Windows.Forms.CheckBox chkShowEdges;
        private System.Windows.Forms.CheckBox chkShowVertices;
        private System.Windows.Forms.CheckBox chkShowSites;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudNumRegions;
        private System.Windows.Forms.Button btnRegen;
        private System.Windows.Forms.NumericUpDown nudSeed;
        private System.Windows.Forms.Button btnStepVoronoi;
        private System.Windows.Forms.Button btnInitialize;
        private System.Windows.Forms.Button btnStepTo;
        private System.Windows.Forms.NumericUpDown nudStepTo;
        private System.Windows.Forms.CheckBox chDebug;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAnimate;
        private System.Windows.Forms.ComboBox cbCircles;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudRelax;
        private System.Windows.Forms.Label label4;

    }
}