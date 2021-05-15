
namespace ChatClient_jkw
{
    partial class MainClientForm
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
            this.ConnectLocalServerButton = new System.Windows.Forms.Button();
            this.MessageTextBox = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.ConnectRemoteServerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConnectLocalServerButton
            // 
            this.ConnectLocalServerButton.Location = new System.Drawing.Point(12, 12);
            this.ConnectLocalServerButton.Name = "ConnectLocalServerButton";
            this.ConnectLocalServerButton.Size = new System.Drawing.Size(127, 41);
            this.ConnectLocalServerButton.TabIndex = 0;
            this.ConnectLocalServerButton.Text = "Connect Local";
            this.ConnectLocalServerButton.UseVisualStyleBackColor = true;
            this.ConnectLocalServerButton.Click += new System.EventHandler(this.ConnectServerButton_Click);
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.Location = new System.Drawing.Point(12, 68);
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.Size = new System.Drawing.Size(267, 23);
            this.MessageTextBox.TabIndex = 1;
            this.MessageTextBox.TextChanged += new System.EventHandler(this.MessageTextBox_TextChanged);
            this.MessageTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MessageTextBox_KeyDown);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(316, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(369, 317);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // ConnectRemoteServerButton
            // 
            this.ConnectRemoteServerButton.Location = new System.Drawing.Point(152, 12);
            this.ConnectRemoteServerButton.Name = "ConnectRemoteServerButton";
            this.ConnectRemoteServerButton.Size = new System.Drawing.Size(127, 41);
            this.ConnectRemoteServerButton.TabIndex = 3;
            this.ConnectRemoteServerButton.Text = "Connect Server";
            this.ConnectRemoteServerButton.UseVisualStyleBackColor = true;
            this.ConnectRemoteServerButton.Click += new System.EventHandler(this.ConnectRemoteServerButton_Click);
            // 
            // MainClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ConnectRemoteServerButton);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.MessageTextBox);
            this.Controls.Add(this.ConnectLocalServerButton);
            this.Name = "MainClientForm";
            this.Text = "MainClientForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectLocalServerButton;
        private System.Windows.Forms.TextBox MessageTextBox;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button ConnectRemoteServerButton;
    }
}

