namespace Text_Justifer_C_sharp
{
    partial class Form1
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
            this.JUSTIFY = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // JUSTIFY
            // 
            this.JUSTIFY.Location = new System.Drawing.Point(1388, 35);
            this.JUSTIFY.Name = "JUSTIFY";
            this.JUSTIFY.Size = new System.Drawing.Size(92, 62);
            this.JUSTIFY.TabIndex = 0;
            this.JUSTIFY.Text = "JUSTIFY";
            this.JUSTIFY.UseVisualStyleBackColor = true;
            this.JUSTIFY.Click += new System.EventHandler(this.JUSTIFY_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1574, 588);
            this.Controls.Add(this.JUSTIFY);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button JUSTIFY;
    }
}

