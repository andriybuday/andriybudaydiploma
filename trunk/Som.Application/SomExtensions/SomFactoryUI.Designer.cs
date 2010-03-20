namespace Som.Application.SomExtensions
{
    partial class SomFactoryUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxContainer = new System.Windows.Forms.GroupBox();
            this.textBoxLearningRate = new System.Windows.Forms.TextBox();
            this.numericUpDownIterations = new System.Windows.Forms.NumericUpDown();
            this.labelIterations = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownColumns = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRows = new System.Windows.Forms.NumericUpDown();
            this.labelTopology = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRows)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxContainer
            // 
            this.groupBoxContainer.Controls.Add(this.label3);
            this.groupBoxContainer.Controls.Add(this.textBoxLearningRate);
            this.groupBoxContainer.Controls.Add(this.numericUpDownIterations);
            this.groupBoxContainer.Controls.Add(this.labelIterations);
            this.groupBoxContainer.Controls.Add(this.label2);
            this.groupBoxContainer.Controls.Add(this.label1);
            this.groupBoxContainer.Controls.Add(this.numericUpDownColumns);
            this.groupBoxContainer.Controls.Add(this.numericUpDownRows);
            this.groupBoxContainer.Controls.Add(this.labelTopology);
            this.groupBoxContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxContainer.Location = new System.Drawing.Point(0, 0);
            this.groupBoxContainer.Name = "groupBoxContainer";
            this.groupBoxContainer.Size = new System.Drawing.Size(236, 265);
            this.groupBoxContainer.TabIndex = 0;
            this.groupBoxContainer.TabStop = false;
            this.groupBoxContainer.Text = "Self-Organizing Map Parameters";
            // 
            // textBoxLearningRate
            // 
            this.textBoxLearningRate.Location = new System.Drawing.Point(121, 172);
            this.textBoxLearningRate.Name = "textBoxLearningRate";
            this.textBoxLearningRate.Size = new System.Drawing.Size(100, 20);
            this.textBoxLearningRate.TabIndex = 7;
            this.textBoxLearningRate.Text = "0,07";
            // 
            // numericUpDownIterations
            // 
            this.numericUpDownIterations.Location = new System.Drawing.Point(72, 50);
            this.numericUpDownIterations.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownIterations.Name = "numericUpDownIterations";
            this.numericUpDownIterations.Size = new System.Drawing.Size(66, 20);
            this.numericUpDownIterations.TabIndex = 6;
            this.numericUpDownIterations.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // labelIterations
            // 
            this.labelIterations.AutoSize = true;
            this.labelIterations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelIterations.Location = new System.Drawing.Point(6, 53);
            this.labelIterations.Name = "labelIterations";
            this.labelIterations.Size = new System.Drawing.Size(60, 13);
            this.labelIterations.TabIndex = 5;
            this.labelIterations.Text = "Iterations";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Columns:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Rows:";
            // 
            // numericUpDownColumns
            // 
            this.numericUpDownColumns.Location = new System.Drawing.Point(171, 113);
            this.numericUpDownColumns.Name = "numericUpDownColumns";
            this.numericUpDownColumns.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownColumns.TabIndex = 2;
            this.numericUpDownColumns.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericUpDownRows
            // 
            this.numericUpDownRows.Location = new System.Drawing.Point(61, 113);
            this.numericUpDownRows.Name = "numericUpDownRows";
            this.numericUpDownRows.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownRows.TabIndex = 1;
            this.numericUpDownRows.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // labelTopology
            // 
            this.labelTopology.AutoSize = true;
            this.labelTopology.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTopology.Location = new System.Drawing.Point(6, 92);
            this.labelTopology.Name = "labelTopology";
            this.labelTopology.Size = new System.Drawing.Size(59, 13);
            this.labelTopology.TabIndex = 0;
            this.labelTopology.Text = "Topology";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(6, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Start learning rate:";
            // 
            // SomFactoryUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxContainer);
            this.Name = "SomFactoryUI";
            this.Size = new System.Drawing.Size(236, 265);
            this.groupBoxContainer.ResumeLayout(false);
            this.groupBoxContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRows)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxContainer;
        private System.Windows.Forms.NumericUpDown numericUpDownRows;
        private System.Windows.Forms.Label labelTopology;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownColumns;
        private System.Windows.Forms.NumericUpDown numericUpDownIterations;
        private System.Windows.Forms.Label labelIterations;
        private System.Windows.Forms.TextBox textBoxLearningRate;
        private System.Windows.Forms.Label label3;
    }
}
