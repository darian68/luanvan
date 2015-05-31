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
            this.btnKPCA = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.btnKDA = new System.Windows.Forms.Button();
            this.btnDTW = new System.Windows.Forms.Button();
            this.txtFrames = new System.Windows.Forms.TextBox();
            this.txtDim = new System.Windows.Forms.TextBox();
            this.txtCom = new System.Windows.Forms.TextBox();
            this.txtThreshold = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSVM = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtSymbol = new System.Windows.Forms.TextBox();
            this.txtState = new System.Windows.Forms.TextBox();
            this.txtTolerance = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCombine = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnKPCA
            // 
            this.btnKPCA.Location = new System.Drawing.Point(7, 12);
            this.btnKPCA.Name = "btnKPCA";
            this.btnKPCA.Size = new System.Drawing.Size(90, 40);
            this.btnKPCA.TabIndex = 10;
            this.btnKPCA.Text = "PCA";
            this.btnKPCA.UseVisualStyleBackColor = true;
            this.btnKPCA.Click += new System.EventHandler(this.btnKPCA_Click);
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
            this.btnKDA.Location = new System.Drawing.Point(7, 58);
            this.btnKDA.Name = "btnKDA";
            this.btnKDA.Size = new System.Drawing.Size(90, 40);
            this.btnKDA.TabIndex = 11;
            this.btnKDA.Text = "LDA";
            this.btnKDA.UseVisualStyleBackColor = true;
            this.btnKDA.Click += new System.EventHandler(this.btnKLDA_Click);
            // 
            // btnDTW
            // 
            this.btnDTW.Location = new System.Drawing.Point(7, 104);
            this.btnDTW.Name = "btnDTW";
            this.btnDTW.Size = new System.Drawing.Size(90, 40);
            this.btnDTW.TabIndex = 12;
            this.btnDTW.Text = "DTW";
            this.btnDTW.UseVisualStyleBackColor = true;
            this.btnDTW.Click += new System.EventHandler(this.btnDTW_Click);
            // 
            // txtFrames
            // 
            this.txtFrames.Location = new System.Drawing.Point(540, 43);
            this.txtFrames.Name = "txtFrames";
            this.txtFrames.Size = new System.Drawing.Size(58, 20);
            this.txtFrames.TabIndex = 3;
            this.txtFrames.Text = "120";
            this.txtFrames.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtDim
            // 
            this.txtDim.Location = new System.Drawing.Point(540, 78);
            this.txtDim.Name = "txtDim";
            this.txtDim.Size = new System.Drawing.Size(58, 20);
            this.txtDim.TabIndex = 4;
            this.txtDim.Text = "5";
            this.txtDim.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtCom
            // 
            this.txtCom.Location = new System.Drawing.Point(540, 115);
            this.txtCom.Name = "txtCom";
            this.txtCom.Size = new System.Drawing.Size(58, 20);
            this.txtCom.TabIndex = 5;
            this.txtCom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtThreshold
            // 
            this.txtThreshold.Location = new System.Drawing.Point(540, 150);
            this.txtThreshold.Name = "txtThreshold";
            this.txtThreshold.Size = new System.Drawing.Size(58, 20);
            this.txtThreshold.TabIndex = 6;
            this.txtThreshold.Text = "0.0001";
            this.txtThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(472, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Frames";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(472, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Dimension";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(472, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Complexity";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(472, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Threshold";
            // 
            // btnSVM
            // 
            this.btnSVM.Location = new System.Drawing.Point(7, 153);
            this.btnSVM.Name = "btnSVM";
            this.btnSVM.Size = new System.Drawing.Size(90, 40);
            this.btnSVM.TabIndex = 12;
            this.btnSVM.Text = "SVM";
            this.btnSVM.UseVisualStyleBackColor = true;
            this.btnSVM.Click += new System.EventHandler(this.btnSVM_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 199);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 37);
            this.button1.TabIndex = 13;
            this.button1.Text = "HMM";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSymbol
            // 
            this.txtSymbol.Location = new System.Drawing.Point(546, 208);
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Size = new System.Drawing.Size(51, 20);
            this.txtSymbol.TabIndex = 14;
            this.txtSymbol.Text = "10";
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(546, 234);
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(51, 20);
            this.txtState.TabIndex = 14;
            this.txtState.Text = "10";
            // 
            // txtTolerance
            // 
            this.txtTolerance.Location = new System.Drawing.Point(546, 260);
            this.txtTolerance.Name = "txtTolerance";
            this.txtTolerance.Size = new System.Drawing.Size(51, 20);
            this.txtTolerance.TabIndex = 14;
            this.txtTolerance.Text = "0.001";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(472, 208);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Symbols";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(472, 237);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "States";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(472, 263);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Tolerance";
            // 
            // btnCombine
            // 
            this.btnCombine.Location = new System.Drawing.Point(7, 335);
            this.btnCombine.Name = "btnCombine";
            this.btnCombine.Size = new System.Drawing.Size(90, 31);
            this.btnCombine.TabIndex = 15;
            this.btnCombine.Text = "Combine";
            this.btnCombine.UseVisualStyleBackColor = true;
            this.btnCombine.Click += new System.EventHandler(this.btnCombine_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 378);
            this.Controls.Add(this.btnCombine);
            this.Controls.Add(this.txtTolerance);
            this.Controls.Add(this.txtState);
            this.Controls.Add(this.txtSymbol);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtThreshold);
            this.Controls.Add(this.txtCom);
            this.Controls.Add(this.txtDim);
            this.Controls.Add(this.txtFrames);
            this.Controls.Add(this.btnSVM);
            this.Controls.Add(this.btnDTW);
            this.Controls.Add(this.btnKDA);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnKPCA);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnKPCA;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.Button btnKDA;
        private System.Windows.Forms.Button btnDTW;
        private System.Windows.Forms.TextBox txtFrames;
        private System.Windows.Forms.TextBox txtDim;
        private System.Windows.Forms.TextBox txtCom;
        private System.Windows.Forms.TextBox txtThreshold;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSVM;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtSymbol;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.TextBox txtTolerance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCombine;
    }
}

