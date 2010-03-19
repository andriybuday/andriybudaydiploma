namespace Som.Application
{
    partial class SomMain
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
            this.buttonGridTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonGridTest
            // 
            this.buttonGridTest.Location = new System.Drawing.Point(12, 12);
            this.buttonGridTest.Name = "buttonGridTest";
            this.buttonGridTest.Size = new System.Drawing.Size(75, 23);
            this.buttonGridTest.TabIndex = 0;
            this.buttonGridTest.Text = "Grid Test";
            this.buttonGridTest.UseVisualStyleBackColor = true;
            this.buttonGridTest.Click += new System.EventHandler(this.buttonGridTest_Click);
            // 
            // SomMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 245);
            this.Controls.Add(this.buttonGridTest);
            this.Name = "SomMain";
            this.Text = "SomMain";
            this.Shown += new System.EventHandler(this.SomMain_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonGridTest;
    }
}