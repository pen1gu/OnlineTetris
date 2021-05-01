
namespace ChatClient_jtg
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
            this.ChattingListTextBox = new System.Windows.Forms.RichTextBox();
            this.ConnetToServerBtn = new System.Windows.Forms.Button();
            this.MessageTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ChattingListTextBox
            // 
            this.ChattingListTextBox.Location = new System.Drawing.Point(12, 12);
            this.ChattingListTextBox.Name = "ChattingListTextBox";
            this.ChattingListTextBox.Size = new System.Drawing.Size(320, 381);
            this.ChattingListTextBox.TabIndex = 0;
            this.ChattingListTextBox.Text = "";
            // 
            // ConnetToServerBtn
            // 
            this.ConnetToServerBtn.Location = new System.Drawing.Point(339, 13);
            this.ConnetToServerBtn.Name = "ConnetToServerBtn";
            this.ConnetToServerBtn.Size = new System.Drawing.Size(105, 47);
            this.ConnetToServerBtn.TabIndex = 1;
            this.ConnetToServerBtn.Text = "서버 연결";
            this.ConnetToServerBtn.UseVisualStyleBackColor = true;
            this.ConnetToServerBtn.Click += new System.EventHandler(this.ConnetToServerBtn_Click);
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.Location = new System.Drawing.Point(13, 400);
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.Size = new System.Drawing.Size(319, 23);
            this.MessageTextBox.TabIndex = 2;
            this.MessageTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MessageTextBox_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 456);
            this.Controls.Add(this.MessageTextBox);
            this.Controls.Add(this.ConnetToServerBtn);
            this.Controls.Add(this.ChattingListTextBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox ChattingListTextBox;
        private System.Windows.Forms.Button ConnetToServerBtn;
        private System.Windows.Forms.RichTextBox ChattingList;
        private System.Windows.Forms.TextBox MessageTextBox;
    }
}

