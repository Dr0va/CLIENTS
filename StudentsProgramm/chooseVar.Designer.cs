namespace StudentsProgramm
{
    partial class chooseVar
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
            this.okbutton = new System.Windows.Forms.Button();
            this.вариантcomboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // okbutton
            // 
            this.okbutton.Location = new System.Drawing.Point(263, 51);
            this.okbutton.Name = "okbutton";
            this.okbutton.Size = new System.Drawing.Size(75, 23);
            this.okbutton.TabIndex = 0;
            this.okbutton.Text = "Ok";
            this.okbutton.UseVisualStyleBackColor = true;
            this.okbutton.Click += new System.EventHandler(this.okbutton_Click);
            // 
            // вариантcomboBox1
            // 
            this.вариантcomboBox1.FormattingEnabled = true;
            this.вариантcomboBox1.Location = new System.Drawing.Point(22, 51);
            this.вариантcomboBox1.MaxDropDownItems = 10;
            this.вариантcomboBox1.Name = "вариантcomboBox1";
            this.вариантcomboBox1.Size = new System.Drawing.Size(121, 21);
            this.вариантcomboBox1.TabIndex = 1;
            this.вариантcomboBox1.Text = "Вариант";
            // 
            // chooseVar
            // 
            this.AcceptButton = this.okbutton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 127);
            this.Controls.Add(this.вариантcomboBox1);
            this.Controls.Add(this.okbutton);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(413, 166);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(413, 166);
            this.Name = "chooseVar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор варианта";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okbutton;
        private System.Windows.Forms.ComboBox вариантcomboBox1;
    }
}