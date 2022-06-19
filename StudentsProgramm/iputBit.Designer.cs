namespace StudentsProgramm
{
    partial class iputBit
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
            this.label2 = new System.Windows.Forms.Label();
            this.inputBin_button1 = new System.Windows.Forms.Button();
            this.inputBit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Введите код состояния";
            // 
            // inputBin_button1
            // 
            this.inputBin_button1.Location = new System.Drawing.Point(145, 79);
            this.inputBin_button1.Name = "inputBin_button1";
            this.inputBin_button1.Size = new System.Drawing.Size(113, 36);
            this.inputBin_button1.TabIndex = 9;
            this.inputBin_button1.Text = "Ok";
            this.inputBin_button1.UseVisualStyleBackColor = true;
            this.inputBin_button1.Click += new System.EventHandler(this.inputBin_button1_Click);
            // 
            // inputBit
            // 
            this.inputBit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.inputBit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inputBit.Location = new System.Drawing.Point(265, 31);
            this.inputBit.MaximumSize = new System.Drawing.Size(120, 22);
            this.inputBit.MinimumSize = new System.Drawing.Size(120, 22);
            this.inputBit.Name = "inputBit";
            this.inputBit.Size = new System.Drawing.Size(120, 22);
            this.inputBit.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 11;
            // 
            // iputBit
            // 
            this.AcceptButton = this.inputBin_button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 127);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inputBit);
            this.Controls.Add(this.inputBin_button1);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(413, 166);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(413, 166);
            this.Name = "iputBit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Код состояния";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button inputBin_button1;
        private System.Windows.Forms.TextBox inputBit;
        private System.Windows.Forms.Label label1;
    }
}