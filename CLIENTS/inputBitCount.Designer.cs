namespace CLIENTS
{
    partial class inputBitCount
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.разрядностьcomboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(144, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 36);
            this.button1.TabIndex = 5;
            this.button1.Text = "Выбрать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Выберите разрядность кода состояния";
            // 
            // разрядностьcomboBox1
            // 
            this.разрядностьcomboBox1.FormattingEnabled = true;
            this.разрядностьcomboBox1.Items.AddRange(new object[] {
            "2",
            "3",
            "4"});
            this.разрядностьcomboBox1.Location = new System.Drawing.Point(263, 35);
            this.разрядностьcomboBox1.Name = "разрядностьcomboBox1";
            this.разрядностьcomboBox1.Size = new System.Drawing.Size(121, 21);
            this.разрядностьcomboBox1.TabIndex = 3;
            this.разрядностьcomboBox1.Text = "Разрядность";
            // 
            // inputBitCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 127);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.разрядностьcomboBox1);
            this.MaximumSize = new System.Drawing.Size(413, 166);
            this.MinimumSize = new System.Drawing.Size(413, 166);
            this.Name = "inputBitCount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Разрядность";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox разрядностьcomboBox1;
    }
}