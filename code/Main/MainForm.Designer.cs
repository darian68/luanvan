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
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.btnKDA = new System.Windows.Forms.Button();
            this.btnSVM = new System.Windows.Forms.Button();
            this.btnCombine = new System.Windows.Forms.Button();
            this.txtDim = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnKPCA = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(103, 12);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(348, 354);
            this.txtOutput.TabIndex = 0;
            this.txtOutput.Text = "";
            // 
            // btnKDA
            // 
            this.btnKDA.Location = new System.Drawing.Point(7, 107);
            this.btnKDA.Name = "btnKDA";
            this.btnKDA.Size = new System.Drawing.Size(90, 40);
            this.btnKDA.TabIndex = 11;
            this.btnKDA.Text = "LDA";
            this.btnKDA.UseVisualStyleBackColor = true;
            this.btnKDA.Click += new System.EventHandler(this.btnKLDA_Click);
            // 
            // btnSVM
            // 
            this.btnSVM.Location = new System.Drawing.Point(7, 12);
            this.btnSVM.Name = "btnSVM";
            this.btnSVM.Size = new System.Drawing.Size(90, 40);
            this.btnSVM.TabIndex = 12;
            this.btnSVM.Text = "SVM";
            this.btnSVM.UseVisualStyleBackColor = true;
            this.btnSVM.Click += new System.EventHandler(this.btnSVM_Click);
            // 
            // btnCombine
            // 
            this.btnCombine.Location = new System.Drawing.Point(7, 153);
            this.btnCombine.Name = "btnCombine";
            this.btnCombine.Size = new System.Drawing.Size(90, 42);
            this.btnCombine.TabIndex = 15;
            this.btnCombine.Text = "Combine";
            this.btnCombine.UseVisualStyleBackColor = true;
            this.btnCombine.Click += new System.EventHandler(this.btnCombine_Click);
            // 
            // txtDim
            // 
            this.txtDim.Location = new System.Drawing.Point(534, 45);
            this.txtDim.Name = "txtDim";
            this.txtDim.Size = new System.Drawing.Size(58, 20);
            this.txtDim.TabIndex = 4;
            this.txtDim.Text = "5";
            this.txtDim.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(466, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Dimension";
            // 
            // btnKPCA
            // 
            this.btnKPCA.Location = new System.Drawing.Point(7, 59);
            this.btnKPCA.Name = "btnKPCA";
            this.btnKPCA.Size = new System.Drawing.Size(90, 40);
            this.btnKPCA.TabIndex = 10;
            this.btnKPCA.Text = "PCA";
            this.btnKPCA.UseVisualStyleBackColor = true;
            this.btnKPCA.Click += new System.EventHandler(this.btnKPCA_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 378);
            this.Controls.Add(this.btnCombine);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDim);
            this.Controls.Add(this.btnSVM);
            this.Controls.Add(this.btnKDA);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnKPCA);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.Button btnKDA;
        private System.Windows.Forms.Button btnSVM;
        private System.Windows.Forms.Button btnCombine;
        private System.Windows.Forms.TextBox txtDim;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnKPCA;
    }
}

