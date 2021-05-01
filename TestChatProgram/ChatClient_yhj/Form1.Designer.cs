namespace ChatClient_yhj
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.BtnDisConnect = new System.Windows.Forms.Button();
            this.TxtCotents = new System.Windows.Forms.TextBox();
            this.BtnSend = new System.Windows.Forms.Button();
            this.LbConnected = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(314, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(258, 359);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(12, 12);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(126, 41);
            this.BtnConnect.TabIndex = 2;
            this.BtnConnect.Text = "Connect";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // BtnDisConnect
            // 
            this.BtnDisConnect.Location = new System.Drawing.Point(144, 12);
            this.BtnDisConnect.Name = "BtnDisConnect";
            this.BtnDisConnect.Size = new System.Drawing.Size(126, 41);
            this.BtnDisConnect.TabIndex = 2;
            this.BtnDisConnect.Text = "DisConnect";
            this.BtnDisConnect.UseVisualStyleBackColor = true;
            this.BtnDisConnect.Click += new System.EventHandler(this.BtnDisConnect_Click);
            // 
            // TxtCotents
            // 
            this.TxtCotents.Location = new System.Drawing.Point(314, 377);
            this.TxtCotents.Name = "TxtCotents";
            this.TxtCotents.Size = new System.Drawing.Size(201, 21);
            this.TxtCotents.TabIndex = 3;
            this.TxtCotents.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCotents_KeyDown);
            // 
            // BtnSend
            // 
            this.BtnSend.Location = new System.Drawing.Point(521, 377);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(52, 23);
            this.BtnSend.TabIndex = 4;
            this.BtnSend.Text = "send";
            this.BtnSend.UseVisualStyleBackColor = true;
            // 
            // LbConnected
            // 
            this.LbConnected.AutoSize = true;
            this.LbConnected.Location = new System.Drawing.Point(12, 72);
            this.LbConnected.Name = "LbConnected";
            this.LbConnected.Size = new System.Drawing.Size(137, 12);
            this.LbConnected.TabIndex = 5;
            this.LbConnected.Text = "연결된 서버가 없습니다.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 419);
            this.Controls.Add(this.LbConnected);
            this.Controls.Add(this.BtnSend);
            this.Controls.Add(this.TxtCotents);
            this.Controls.Add(this.BtnDisConnect);
            this.Controls.Add(this.BtnConnect);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.Button BtnDisConnect;
        private System.Windows.Forms.TextBox TxtCotents;
        private System.Windows.Forms.Button BtnSend;
        private System.Windows.Forms.Label LbConnected;
    }
}

