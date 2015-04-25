namespace Main
{
    partial class MainForm
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
            this.btnSVM = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.btnHMM = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSVM
            // 
            this.btnSVM.Location = new System.Drawing.Point(203, 28);
            this.btnSVM.Name = "btnSVM";
            this.btnSVM.Size = new System.Drawing.Size(90, 40);
            this.btnSVM.TabIndex = 0;
            this.btnSVM.Text = "SVM";
            this.btnSVM.UseVisualStyleBackColor = true;
            this.btnSVM.Click += new System.EventHandler(this.btnSVM_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(103, 120);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(421, 232);
            this.txtOutput.TabIndex = 1;
            this.txtOutput.Text = "";
            // 
            // btnHMM
            // 
            this.btnHMM.Location = new System.Drawing.Point(353, 28);
            this.btnHMM.Name = "btnHMM";
            this.btnHMM.Size = new System.Drawing.Size(100, 35);
            this.btnHMM.TabIndex = 2;
            this.btnHMM.Text = "HMM";
            this.btnHMM.UseVisualStyleBackColor = true;
            this.btnHMM.Click += new System.EventHandler(this.btnHMM_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 378);
            this.Controls.Add(this.btnHMM);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnSVM);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSVM;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.Button btnHMM;
    }
}

