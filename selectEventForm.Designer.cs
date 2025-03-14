namespace DavaoCosplayShopCMS
{
    partial class selectEventForm
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
            this.eventsList = new System.Windows.Forms.ListView();
            this.addEventButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // eventsList
            // 
            this.eventsList.HideSelection = false;
            this.eventsList.Location = new System.Drawing.Point(12, 43);
            this.eventsList.Name = "eventsList";
            this.eventsList.Size = new System.Drawing.Size(521, 364);
            this.eventsList.TabIndex = 1;
            this.eventsList.UseCompatibleStateImageBehavior = false;
            this.eventsList.View = System.Windows.Forms.View.Details;
            // 
            // addEventButton
            // 
            this.addEventButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.addEventButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addEventButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.addEventButton.Location = new System.Drawing.Point(431, 5);
            this.addEventButton.Name = "addEventButton";
            this.addEventButton.Size = new System.Drawing.Size(102, 32);
            this.addEventButton.TabIndex = 8;
            this.addEventButton.Text = "ADD EVENT";
            this.addEventButton.UseVisualStyleBackColor = true;
            this.addEventButton.Click += new System.EventHandler(this.addEventButton_Click);
            // 
            // selectEventForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 419);
            this.Controls.Add(this.addEventButton);
            this.Controls.Add(this.eventsList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "selectEventForm";
            this.Text = "Select Event";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView eventsList;
        private System.Windows.Forms.Button addEventButton;
    }
}