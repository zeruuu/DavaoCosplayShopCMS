namespace DavaoCosplayShopCMS
{
    partial class paymentsForm
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
            this.paymentsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.topPanel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.viewHistoryButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.topPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // paymentsFlowPanel
            // 
            this.paymentsFlowPanel.AutoScroll = true;
            this.paymentsFlowPanel.Location = new System.Drawing.Point(12, 64);
            this.paymentsFlowPanel.Name = "paymentsFlowPanel";
            this.paymentsFlowPanel.Size = new System.Drawing.Size(1795, 957);
            this.paymentsFlowPanel.TabIndex = 0;
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.RosyBrown;
            this.topPanel.Controls.Add(this.panel3);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1872, 50);
            this.topPanel.TabIndex = 50;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.viewHistoryButton);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Location = new System.Drawing.Point(-1, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(2085, 44);
            this.panel3.TabIndex = 0;
            // 
            // viewHistoryButton
            // 
            this.viewHistoryButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.viewHistoryButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.viewHistoryButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.viewHistoryButton.Location = new System.Drawing.Point(1604, 6);
            this.viewHistoryButton.Name = "viewHistoryButton";
            this.viewHistoryButton.Size = new System.Drawing.Size(187, 32);
            this.viewHistoryButton.TabIndex = 51;
            this.viewHistoryButton.Text = "TRANSACTION HISTORY";
            this.viewHistoryButton.UseVisualStyleBackColor = true;
            this.viewHistoryButton.Click += new System.EventHandler(this.viewHistoryButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(3, 5);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(140, 32);
            this.label8.TabIndex = 0;
            this.label8.Text = "PAYMENTS";
            // 
            // paymentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1872, 978);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.paymentsFlowPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "paymentsForm";
            this.Text = "paymentsForm";
            this.Activated += new System.EventHandler(this.paymentsForm_Load);
            this.Load += new System.EventHandler(this.paymentsForm_Load);
            this.Shown += new System.EventHandler(this.paymentsForm_Load);
            this.topPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel paymentsFlowPanel;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button viewHistoryButton;
    }
}