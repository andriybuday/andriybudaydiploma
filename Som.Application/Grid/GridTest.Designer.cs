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
            this.bufferedControlGrid = new Som.Application.Grid.BufferedControl();
            this.SuspendLayout();
            // 
            // buttonInitialize
            // 
            this.buttonInitialize.Location = new System.Drawing.Point(12, 184);
            this.buttonInitialize.Name = "buttonInitialize";
            this.buttonInitialize.Size = new System.Drawing.Size(75, 23);
            this.buttonInitialize.TabIndex = 1;
            this.buttonInitialize.Text = "Initialize";
            this.buttonInitialize.UseVisualStyleBackColor = true;
            this.buttonInitialize.Click += new System.EventHandler(this.buttonInitialize_Click);
            // 
            // buttonLearn
            // 
            this.buttonLearn.Location = new System.Drawing.Point(12, 217);
            this.buttonLearn.Name = "buttonLearn";
            this.buttonLearn.Size = new System.Drawing.Size(75, 23);
            this.buttonLearn.TabIndex = 2;
            this.buttonLearn.Text = "Learn";
            this.buttonLearn.UseVisualStyleBackColor = true;
            this.buttonLearn.Click += new System.EventHandler(this.buttonLearn_Click);
            // 
            // labelIteration
            // 
            this.labelIteration.AutoSize = true;
            this.labelIteration.Location = new System.Drawing.Point(12, 258);
            this.labelIteration.Name = "labelIteration";
            this.labelIteration.Size = new System.Drawing.Size(35, 13);
            this.labelIteration.TabIndex = 3;
            this.labelIteration.Text = "label1";
            // 
            // bufferedControlGrid
            // 
            this.bufferedControlGrid.Dirty = false;
            this.bufferedControlGrid.Location = new System.Drawing.Point(195, 12);
            this.bufferedControlGrid.Name = "bufferedControlGrid";
            this.bufferedControlGrid.Size = new System.Drawing.Size(600, 600);
            this.bufferedControlGrid.TabIndex = 4;
            // 
            // GridTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 622);
            this.Controls.Add(this.bufferedControlGrid);
            this.Controls.Add(this.labelIteration);
            this.Controls.Add(this.buttonLearn);
            this.Controls.Add(this.buttonInitialize);
            this.Name = "GridTest";
            this.Text = "GridTest";
            this.Load += new System.EventHandler(this.GridTest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonInitialize;
        private System.Windows.Forms.Button buttonLearn;
        private System.Windows.Forms.Label labelIteration;
        private BufferedControl bufferedControlGrid;
    }
}