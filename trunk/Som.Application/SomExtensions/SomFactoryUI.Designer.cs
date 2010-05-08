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
            this.labelDataProvier = new System.Windows.Forms.Label();
            this.comboBoxDataProvider = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxProcessorType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxLearningRate = new System.Windows.Forms.TextBox();
            this.numericUpDownIterations = new System.Windows.Forms.NumericUpDown();
            this.labelIterations = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownColumns = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRows = new System.Windows.Forms.NumericUpDown();
            this.labelTopology = new System.Windows.Forms.Label();
            this.numericUpDownInputSpaceNumber = new System.Windows.Forms.NumericUpDown();
            this.groupBoxContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInputSpaceNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxContainer
            // 
            this.groupBoxContainer.Controls.Add(this.numericUpDownInputSpaceNumber);
            this.groupBoxContainer.Controls.Add(this.labelDataProvier);
            this.groupBoxContainer.Controls.Add(this.comboBoxDataProvider);
            this.groupBoxContainer.Controls.Add(this.label4);
            this.groupBoxContainer.Controls.Add(this.comboBoxProcessorType);
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
            this.groupBoxContainer.Text = "Параметри SOM";
            // 
            // labelDataProvier
            // 
            this.labelDataProvier.AutoSize = true;
            this.labelDataProvier.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDataProvier.Location = new System.Drawing.Point(17, 181);
            this.labelDataProvier.Name = "labelDataProvier";
            this.labelDataProvier.Size = new System.Drawing.Size(58, 13);
            this.labelDataProvier.TabIndex = 12;
            this.labelDataProvier.Text = "Вхідні дані";
            // 
            // comboBoxDataProvider
            // 
            this.comboBoxDataProvider.FormattingEnabled = true;
            this.comboBoxDataProvider.Items.AddRange(new object[] {
            "Випадково",
            "Файл",
            "Силует"});
            this.comboBoxDataProvider.Location = new System.Drawing.Point(105, 178);
            this.comboBoxDataProvider.Name = "comboBoxDataProvider";
            this.comboBoxDataProvider.Size = new System.Drawing.Size(118, 21);
            this.comboBoxDataProvider.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(17, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Тип алгоритму:";
            // 
            // comboBoxProcessorType
            // 
            this.comboBoxProcessorType.FormattingEnabled = true;
            this.comboBoxProcessorType.Items.AddRange(new object[] {
            "Класичний",
            "Масштабований"});
            this.comboBoxProcessorType.Location = new System.Drawing.Point(105, 19);
            this.comboBoxProcessorType.Name = "comboBoxProcessorType";
            this.comboBoxProcessorType.Size = new System.Drawing.Size(118, 21);
            this.comboBoxProcessorType.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(17, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Навч. коеф:";
            // 
            // textBoxLearningRate
            // 
            this.textBoxLearningRate.Location = new System.Drawing.Point(105, 76);
            this.textBoxLearningRate.Name = "textBoxLearningRate";
            this.textBoxLearningRate.Size = new System.Drawing.Size(118, 20);
            this.textBoxLearningRate.TabIndex = 7;
            this.textBoxLearningRate.Text = "0,07";
            // 
            // numericUpDownIterations
            // 
            this.numericUpDownIterations.Location = new System.Drawing.Point(105, 47);
            this.numericUpDownIterations.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownIterations.Name = "numericUpDownIterations";
            this.numericUpDownIterations.Size = new System.Drawing.Size(118, 20);
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
            this.labelIterations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelIterations.Location = new System.Drawing.Point(17, 49);
            this.labelIterations.Name = "labelIterations";
            this.labelIterations.Size = new System.Drawing.Size(47, 13);
            this.labelIterations.TabIndex = 5;
            this.labelIterations.Text = "Ітерацій";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(172, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Кол.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ряд.";
            // 
            // numericUpDownColumns
            // 
            this.numericUpDownColumns.Location = new System.Drawing.Point(175, 115);
            this.numericUpDownColumns.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownColumns.Name = "numericUpDownColumns";
            this.numericUpDownColumns.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownColumns.TabIndex = 2;
            this.numericUpDownColumns.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // numericUpDownRows
            // 
            this.numericUpDownRows.Location = new System.Drawing.Point(105, 115);
            this.numericUpDownRows.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownRows.Name = "numericUpDownRows";
            this.numericUpDownRows.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownRows.TabIndex = 1;
            this.numericUpDownRows.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // labelTopology
            // 
            this.labelTopology.AutoSize = true;
            this.labelTopology.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTopology.Location = new System.Drawing.Point(17, 117);
            this.labelTopology.Name = "labelTopology";
            this.labelTopology.Size = new System.Drawing.Size(57, 13);
            this.labelTopology.TabIndex = 0;
            this.labelTopology.Text = "Топологія";
            // 
            // numericUpDownInputSpaceNumber
            // 
            this.numericUpDownInputSpaceNumber.Location = new System.Drawing.Point(105, 205);
            this.numericUpDownInputSpaceNumber.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownInputSpaceNumber.Name = "numericUpDownInputSpaceNumber";
            this.numericUpDownInputSpaceNumber.Size = new System.Drawing.Size(118, 20);
            this.numericUpDownInputSpaceNumber.TabIndex = 13;
            this.numericUpDownInputSpaceNumber.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
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
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInputSpaceNumber)).EndInit();
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxProcessorType;
        private System.Windows.Forms.Label labelDataProvier;
        private System.Windows.Forms.ComboBox comboBoxDataProvider;
        private System.Windows.Forms.NumericUpDown numericUpDownInputSpaceNumber;
    }
}
