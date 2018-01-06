namespace AppServer
{
    partial class ClientForm
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
            this.txtMsgReceive = new System.Windows.Forms.TextBox();
            this.txtMsgSend = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtMsgReceive
            // 
            this.txtMsgReceive.Location = new System.Drawing.Point(12, 12);
            this.txtMsgReceive.Multiline = true;
            this.txtMsgReceive.Name = "txtMsgReceive";
            this.txtMsgReceive.Size = new System.Drawing.Size(387, 201);
            this.txtMsgReceive.TabIndex = 0;
            // 
            // txtMsgSend
            // 
            this.txtMsgSend.Location = new System.Drawing.Point(12, 219);
            this.txtMsgSend.Multiline = true;
            this.txtMsgSend.Name = "txtMsgSend";
            this.txtMsgSend.Size = new System.Drawing.Size(313, 44);
            this.txtMsgSend.TabIndex = 1;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(331, 219);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(68, 44);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 275);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMsgSend);
            this.Controls.Add(this.txtMsgReceive);
            this.Name = "ClientForm";
            this.Text = "ClientForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtMsgReceive;
        public System.Windows.Forms.TextBox txtMsgSend;
        public System.Windows.Forms.Button btnSend;

    }
}