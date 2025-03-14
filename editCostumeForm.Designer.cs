namespace DavaoCosplayShopCMS
{
    partial class editCostumeForm
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
            this.label22 = new System.Windows.Forms.Label();
            this.genderComboBox = new System.Windows.Forms.ComboBox();
            this.costumeSizeComboBox = new System.Windows.Forms.ComboBox();
            this.costumeTypeComboBox = new System.Windows.Forms.ComboBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.doneButton = new System.Windows.Forms.Button();
            this.browseButton = new System.Windows.Forms.Button();
            this.costumePictureBox = new System.Windows.Forms.PictureBox();
            this.inclusionsTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.priceTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.originTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.costumeIDTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.costumePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(134, 322);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(16, 17);
            this.label22.TabIndex = 86;
            this.label22.Text = "₱";
            // 
            // genderComboBox
            // 
            this.genderComboBox.FormattingEnabled = true;
            this.genderComboBox.Items.AddRange(new object[] {
            "MALE",
            "FEMALE"});
            this.genderComboBox.Location = new System.Drawing.Point(137, 159);
            this.genderComboBox.Name = "genderComboBox";
            this.genderComboBox.Size = new System.Drawing.Size(157, 21);
            this.genderComboBox.TabIndex = 85;
            // 
            // costumeSizeComboBox
            // 
            this.costumeSizeComboBox.FormattingEnabled = true;
            this.costumeSizeComboBox.Items.AddRange(new object[] {
            "KIDS",
            "SMALL",
            "SMALL - MEDIUM",
            "MEDIUM",
            "MEDIUM - LARGE",
            "LARGE",
            "XLARGE",
            "XXLARGE",
            "ALL SIZES",
            "NOT APPLICABLE"});
            this.costumeSizeComboBox.Location = new System.Drawing.Point(137, 132);
            this.costumeSizeComboBox.Name = "costumeSizeComboBox";
            this.costumeSizeComboBox.Size = new System.Drawing.Size(157, 21);
            this.costumeSizeComboBox.TabIndex = 84;
            // 
            // costumeTypeComboBox
            // 
            this.costumeTypeComboBox.FormattingEnabled = true;
            this.costumeTypeComboBox.Items.AddRange(new object[] {
            "CLOTH",
            "ARMOR",
            "NATIONAL COSTUME"});
            this.costumeTypeComboBox.Location = new System.Drawing.Point(137, 106);
            this.costumeTypeComboBox.Name = "costumeTypeComboBox";
            this.costumeTypeComboBox.Size = new System.Drawing.Size(157, 21);
            this.costumeTypeComboBox.TabIndex = 83;
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.cancelButton.Location = new System.Drawing.Point(384, 322);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(60, 23);
            this.cancelButton.TabIndex = 82;
            this.cancelButton.Text = "CANCEL";
            this.cancelButton.UseVisualStyleBackColor = false;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click_1);
            // 
            // doneButton
            // 
            this.doneButton.Location = new System.Drawing.Point(450, 322);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(59, 23);
            this.doneButton.TabIndex = 81;
            this.doneButton.Text = "DONE";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click_1);
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(434, 233);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 80;
            this.browseButton.Text = "BROWSE";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click_1);
            // 
            // costumePictureBox
            // 
            this.costumePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.costumePictureBox.Location = new System.Drawing.Point(309, 27);
            this.costumePictureBox.Name = "costumePictureBox";
            this.costumePictureBox.Size = new System.Drawing.Size(200, 200);
            this.costumePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.costumePictureBox.TabIndex = 79;
            this.costumePictureBox.TabStop = false;
            // 
            // inclusionsTextBox
            // 
            this.inclusionsTextBox.Location = new System.Drawing.Point(137, 184);
            this.inclusionsTextBox.Multiline = true;
            this.inclusionsTextBox.Name = "inclusionsTextBox";
            this.inclusionsTextBox.Size = new System.Drawing.Size(157, 133);
            this.inclusionsTextBox.TabIndex = 78;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(16, 184);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 17);
            this.label8.TabIndex = 77;
            this.label8.Text = "INCLUSIONS";
            // 
            // priceTextBox
            // 
            this.priceTextBox.Location = new System.Drawing.Point(150, 322);
            this.priceTextBox.Name = "priceTextBox";
            this.priceTextBox.Size = new System.Drawing.Size(94, 20);
            this.priceTextBox.TabIndex = 76;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(19, 322);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 17);
            this.label6.TabIndex = 75;
            this.label6.Text = "PRICE";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 158);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 17);
            this.label5.TabIndex = 74;
            this.label5.Text = "GENDER";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 17);
            this.label4.TabIndex = 73;
            this.label4.Text = "COSTUME SIZE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 17);
            this.label3.TabIndex = 72;
            this.label3.Text = "COSTUME TYPE";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(137, 53);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(157, 20);
            this.nameTextBox.TabIndex = 71;
            // 
            // originTextBox
            // 
            this.originTextBox.Location = new System.Drawing.Point(137, 80);
            this.originTextBox.Name = "originTextBox";
            this.originTextBox.Size = new System.Drawing.Size(157, 20);
            this.originTextBox.TabIndex = 70;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 69;
            this.label2.Text = "ORIGIN";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 68;
            this.label1.Text = "NAME";
            // 
            // costumeIDTextBox
            // 
            this.costumeIDTextBox.Location = new System.Drawing.Point(137, 27);
            this.costumeIDTextBox.Name = "costumeIDTextBox";
            this.costumeIDTextBox.ReadOnly = true;
            this.costumeIDTextBox.Size = new System.Drawing.Size(59, 20);
            this.costumeIDTextBox.TabIndex = 88;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 17);
            this.label7.TabIndex = 87;
            this.label7.Text = "COSTUME ID";
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.deleteButton.Location = new System.Drawing.Point(434, 2);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 89;
            this.deleteButton.Text = "DELETE";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click_1);
            // 
            // editCostumeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 357);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.costumeIDTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.genderComboBox);
            this.Controls.Add(this.costumeSizeComboBox);
            this.Controls.Add(this.costumeTypeComboBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.costumePictureBox);
            this.Controls.Add(this.inclusionsTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.priceTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.originTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "editCostumeForm";
            this.Text = "Edit Costume";
            ((System.ComponentModel.ISupportInitialize)(this.costumePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox genderComboBox;
        private System.Windows.Forms.ComboBox costumeSizeComboBox;
        private System.Windows.Forms.ComboBox costumeTypeComboBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.PictureBox costumePictureBox;
        private System.Windows.Forms.TextBox inclusionsTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox priceTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.TextBox originTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox costumeIDTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button deleteButton;
    }
}