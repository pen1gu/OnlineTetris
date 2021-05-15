
namespace TetrisMasterClient_jtg
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ConnectToServerBtn = new System.Windows.Forms.Button();
            this.StartBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // ConnectToServerBtn
            // 
            this.ConnectToServerBtn.Location = new System.Drawing.Point(13, 13);
            this.ConnectToServerBtn.Name = "ConnectToServerBtn";
            this.ConnectToServerBtn.Size = new System.Drawing.Size(75, 23);
            this.ConnectToServerBtn.TabIndex = 0;
            this.ConnectToServerBtn.Text = "서버 연결";
            this.ConnectToServerBtn.UseVisualStyleBackColor = true;
            this.ConnectToServerBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(94, 12);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(75, 23);
            this.StartBtn.TabIndex = 0;
            this.StartBtn.Text = "시작";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(14, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(350, 700);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(498, 43);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(350, 700);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(965, 43);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(350, 700);
            this.panel3.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1504, 824);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.ConnectToServerBtn);
            this.Name = "Form1";
            this.Text = "테트리스";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ConnectToServerBtn;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}

