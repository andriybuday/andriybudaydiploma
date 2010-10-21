namespace Som.Application.Grid
{
    partial class GridTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GridTest));
            this.buttonInitialize = new System.Windows.Forms.Button();
            this.buttonLearn = new System.Windows.Forms.Button();
            this.labelIteration = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.numericUpDownIterationsPerOnce = new System.Windows.Forms.NumericUpDown();
            this.checkBoxShowGirl = new System.Windows.Forms.CheckBox();
            this.labelForTime = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelPrevTime = new System.Windows.Forms.Label();
            this.buttonRun = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonStop = new System.Windows.Forms.Button();
            this._splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.somFactoryUI = new Som.Application.SomExtensions.SomFactoryUI();
            this.bufferedControlGrid = new Som.Application.Grid.BufferedControl();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIterationsPerOnce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainerMain)).BeginInit();
            this._splitContainerMain.Panel1.SuspendLayout();
            this._splitContainerMain.Panel2.SuspendLayout();
            this._splitContainerMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonInitialize
            // 
            this.buttonInitialize.Location = new System.Drawing.Point(13, 272);
            this.buttonInitialize.Name = "buttonInitialize";
            this.buttonInitialize.Size = new System.Drawing.Size(125, 23);
            this.buttonInitialize.TabIndex = 1;
            this.buttonInitialize.Text = "Initialize";
            this.buttonInitialize.UseVisualStyleBackColor = true;
            this.buttonInitialize.Click += new System.EventHandler(this.buttonInitialize_Click);
            // 
            // buttonLearn
            // 
            this.buttonLearn.Location = new System.Drawing.Point(13, 336);
            this.buttonLearn.Name = "buttonLearn";
            this.buttonLearn.Size = new System.Drawing.Size(125, 23);
            this.buttonLearn.TabIndex = 2;
            this.buttonLearn.Text = "Learn";
            this.buttonLearn.UseVisualStyleBackColor = true;
            this.buttonLearn.Click += new System.EventHandler(this.buttonLearn_Click);
            // 
            // labelIteration
            // 
            this.labelIteration.AutoSize = true;
            this.labelIteration.Location = new System.Drawing.Point(144, 309);
            this.labelIteration.Name = "labelIteration";
            this.labelIteration.Size = new System.Drawing.Size(13, 13);
            this.labelIteration.TabIndex = 3;
            this.labelIteration.Text = "0";
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(63, 304);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 6;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // numericUpDownIterationsPerOnce
            // 
            this.numericUpDownIterationsPerOnce.Location = new System.Drawing.Point(13, 305);
            this.numericUpDownIterationsPerOnce.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownIterationsPerOnce.Name = "numericUpDownIterationsPerOnce";
            this.numericUpDownIterationsPerOnce.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownIterationsPerOnce.TabIndex = 7;
            this.numericUpDownIterationsPerOnce.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // checkBoxShowGirl
            // 
            this.checkBoxShowGirl.AutoSize = true;
            this.checkBoxShowGirl.Checked = true;
            this.checkBoxShowGirl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowGirl.Location = new System.Drawing.Point(145, 277);
            this.checkBoxShowGirl.Name = "checkBoxShowGirl";
            this.checkBoxShowGirl.Size = new System.Drawing.Size(73, 17);
            this.checkBoxShowGirl.TabIndex = 9;
            this.checkBoxShowGirl.Text = "Silhouette";
            this.checkBoxShowGirl.UseVisualStyleBackColor = true;
            this.checkBoxShowGirl.CheckedChanged += new System.EventHandler(this.checkBoxShowGirl_CheckedChanged);
            // 
            // labelForTime
            // 
            this.labelForTime.AutoSize = true;
            this.labelForTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelForTime.ForeColor = System.Drawing.Color.Maroon;
            this.labelForTime.Location = new System.Drawing.Point(12, 386);
            this.labelForTime.Name = "labelForTime";
            this.labelForTime.Size = new System.Drawing.Size(63, 24);
            this.labelForTime.TabIndex = 10;
            this.labelForTime.Text = "Time:";
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTime.ForeColor = System.Drawing.Color.Maroon;
            this.labelTime.Location = new System.Drawing.Point(62, 386);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(0, 24);
            this.labelTime.TabIndex = 11;
            // 
            // labelPrevTime
            // 
            this.labelPrevTime.AutoSize = true;
            this.labelPrevTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPrevTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.labelPrevTime.Location = new System.Drawing.Point(60, 413);
            this.labelPrevTime.Name = "labelPrevTime";
            this.labelPrevTime.Size = new System.Drawing.Size(0, 16);
            this.labelPrevTime.TabIndex = 12;
            // 
            // buttonRun
            // 
            this.buttonRun.ForeColor = System.Drawing.Color.Green;
            this.buttonRun.Location = new System.Drawing.Point(13, 365);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(54, 23);
            this.buttonRun.TabIndex = 13;
            this.buttonRun.Text = "Run";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonStop
            // 
            this.buttonStop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonStop.Location = new System.Drawing.Point(84, 365);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(54, 23);
            this.buttonStop.TabIndex = 14;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // _splitContainerMain
            // 
            this._splitContainerMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this._splitContainerMain.Name = "_splitContainerMain";
            // 
            // _splitContainerMain.Panel1
            // 
            this._splitContainerMain.Panel1.Controls.Add(this.somFactoryUI);
            this._splitContainerMain.Panel1.Controls.Add(this.buttonStop);
            this._splitContainerMain.Panel1.Controls.Add(this.buttonInitialize);
            this._splitContainerMain.Panel1.Controls.Add(this.buttonRun);
            this._splitContainerMain.Panel1.Controls.Add(this.buttonLearn);
            this._splitContainerMain.Panel1.Controls.Add(this.labelPrevTime);
            this._splitContainerMain.Panel1.Controls.Add(this.labelIteration);
            this._splitContainerMain.Panel1.Controls.Add(this.labelTime);
            this._splitContainerMain.Panel1.Controls.Add(this.buttonNext);
            this._splitContainerMain.Panel1.Controls.Add(this.labelForTime);
            this._splitContainerMain.Panel1.Controls.Add(this.numericUpDownIterationsPerOnce);
            this._splitContainerMain.Panel1.Controls.Add(this.checkBoxShowGirl);
            this._splitContainerMain.Panel1MinSize = 240;
            // 
            // _splitContainerMain.Panel2
            // 
            this._splitContainerMain.Panel2.Controls.Add(this.bufferedControlGrid);
            this._splitContainerMain.Panel2MinSize = 400;
            this._splitContainerMain.Size = new System.Drawing.Size(670, 435);
            this._splitContainerMain.SplitterDistance = 240;
            this._splitContainerMain.TabIndex = 15;
            // 
            // somFactoryUI
            // 
            this.somFactoryUI.LearningDataProvider = null;
            this.somFactoryUI.Location = new System.Drawing.Point(3, 3);
            this.somFactoryUI.MetricFunction = null;
            this.somFactoryUI.Name = "somFactoryUI";
            this.somFactoryUI.Size = new System.Drawing.Size(236, 265);
            this.somFactoryUI.TabIndex = 8;
            // 
            // bufferedControlGrid
            // 
            this.bufferedControlGrid.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.bufferedControlGrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bufferedControlGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bufferedControlGrid.Dirty = false;
            this.bufferedControlGrid.Location = new System.Drawing.Point(11, 18);
            this.bufferedControlGrid.Name = "bufferedControlGrid";
            this.bufferedControlGrid.Size = new System.Drawing.Size(400, 400);
            this.bufferedControlGrid.TabIndex = 4;
            // 
            // GridTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 435);
            this.Controls.Add(this._splitContainerMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(686, 473);
            this.Name = "GridTest";
            this.Text = "Testing SOM (visualization of learning process)";
            this.Load += new System.EventHandler(this.GridTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIterationsPerOnce)).EndInit();
            this._splitContainerMain.Panel1.ResumeLayout(false);
            this._splitContainerMain.Panel1.PerformLayout();
            this._splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainerMain)).EndInit();
            this._splitContainerMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonInitialize;
        private System.Windows.Forms.Button buttonLearn;
        private System.Windows.Forms.Label labelIteration;
        private BufferedControl bufferedControlGrid;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.NumericUpDown numericUpDownIterationsPerOnce;
        private SomExtensions.SomFactoryUI somFactoryUI;
        private System.Windows.Forms.CheckBox checkBoxShowGirl;
        private System.Windows.Forms.Label labelForTime;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelPrevTime;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.SplitContainer _splitContainerMain;
    }
}