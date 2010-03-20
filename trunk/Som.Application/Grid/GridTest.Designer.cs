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
            this.buttonInitialize = new System.Windows.Forms.Button();
            this.buttonLearn = new System.Windows.Forms.Button();
            this.labelIteration = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.numericUpDownIterationsPerOnce = new System.Windows.Forms.NumericUpDown();
            this.somFactoryUI = new Som.Application.SomExtensions.SomFactoryUI();
            this.bufferedControlGrid = new Som.Application.Grid.BufferedControl();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIterationsPerOnce)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonInitialize
            // 
            this.buttonInitialize.Location = new System.Drawing.Point(22, 296);
            this.buttonInitialize.Name = "buttonInitialize";
            this.buttonInitialize.Size = new System.Drawing.Size(125, 23);
            this.buttonInitialize.TabIndex = 1;
            this.buttonInitialize.Text = "Initialize";
            this.buttonInitialize.UseVisualStyleBackColor = true;
            this.buttonInitialize.Click += new System.EventHandler(this.buttonInitialize_Click);
            // 
            // buttonLearn
            // 
            this.buttonLearn.Location = new System.Drawing.Point(22, 367);
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
            this.labelIteration.Location = new System.Drawing.Point(153, 333);
            this.labelIteration.Name = "labelIteration";
            this.labelIteration.Size = new System.Drawing.Size(13, 13);
            this.labelIteration.TabIndex = 3;
            this.labelIteration.Text = "0";
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(72, 328);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 6;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // numericUpDownIterationsPerOnce
            // 
            this.numericUpDownIterationsPerOnce.Location = new System.Drawing.Point(22, 328);
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
            // somFactoryUI
            // 
            this.somFactoryUI.Location = new System.Drawing.Point(12, 12);
            this.somFactoryUI.Name = "somFactoryUI";
            this.somFactoryUI.Size = new System.Drawing.Size(236, 265);
            this.somFactoryUI.TabIndex = 8;
            // 
            // bufferedControlGrid
            // 
            this.bufferedControlGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bufferedControlGrid.Dirty = false;
            this.bufferedControlGrid.Location = new System.Drawing.Point(270, 21);
            this.bufferedControlGrid.Name = "bufferedControlGrid";
            this.bufferedControlGrid.Size = new System.Drawing.Size(400, 400);
            this.bufferedControlGrid.TabIndex = 4;
            // 
            // GridTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 440);
            this.Controls.Add(this.somFactoryUI);
            this.Controls.Add(this.numericUpDownIterationsPerOnce);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.bufferedControlGrid);
            this.Controls.Add(this.labelIteration);
            this.Controls.Add(this.buttonLearn);
            this.Controls.Add(this.buttonInitialize);
            this.Name = "GridTest";
            this.Text = "GridTest";
            this.Load += new System.EventHandler(this.GridTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIterationsPerOnce)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonInitialize;
        private System.Windows.Forms.Button buttonLearn;
        private System.Windows.Forms.Label labelIteration;
        private BufferedControl bufferedControlGrid;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.NumericUpDown numericUpDownIterationsPerOnce;
        private SomExtensions.SomFactoryUI somFactoryUI;
    }
}