
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
            this.PlayerPanel1 = new System.Windows.Forms.Panel();
            this.PlayerPanel2 = new System.Windows.Forms.Panel();
            this.PlayerPanel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
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
            // PlayerPanel1
            // 
            this.PlayerPanel1.Location = new System.Drawing.Point(13, 43);
            this.PlayerPanel1.Name = "PlayerPanel1";
            this.PlayerPanel1.Size = new System.Drawing.Size(351, 700);
            this.PlayerPanel1.TabIndex = 1;
            this.PlayerPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.PlayPanel_Paint);
            // 
            // PlayerPanel2
            // 
            this.PlayerPanel2.Location = new System.Drawing.Point(593, 33);
            this.PlayerPanel2.Name = "PlayerPanel2";
            this.PlayerPanel2.Size = new System.Drawing.Size(350, 700);
            this.PlayerPanel2.TabIndex = 1;
            this.PlayerPanel2.Paint += new System.Windows.Forms.PaintEventHandler(this.PlayerPanel2_Paint);
            // 
            // PlayerPanel3
            // 
            this.PlayerPanel3.Location = new System.Drawing.Point(1136, 43);
            this.PlayerPanel3.Name = "PlayerPanel3";
            this.PlayerPanel3.Size = new System.Drawing.Size(350, 700);
            this.PlayerPanel3.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(13, 750);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(351, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(592, 740);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(351, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "label2";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(1135, 750);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(351, 23);
            this.label4.TabIndex = 4;
            this.label4.Text = "label2";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(176, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1649, 825);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PlayerPanel3);
            this.Controls.Add(this.PlayerPanel2);
            this.Controls.Add(this.PlayerPanel1);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.ConnectToServerBtn);
            this.Name = "Form1";
            this.Text = "테트리스";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ConnectToServerBtn;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Panel PlayerPanel1;
        private System.Windows.Forms.Panel PlayerPanel2;
        private System.Windows.Forms.Panel PlayerPanel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
    }
}

