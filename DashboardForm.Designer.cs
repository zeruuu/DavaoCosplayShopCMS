namespace DavaoCosplayShopCMS
{
    partial class dashboardForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.topPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.eventsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.viewEventsButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.eventCalendar = new System.Windows.Forms.DateTimePicker();
            this.eventsListPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.costumesRentedPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.viewRentedCostumesButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.rentedCostumesPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.newestCostumesPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.newCostumesListPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.MTOPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.viewMTOButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.mtoListPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.viewPaymentsButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.transactionTotalsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.monthPicker = new System.Windows.Forms.DateTimePicker();
            this.revenueLabel = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.totalRevenueLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.refreshDashboardButton = new System.Windows.Forms.PictureBox();
            this.revenueChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.totalCostumesLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.totalRentedCostumes = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.totalMTO = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.totalActiveEvents = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.topPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.eventsPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.costumesRentedPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.newestCostumesPanel.SuspendLayout();
            this.panel4.SuspendLayout();
            this.MTOPanel.SuspendLayout();
            this.panel5.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refreshDashboardButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.revenueChart)).BeginInit();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.RosyBrown;
            this.topPanel.Controls.Add(this.panel1);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1803, 50);
            this.topPanel.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.refreshDashboardButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2085, 44);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(-2, 5);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(164, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "DASHBOARD";
            // 
            // eventsPanel
            // 
            this.eventsPanel.BackColor = System.Drawing.Color.RosyBrown;
            this.eventsPanel.Controls.Add(this.panel2);
            this.eventsPanel.Controls.Add(this.eventCalendar);
            this.eventsPanel.Controls.Add(this.eventsListPanel);
            this.eventsPanel.Location = new System.Drawing.Point(10, 61);
            this.eventsPanel.Margin = new System.Windows.Forms.Padding(1);
            this.eventsPanel.Name = "eventsPanel";
            this.eventsPanel.Size = new System.Drawing.Size(283, 332);
            this.eventsPanel.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.viewEventsButton);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(275, 28);
            this.panel2.TabIndex = 1;
            // 
            // viewEventsButton
            // 
            this.viewEventsButton.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewEventsButton.Location = new System.Drawing.Point(200, 3);
            this.viewEventsButton.Name = "viewEventsButton";
            this.viewEventsButton.Size = new System.Drawing.Size(72, 22);
            this.viewEventsButton.TabIndex = 2;
            this.viewEventsButton.Text = "View More";
            this.viewEventsButton.UseVisualStyleBackColor = true;
            this.viewEventsButton.Click += new System.EventHandler(this.viewEventsButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(-1, -2);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(200, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "UPCOMING EVENTS";
            // 
            // eventCalendar
            // 
            this.eventCalendar.Enabled = false;
            this.eventCalendar.ImeMode = System.Windows.Forms.ImeMode.On;
            this.eventCalendar.Location = new System.Drawing.Point(3, 37);
            this.eventCalendar.Name = "eventCalendar";
            this.eventCalendar.Size = new System.Drawing.Size(275, 20);
            this.eventCalendar.TabIndex = 0;
            // 
            // eventsListPanel
            // 
            this.eventsListPanel.AutoScroll = true;
            this.eventsListPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.eventsListPanel.Location = new System.Drawing.Point(3, 63);
            this.eventsListPanel.Name = "eventsListPanel";
            this.eventsListPanel.Size = new System.Drawing.Size(275, 265);
            this.eventsListPanel.TabIndex = 2;
            // 
            // costumesRentedPanel
            // 
            this.costumesRentedPanel.BackColor = System.Drawing.Color.RosyBrown;
            this.costumesRentedPanel.Controls.Add(this.panel3);
            this.costumesRentedPanel.Controls.Add(this.rentedCostumesPanel);
            this.costumesRentedPanel.Location = new System.Drawing.Point(1510, 61);
            this.costumesRentedPanel.Margin = new System.Windows.Forms.Padding(1);
            this.costumesRentedPanel.Name = "costumesRentedPanel";
            this.costumesRentedPanel.Size = new System.Drawing.Size(283, 578);
            this.costumesRentedPanel.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.viewRentedCostumesButton);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(275, 28);
            this.panel3.TabIndex = 1;
            // 
            // viewRentedCostumesButton
            // 
            this.viewRentedCostumesButton.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewRentedCostumesButton.Location = new System.Drawing.Point(200, 3);
            this.viewRentedCostumesButton.Name = "viewRentedCostumesButton";
            this.viewRentedCostumesButton.Size = new System.Drawing.Size(72, 22);
            this.viewRentedCostumesButton.TabIndex = 9;
            this.viewRentedCostumesButton.Text = "View More";
            this.viewRentedCostumesButton.UseVisualStyleBackColor = true;
            this.viewRentedCostumesButton.Click += new System.EventHandler(this.viewRentedCostumesButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(-1, -2);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(97, 30);
            this.label3.TabIndex = 1;
            this.label3.Text = "RENTED";
            // 
            // rentedCostumesPanel
            // 
            this.rentedCostumesPanel.AutoScroll = true;
            this.rentedCostumesPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rentedCostumesPanel.Location = new System.Drawing.Point(3, 37);
            this.rentedCostumesPanel.Name = "rentedCostumesPanel";
            this.rentedCostumesPanel.Size = new System.Drawing.Size(275, 536);
            this.rentedCostumesPanel.TabIndex = 3;
            // 
            // newestCostumesPanel
            // 
            this.newestCostumesPanel.BackColor = System.Drawing.Color.RosyBrown;
            this.newestCostumesPanel.Controls.Add(this.panel4);
            this.newestCostumesPanel.Controls.Add(this.newCostumesListPanel);
            this.newestCostumesPanel.Location = new System.Drawing.Point(1510, 652);
            this.newestCostumesPanel.Margin = new System.Windows.Forms.Padding(1);
            this.newestCostumesPanel.Name = "newestCostumesPanel";
            this.newestCostumesPanel.Size = new System.Drawing.Size(283, 299);
            this.newestCostumesPanel.TabIndex = 7;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(275, 28);
            this.panel4.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(-1, -2);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(187, 30);
            this.label4.TabIndex = 1;
            this.label4.Text = "NEW COSTUMES";
            // 
            // newCostumesListPanel
            // 
            this.newCostumesListPanel.AutoScroll = true;
            this.newCostumesListPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.newCostumesListPanel.Location = new System.Drawing.Point(3, 37);
            this.newCostumesListPanel.Name = "newCostumesListPanel";
            this.newCostumesListPanel.Size = new System.Drawing.Size(275, 258);
            this.newCostumesListPanel.TabIndex = 3;
            // 
            // MTOPanel
            // 
            this.MTOPanel.BackColor = System.Drawing.Color.RosyBrown;
            this.MTOPanel.Controls.Add(this.panel5);
            this.MTOPanel.Controls.Add(this.mtoListPanel);
            this.MTOPanel.Location = new System.Drawing.Point(10, 408);
            this.MTOPanel.Margin = new System.Windows.Forms.Padding(1);
            this.MTOPanel.Name = "MTOPanel";
            this.MTOPanel.Size = new System.Drawing.Size(283, 250);
            this.MTOPanel.TabIndex = 8;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.viewMTOButton);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Location = new System.Drawing.Point(3, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(275, 28);
            this.panel5.TabIndex = 1;
            // 
            // viewMTOButton
            // 
            this.viewMTOButton.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewMTOButton.Location = new System.Drawing.Point(200, 3);
            this.viewMTOButton.Name = "viewMTOButton";
            this.viewMTOButton.Size = new System.Drawing.Size(72, 22);
            this.viewMTOButton.TabIndex = 2;
            this.viewMTOButton.Text = "View More";
            this.viewMTOButton.UseVisualStyleBackColor = true;
            this.viewMTOButton.Click += new System.EventHandler(this.viewMTOButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(-1, -2);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(63, 30);
            this.label5.TabIndex = 1;
            this.label5.Text = "MTO";
            // 
            // mtoListPanel
            // 
            this.mtoListPanel.AutoScroll = true;
            this.mtoListPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.mtoListPanel.Location = new System.Drawing.Point(3, 37);
            this.mtoListPanel.Name = "mtoListPanel";
            this.mtoListPanel.Size = new System.Drawing.Size(275, 208);
            this.mtoListPanel.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.RosyBrown;
            this.flowLayoutPanel1.Controls.Add(this.panel6);
            this.flowLayoutPanel1.Controls.Add(this.transactionTotalsPanel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 672);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(1);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(283, 279);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.White;
            this.panel6.Controls.Add(this.viewPaymentsButton);
            this.panel6.Controls.Add(this.label9);
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(275, 28);
            this.panel6.TabIndex = 1;
            // 
            // viewPaymentsButton
            // 
            this.viewPaymentsButton.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewPaymentsButton.Location = new System.Drawing.Point(200, 3);
            this.viewPaymentsButton.Name = "viewPaymentsButton";
            this.viewPaymentsButton.Size = new System.Drawing.Size(72, 22);
            this.viewPaymentsButton.TabIndex = 2;
            this.viewPaymentsButton.Text = "View More";
            this.viewPaymentsButton.UseVisualStyleBackColor = true;
            this.viewPaymentsButton.Click += new System.EventHandler(this.viewPaymentsButton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(-1, -2);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label9.Size = new System.Drawing.Size(127, 30);
            this.label9.TabIndex = 1;
            this.label9.Text = "PAYMENTS";
            // 
            // transactionTotalsPanel
            // 
            this.transactionTotalsPanel.AutoScroll = true;
            this.transactionTotalsPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.transactionTotalsPanel.Location = new System.Drawing.Point(3, 37);
            this.transactionTotalsPanel.Name = "transactionTotalsPanel";
            this.transactionTotalsPanel.Size = new System.Drawing.Size(275, 238);
            this.transactionTotalsPanel.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Black", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(275, -8);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(297, 45);
            this.label6.TabIndex = 9;
            this.label6.Text = "Monthly Revenue";
            // 
            // monthPicker
            // 
            this.monthPicker.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            this.monthPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.monthPicker.Location = new System.Drawing.Point(392, 34);
            this.monthPicker.MinDate = new System.DateTime(2022, 12, 4, 0, 0, 0, 0);
            this.monthPicker.Name = "monthPicker";
            this.monthPicker.Size = new System.Drawing.Size(157, 29);
            this.monthPicker.TabIndex = 11;
            this.monthPicker.ValueChanged += new System.EventHandler(this.monthPicker_ValueChanged_1);
            this.monthPicker.MouseLeave += new System.EventHandler(this.monthPicker_MouseLeave);
            // 
            // revenueLabel
            // 
            this.revenueLabel.AutoSize = true;
            this.revenueLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12.25F, System.Drawing.FontStyle.Bold);
            this.revenueLabel.Location = new System.Drawing.Point(291, 37);
            this.revenueLabel.Name = "revenueLabel";
            this.revenueLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.revenueLabel.Size = new System.Drawing.Size(147, 23);
            this.revenueLabel.TabIndex = 12;
            this.revenueLabel.Text = "Monthly Revenue";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label17);
            this.panel7.Controls.Add(this.label12);
            this.panel7.Controls.Add(this.label14);
            this.panel7.Controls.Add(this.label11);
            this.panel7.Controls.Add(this.label7);
            this.panel7.Controls.Add(this.revenueChart);
            this.panel7.Controls.Add(this.totalRevenueLabel);
            this.panel7.Controls.Add(this.label8);
            this.panel7.Controls.Add(this.monthPicker);
            this.panel7.Controls.Add(this.revenueLabel);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Controls.Add(this.totalMTO);
            this.panel7.Controls.Add(this.totalActiveEvents);
            this.panel7.Controls.Add(this.totalRentedCostumes);
            this.panel7.Controls.Add(this.totalCostumesLabel);
            this.panel7.Location = new System.Drawing.Point(307, 61);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1188, 890);
            this.panel7.TabIndex = 13;
            // 
            // totalRevenueLabel
            // 
            this.totalRevenueLabel.AutoSize = true;
            this.totalRevenueLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12.25F, System.Drawing.FontStyle.Bold);
            this.totalRevenueLabel.Location = new System.Drawing.Point(594, 37);
            this.totalRevenueLabel.Name = "totalRevenueLabel";
            this.totalRevenueLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.totalRevenueLabel.Size = new System.Drawing.Size(118, 23);
            this.totalRevenueLabel.TabIndex = 15;
            this.totalRevenueLabel.Text = "Total Revenue";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Black", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(578, -8);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(245, 45);
            this.label8.TabIndex = 13;
            this.label8.Text = "Total Revenue";
            // 
            // refreshDashboardButton
            // 
            this.refreshDashboardButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refreshDashboardButton.Image = global::DavaoCosplayShopCMS.Properties.Resources.refresh_page_option;
            this.refreshDashboardButton.Location = new System.Drawing.Point(1764, 7);
            this.refreshDashboardButton.Name = "refreshDashboardButton";
            this.refreshDashboardButton.Size = new System.Drawing.Size(30, 30);
            this.refreshDashboardButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.refreshDashboardButton.TabIndex = 1;
            this.refreshDashboardButton.TabStop = false;
            this.refreshDashboardButton.Click += new System.EventHandler(this.refreshDashboardButton_Click);
            // 
            // revenueChart
            // 
            chartArea1.Name = "ChartArea1";
            this.revenueChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.revenueChart.Legends.Add(legend1);
            this.revenueChart.Location = new System.Drawing.Point(227, 84);
            this.revenueChart.Name = "revenueChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.revenueChart.Series.Add(series1);
            this.revenueChart.Size = new System.Drawing.Size(745, 626);
            this.revenueChart.TabIndex = 16;
            this.revenueChart.Text = "chart1";
            // 
            // totalCostumesLabel
            // 
            this.totalCostumesLabel.AutoSize = true;
            this.totalCostumesLabel.BackColor = System.Drawing.Color.Transparent;
            this.totalCostumesLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalCostumesLabel.Location = new System.Drawing.Point(1069, 27);
            this.totalCostumesLabel.Name = "totalCostumesLabel";
            this.totalCostumesLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.totalCostumesLabel.Size = new System.Drawing.Size(88, 30);
            this.totalCostumesLabel.TabIndex = 17;
            this.totalCostumesLabel.Text = "number";
            this.totalCostumesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(1004, 6);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(181, 30);
            this.label7.TabIndex = 18;
            this.label7.Text = "Total Costumes";
            // 
            // totalRentedCostumes
            // 
            this.totalRentedCostumes.AutoSize = true;
            this.totalRentedCostumes.BackColor = System.Drawing.Color.Transparent;
            this.totalRentedCostumes.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalRentedCostumes.Location = new System.Drawing.Point(1069, 81);
            this.totalRentedCostumes.Name = "totalRentedCostumes";
            this.totalRentedCostumes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.totalRentedCostumes.Size = new System.Drawing.Size(88, 30);
            this.totalRentedCostumes.TabIndex = 19;
            this.totalRentedCostumes.Text = "number";
            this.totalRentedCostumes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(1015, 58);
            this.label11.Name = "label11";
            this.label11.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label11.Size = new System.Drawing.Size(151, 30);
            this.label11.TabIndex = 20;
            this.label11.Text = "Total Rented";
            // 
            // totalMTO
            // 
            this.totalMTO.AutoSize = true;
            this.totalMTO.BackColor = System.Drawing.Color.Transparent;
            this.totalMTO.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalMTO.Location = new System.Drawing.Point(94, 81);
            this.totalMTO.Name = "totalMTO";
            this.totalMTO.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.totalMTO.Size = new System.Drawing.Size(88, 30);
            this.totalMTO.TabIndex = 23;
            this.totalMTO.Text = "number";
            this.totalMTO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.totalMTO.Click += new System.EventHandler(this.totalMTO_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(47, 58);
            this.label12.Name = "label12";
            this.label12.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label12.Size = new System.Drawing.Size(128, 30);
            this.label12.TabIndex = 24;
            this.label12.Text = "Total MTO";
            // 
            // totalActiveEvents
            // 
            this.totalActiveEvents.AutoSize = true;
            this.totalActiveEvents.BackColor = System.Drawing.Color.Transparent;
            this.totalActiveEvents.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalActiveEvents.Location = new System.Drawing.Point(94, 27);
            this.totalActiveEvents.Name = "totalActiveEvents";
            this.totalActiveEvents.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.totalActiveEvents.Size = new System.Drawing.Size(88, 30);
            this.totalActiveEvents.TabIndex = 21;
            this.totalActiveEvents.Text = "number";
            this.totalActiveEvents.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.totalActiveEvents.Click += new System.EventHandler(this.totalActiveEvents_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(6, 6);
            this.label14.Name = "label14";
            this.label14.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label14.Size = new System.Drawing.Size(221, 30);
            this.label14.TabIndex = 22;
            this.label14.Text = "Total Active Events";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI Light", 8F, System.Drawing.FontStyle.Italic);
            this.label17.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label17.Location = new System.Drawing.Point(452, 684);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(218, 13);
            this.label17.TabIndex = 98;
            this.label17.Text = "Only months with payment records are shown.";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1803, 961);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.MTOPanel);
            this.Controls.Add(this.newestCostumesPanel);
            this.Controls.Add(this.eventsPanel);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.costumesRentedPanel);
            this.Controls.Add(this.panel7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "dashboardForm";
            this.Text = "dashboardForm";
            this.Load += new System.EventHandler(this.dashboardForm_Load);
            this.topPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.eventsPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.costumesRentedPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.newestCostumesPanel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.MTOPanel.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refreshDashboardButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.revenueChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel eventsPanel;
        private System.Windows.Forms.DateTimePicker eventCalendar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button viewEventsButton;
        private System.Windows.Forms.FlowLayoutPanel costumesRentedPanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button viewRentedButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel eventsListPanel;
        private System.Windows.Forms.FlowLayoutPanel rentedCostumesPanel;
        private System.Windows.Forms.FlowLayoutPanel newestCostumesPanel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FlowLayoutPanel newCostumesListPanel;
        private System.Windows.Forms.FlowLayoutPanel MTOPanel;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button viewMTOButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FlowLayoutPanel mtoListPanel;
        private System.Windows.Forms.Button viewRentedCostumesButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button viewPaymentsButton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.FlowLayoutPanel transactionTotalsPanel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker monthPicker;
        private System.Windows.Forms.Label revenueLabel;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label totalRevenueLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox refreshDashboardButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart revenueChart;
        private System.Windows.Forms.Label totalRentedCostumes;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label totalCostumesLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label totalMTO;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label totalActiveEvents;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label17;
    }
}