namespace Ant.UserView.WinForm
{
    partial class Form2
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.lblIp = new System.Windows.Forms.Label();
            this.lblInt = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.chkEnableProxy = new System.Windows.Forms.CheckBox();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblProxyUrls = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblEnableNum = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(292, 22);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(804, 541);
            this.webBrowser1.TabIndex = 0;
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Location = new System.Drawing.Point(12, 212);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(17, 12);
            this.lblIp.TabIndex = 1;
            this.lblIp.Text = "IP";
            // 
            // lblInt
            // 
            this.lblInt.AutoSize = true;
            this.lblInt.Location = new System.Drawing.Point(12, 234);
            this.lblInt.Name = "lblInt";
            this.lblInt.Size = new System.Drawing.Size(29, 12);
            this.lblInt.TabIndex = 2;
            this.lblInt.Text = "页码";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(82, 22);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(181, 21);
            this.txtUrl.TabIndex = 3;
            this.txtUrl.Text = "http://www.sqber.com";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "网址：";
            // 
            // btnSwitch
            // 
            this.btnSwitch.Location = new System.Drawing.Point(79, 113);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(75, 23);
            this.btnSwitch.TabIndex = 5;
            this.btnSwitch.Text = "开始";
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // chkEnableProxy
            // 
            this.chkEnableProxy.AutoSize = true;
            this.chkEnableProxy.Location = new System.Drawing.Point(82, 81);
            this.chkEnableProxy.Name = "chkEnableProxy";
            this.chkEnableProxy.Size = new System.Drawing.Size(72, 16);
            this.chkEnableProxy.TabIndex = 6;
            this.chkEnableProxy.Text = "启用代理";
            this.chkEnableProxy.UseVisualStyleBackColor = true;
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(82, 49);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(181, 21);
            this.txtInterval.TabIndex = 3;
            this.txtInterval.Text = "5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "时间间隔：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(269, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "秒";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 266);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "代理服务器列表：";
            // 
            // lblProxyUrls
            // 
            this.lblProxyUrls.AutoSize = true;
            this.lblProxyUrls.Location = new System.Drawing.Point(12, 296);
            this.lblProxyUrls.Name = "lblProxyUrls";
            this.lblProxyUrls.Size = new System.Drawing.Size(77, 12);
            this.lblProxyUrls.TabIndex = 7;
            this.lblProxyUrls.Text = "lblProxyUrls";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "状态：";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 173);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(11, 12);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "-";
            this.lblStatus.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(174, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "代理可用数量：";
            // 
            // lblEnableNum
            // 
            this.lblEnableNum.AutoSize = true;
            this.lblEnableNum.Location = new System.Drawing.Point(174, 107);
            this.lblEnableNum.Name = "lblEnableNum";
            this.lblEnableNum.Size = new System.Drawing.Size(77, 12);
            this.lblEnableNum.TabIndex = 7;
            this.lblEnableNum.Text = "lblEnableNum";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 576);
            this.Controls.Add(this.lblEnableNum);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblProxyUrls);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkEnableProxy);
            this.Controls.Add(this.btnSwitch);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblInt);
            this.Controls.Add(this.lblIp);
            this.Controls.Add(this.webBrowser1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.Label lblInt;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.CheckBox chkEnableProxy;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblProxyUrls;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblEnableNum;
    }
}