
namespace ChatClient_jkw
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
            this.ConnectServerButton = new System.Windows.Forms.Button();
            this.MessageTextBox = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ConnectServerButton
            // 
            this.ConnectServerButton.Location = new System.Drawing.Point(111, 51);
            this.ConnectServerButton.Name = "ConnectServerButton";
            this.ConnectServerButton.Size = new System.Drawing.Size(127, 41);
            this.ConnectServerButton.TabIndex = 0;
            this.ConnectServerButton.Text = "Connect";
            this.ConnectServerButton.UseVisualStyleBackColor = true;
            this.ConnectServerButton.Click += new System.EventHandler(this.ConnectServerButton_Click);
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.Location = new System.Drawing.Point(111, 107);
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.Size = new System.Drawing.Size(267, 23);
            this.MessageTextBox.TabIndex = 1;
            this.MessageTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MessageTextBox_KeyDown);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(419, 40);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(369, 317);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.MessageTextBox);
            this.Controls.Add(this.ConnectServerButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectServerButton;
        private System.Windows.Forms.TextBox MessageTextBox;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

