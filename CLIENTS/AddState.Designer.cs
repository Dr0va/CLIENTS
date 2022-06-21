namespace CLIENTS
{
    partial class AddState
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
            this.label1 = new System.Windows.Forms.Label();
            this.a0btn = new System.Windows.Forms.Button();
            this.a15btn = new System.Windows.Forms.Button();
            this.a3btn = new System.Windows.Forms.Button();
            this.a7btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "__________________________________";
            // 
            // a0btn
            // 
            this.a0btn.Location = new System.Drawing.Point(27, 29);
            this.a0btn.Name = "a0btn";
            this.a0btn.Size = new System.Drawing.Size(75, 23);
            this.a0btn.TabIndex = 11;
            this.a0btn.Text = "a0";
            this.a0btn.UseVisualStyleBackColor = true;
            this.a0btn.Click += new System.EventHandler(this.a0btn_Click);
            // 
            // a15btn
            // 
            this.a15btn.Enabled = false;
            this.a15btn.Location = new System.Drawing.Point(146, 29);
            this.a15btn.Name = "a15btn";
            this.a15btn.Size = new System.Drawing.Size(72, 23);
            this.a15btn.TabIndex = 12;
            this.a15btn.Text = "a1-a15";
            this.a15btn.UseVisualStyleBackColor = true;
            this.a15btn.Visible = false;
            this.a15btn.Click += new System.EventHandler(this.a15btn_Click);
            // 
            // a3btn
            // 
            this.a3btn.Enabled = false;
            this.a3btn.Location = new System.Drawing.Point(146, 29);
            this.a3btn.Name = "a3btn";
            this.a3btn.Size = new System.Drawing.Size(72, 23);
            this.a3btn.TabIndex = 13;
            this.a3btn.Text = "a1-a3";
            this.a3btn.UseVisualStyleBackColor = true;
            this.a3btn.Visible = false;
            this.a3btn.Click += new System.EventHandler(this.a3btn_Click);
            // 
            // a7btn
            // 
            this.a7btn.Enabled = false;
            this.a7btn.Location = new System.Drawing.Point(146, 29);
            this.a7btn.Name = "a7btn";
            this.a7btn.Size = new System.Drawing.Size(72, 23);
            this.a7btn.TabIndex = 14;
            this.a7btn.Text = "a1-a7";
            this.a7btn.UseVisualStyleBackColor = true;
            this.a7btn.Visible = false;
            this.a7btn.Click += new System.EventHandler(this.a7btn_Click);
            // 
            // AddState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 96);
            this.Controls.Add(this.a7btn);
            this.Controls.Add(this.a3btn);
            this.Controls.Add(this.a15btn);
            this.Controls.Add(this.a0btn);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(279, 135);
            this.MinimumSize = new System.Drawing.Size(279, 135);
            this.Name = "AddState";
            this.Text = "Добавить состояния";
            this.Load += new System.EventHandler(this.AddState_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button a0btn;
        private System.Windows.Forms.Button a15btn;
        private System.Windows.Forms.Button a3btn;
        private System.Windows.Forms.Button a7btn;
    }
}