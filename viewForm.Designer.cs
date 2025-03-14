namespace DavaoCosplayShopCMS
{
    partial class viewForm
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
            this.topPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.refreshCostumesButton = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.addCostumeButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.sortBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.viewCostumesFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.topPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refreshCostumesButton)).BeginInit();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.RosyBrown;
            this.topPanel.Controls.Add(this.panel1);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1868, 50);
            this.topPanel.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.refreshCostumesButton);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.addCostumeButton);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.sortBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2085, 44);
            this.panel1.TabIndex = 0;
            // 
            // refreshCostumesButton
            // 
            this.refreshCostumesButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refreshCostumesButton.Image = global::DavaoCosplayShopCMS.Properties.Resources.refresh_page_option;
            this.refreshCostumesButton.Location = new System.Drawing.Point(1571, 6);
            this.refreshCostumesButton.Name = "refreshCostumesButton";
            this.refreshCostumesButton.Size = new System.Drawing.Size(30, 30);
            this.refreshCostumesButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.refreshCostumesButton.TabIndex = 0;
            this.refreshCostumesButton.TabStop = false;
            this.refreshCostumesButton.Click += new System.EventHandler(this.refreshCostumesButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(788, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 32);
            this.label3.TabIndex = 8;
            this.label3.Text = "Search:";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(888, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(265, 29);
            this.textBox1.TabIndex = 7;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // addCostumeButton
            // 
            this.addCostumeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.addCostumeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addCostumeButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.addCostumeButton.Location = new System.Drawing.Point(1607, 6);
            this.addCostumeButton.Name = "addCostumeButton";
            this.addCostumeButton.Size = new System.Drawing.Size(191, 32);
            this.addCostumeButton.TabIndex = 6;
            this.addCostumeButton.Text = "REGISTER NEW COSTUME";
            this.addCostumeButton.UseVisualStyleBackColor = true;
            this.addCostumeButton.Click += new System.EventHandler(this.addCostumeButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(1159, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sort by:";
            // 
            // sortBox
            // 
            this.sortBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.sortBox.FormattingEnabled = true;
            this.sortBox.Items.AddRange(new object[] {
            "None",
            "Name",
            "Price",
            "Available"});
            this.sortBox.Location = new System.Drawing.Point(1269, 12);
            this.sortBox.Name = "sortBox";
            this.sortBox.Size = new System.Drawing.Size(121, 23);
            this.sortBox.TabIndex = 1;
            this.sortBox.Text = "None";
            this.sortBox.SelectedIndexChanged += new System.EventHandler(this.sortBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(1, 5);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(208, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "VIEW COSTUMES";
            // 
            // viewCostumesFlowLayoutPanel
            // 
            this.viewCostumesFlowLayoutPanel.AutoScroll = true;
            this.viewCostumesFlowLayoutPanel.Location = new System.Drawing.Point(6, 57);
            this.viewCostumesFlowLayoutPanel.Name = "viewCostumesFlowLayoutPanel";
            this.viewCostumesFlowLayoutPanel.Size = new System.Drawing.Size(1791, 879);
            this.viewCostumesFlowLayoutPanel.TabIndex = 6;
            // 
            // viewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1868, 1017);
            this.Controls.Add(this.viewCostumesFlowLayoutPanel);
            this.Controls.Add(this.topPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "viewForm";
            this.Text = "viewForm";
            this.Load += new System.EventHandler(this.viewForm_Load);
            this.topPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refreshCostumesButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel viewCostumesFlowLayoutPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox sortBox;
        private System.Windows.Forms.Button addCostumeButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox refreshCostumesButton;
    }
}