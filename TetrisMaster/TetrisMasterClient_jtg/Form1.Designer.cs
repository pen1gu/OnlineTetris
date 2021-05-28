
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
            this.PlayPanel = new System.Windows.Forms.Panel();
            this.Player2Panel = new System.Windows.Forms.Panel();
            this.Player3Panel = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
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
            // PlayPanel
            // 
            this.PlayPanel.Location = new System.Drawing.Point(14, 43);
            this.PlayPanel.Name = "PlayPanel";
            this.PlayPanel.Size = new System.Drawing.Size(350, 700);
            this.PlayPanel.TabIndex = 1;
            this.PlayPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.PlayPanel_Paint);
            // 
            // Player2Panel
            // 
            this.Player2Panel.Location = new System.Drawing.Point(625, 43);
            this.Player2Panel.Name = "Player2Panel";
            this.Player2Panel.Size = new System.Drawing.Size(350, 700);
            this.Player2Panel.TabIndex = 1;
            // 
            // Player3Panel
            // 
            this.Player3Panel.Location = new System.Drawing.Point(1092, 43);
            this.Player3Panel.Name = "Player3Panel";
            this.Player3Panel.Size = new System.Drawing.Size(350, 700);
            this.Player3Panel.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(370, 61);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(159, 94);
            this.listBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(370, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "유저목록";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1649, 825);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.Player3Panel);
            this.Controls.Add(this.Player2Panel);
            this.Controls.Add(this.PlayPanel);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.ConnectToServerBtn);
            this.Name = "Form1";
            this.Text = "테트리스";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectToServerBtn;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Panel PlayPanel;
        private System.Windows.Forms.Panel Player2Panel;
        private System.Windows.Forms.Panel Player3Panel;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
    }
}

