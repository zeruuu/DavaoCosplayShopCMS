using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DavaoCosplayShopCMS
{
    public partial class dashboardForm : Form  
    {
        connection con = new connection();
        SqlCommand cmd = new SqlCommand();
        SqlConnection cq;
        SqlDataReader rd;

        public dashboardForm()
        {
            InitializeComponent();
            loadDashboardData(); // load data
            loadDashboardStatistics();
            configureMonthPickerRevenue();
            loadMonthlyRevenue(DateTime.Now.Year, DateTime.Now.Month); // default to current month
            loadTotalRevenue();
        }
        private void refreshDashboardButton_Click(object sender, EventArgs e)
        {
            loadDashboardData(); // load data
            loadDashboardStatistics();
            configureMonthPickerRevenue();
            loadMonthlyRevenue(DateTime.Now.Year, DateTime.Now.Month); // default to current month
            loadTotalRevenue();
        }
        private void configureMonthPickerRevenue()
        {
            monthPicker.Format = DateTimePickerFormat.Custom;
            monthPicker.CustomFormat = "MMMM yyyy";
            monthPicker.ShowUpDown = true;
            monthPicker.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); // Set to the first day of the current month
        }

        private void loadDashboardStatistics()
        {
            using (SqlConnection cq = con.getCon())
            {
                cq.Open();
                try
                {
                    // Query to count total costumes
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Costume", cq))
                    {
                        totalCostumesLabel.Text = cmd.ExecuteScalar().ToString();
                    }

                    // Query to count rented costumes (assuming there's a `costume_returned` column)
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Rents WHERE costume_returned = 0", cq))
                    {
                        totalRentedCostumes.Text = cmd.ExecuteScalar().ToString();
                    }

                    // Query to count active events (assuming `event_Status = 'Active'`)
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Event WHERE event_Active = '1'", cq))
                    {
                        totalActiveEvents.Text = cmd.ExecuteScalar().ToString();
                    }

                    // Query to count MTO (assuming MTO orders are in a table called `MTO`)
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM MTO", cq))
                    {
                        totalMTO.Text = cmd.ExecuteScalar().ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading dashboard statistics: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void loadMonthlyRevenue(int year, int month)
        {
            decimal revenue = 0;
            using (SqlConnection cq = con.getCon())
            {
                cq.Open();
                using (SqlCommand cmd = new SqlCommand("GetMonthlyRevenue", cq))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.Parameters.AddWithValue("@month", month);

                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        revenue = Convert.ToDecimal(result);
                    }
                }
            }

            // ✅ Update Revenue Label
            revenueLabel.Text = $"₱{revenue:N2}";

            // ✅ Update Revenue Chart
            UpdateRevenueChart(year);
        }
        private void UpdateRevenueChart(int year)
        {
            revenueChart.Series.Clear(); // Clear old data

            Series series = new Series("Revenue");
            series.ChartType = SeriesChartType.Column; // Column/Bar Chart
            series.Color = System.Drawing.Color.CornflowerBlue;

            using (SqlConnection cq = con.getCon())
            {
                cq.Open();
                using (SqlCommand cmd = new SqlCommand("GetYearlyRevenue", cq))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@year", year);

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            string month = rd["MonthName"].ToString(); // Month Name (January, February, etc.)
                            decimal monthlyRevenue = Convert.ToDecimal(rd["TotalRevenue"]);

                            series.Points.AddXY(month, monthlyRevenue);
                        }
                    }
                }
            }

            revenueChart.Series.Add(series);
        }
        private void loadTotalRevenue()
        {
            decimal totalRevenue = 0;
            using (SqlConnection cq = con.getCon())
            {
                cq.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT SUM(payment_Amount) FROM Payment", cq))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        totalRevenue = Convert.ToDecimal(result);
                    }
                }
            }

            totalRevenueLabel.Text = $"₱{totalRevenue:N2}"; // Update the label
        }
        private void monthPicker_ValueChanged_1(object sender, EventArgs e)
        {
            int selectedYear = monthPicker.Value.Year;
            int selectedMonth = monthPicker.Value.Month;
            loadMonthlyRevenue(selectedYear, selectedMonth);
        }
        private void dashboardForm_Load(object sender, EventArgs e)
        {
            loadDashboardData(); 
        }
        private void dashboardForm_Activated(object sender, EventArgs e)
        {
            loadDashboardData(); 
        }
        public void loadDashboardData()
        {
            loadEventsPanel();
            loadCostumesPanel();
            loadRentedCostumesPanel();
            loadMTOPanel();
            loadTransactionTotalsPanel();
            DateTime date = DateTime.Now;
            eventCalendar.Value = date;
        }

        // View Buttons
        private void viewEventsButton_Click(object sender, EventArgs e)
        {
            viewEventsForm viewEventsForm = new viewEventsForm();
            viewEventsForm.FormClosed += (s, args) => loadEventsPanel(); // refresh events panel after closing
            viewEventsForm.Show();
        }
        private void viewMTOButton_Click(object sender, EventArgs e)
        {
            viewMTOForm viewMTOForm = new viewMTOForm();
            viewMTOForm.Show();
        }
        private void viewRentedCostumesButton_Click(object sender, EventArgs e)
        {
            Form currentForm = this; // Get the current form before closing

            if (MdiParent is mainForm parentForm) // Ensure the parent is mainForm
            {
                if (parentForm.rentsForm == null)
                {
                    parentForm.rentsForm = new rentsForm();
                    parentForm.rentsForm.FormClosed += parentForm.rentsForm_FormClosed;
                    parentForm.rentsForm.MdiParent = parentForm;
                    parentForm.rentsForm.Dock = DockStyle.Fill;
                    parentForm.rentsForm.Show();
                }
                else
                {
                    parentForm.rentsForm.Activate();
                }
            }

            currentForm.Close(); // Close the current child form before opening the new one
        }
        private void viewPaymentsButton_Click(object sender, EventArgs e)
        {
            Form currentForm = this; // Get the current form before closing

            if (MdiParent is mainForm parentForm) // Ensure the parent is mainForm
            {
                if (parentForm.paymentsForm == null)
                {
                    parentForm.paymentsForm = new paymentsForm();
                    parentForm.paymentsForm.FormClosed += parentForm.Payments_FormClosed;
                    parentForm.paymentsForm.MdiParent = parentForm;
                    parentForm.paymentsForm.Dock = DockStyle.Fill;
                    parentForm.paymentsForm.Show();
                }
                else
                {
                    parentForm.paymentsForm.Activate();
                }
            }

            currentForm.Close(); // Close the current child form before opening the new one
        }

        // Events Panel
        private void loadEventsPanel()
        {
            eventsListPanel.Controls.Clear();

            cq = con.getCon();
            cq.Open();
            using (cq)
            {
                string query = "SELECT event_Name, event_Location, event_Date FROM Event WHERE event_Date >= GETDATE() AND event_Active = 1 ORDER BY event_Date ASC";
                cmd = new SqlCommand(query, cq);
                using (rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string eventName = rd["event_Name"].ToString();
                        string eventLocation = rd["event_Location"].ToString();
                        string eventDate = Convert.ToDateTime(rd["event_Date"]).ToString("MM-dd-yyyy");

                        Panel eventPanel = new Panel
                        {
                            Width = 194,
                            AutoSize = true,
                            AutoSizeMode = AutoSizeMode.GrowAndShrink,
                            BorderStyle = BorderStyle.FixedSingle,
                            Padding = new Padding(5),
                            BackColor = Color.White,
                        };

                        FlowLayoutPanel flowPanel = new FlowLayoutPanel
                        {
                            FlowDirection = FlowDirection.TopDown,
                            AutoSize = true,
                            AutoSizeMode = AutoSizeMode.GrowAndShrink,
                            WrapContents = false,
                            Width = 180,
                            MinimumSize = new Size(250, 0),
                            Padding = new Padding(0),
                        };

                        Label eventNameLabel = new Label
                        {
                            Text = eventName,
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Bold),
                            ForeColor = Color.Black,
                        };

                        Label eventLocationLabel = new Label
                        {
                            Text = eventLocation,
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Regular),
                            ForeColor = Color.DarkGray,
                        };

                        Label eventDateLabel = new Label
                        {
                            Text = eventDate,
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Italic),
                            ForeColor = Color.Gray,
                        };

                        flowPanel.Controls.Add(eventNameLabel);
                        flowPanel.Controls.Add(eventLocationLabel);
                        flowPanel.Controls.Add(eventDateLabel);

                        eventPanel.Controls.Add(flowPanel);

                        eventsListPanel.Controls.Add(eventPanel);
                    }
                }
            }
        }
        // MTO Panel
        private void loadMTOPanel()
        {
            mtoListPanel.Controls.Clear();

            cq = con.getCon();
            cq.Open();
            using (cq)
            {
                string query = "SELECT TOP 5 * FROM MTOClientDetails ORDER BY mto_Finished ASC";

                cmd = new SqlCommand(query, cq);
                using (rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string mtoTitle = rd["mto_Title"].ToString();
                        string clientName = rd["client_Name"].ToString();
                        string mtoFee = Convert.ToDecimal(rd["mto_Fee"]).ToString();
                        string mtoDescription = rd["mto_Description"].ToString();
                        bool isFinished = Convert.ToInt32(rd["mto_Finished"]) == 1;

                        Panel mtoPanel = new Panel
                        {
                            Width = 194,
                            AutoSize = true,
                            AutoSizeMode = AutoSizeMode.GrowAndShrink,
                            BorderStyle = BorderStyle.FixedSingle,
                            Padding = new Padding(5),
                            BackColor = Color.White,
                        };

                        FlowLayoutPanel flowPanel = new FlowLayoutPanel
                        {
                            FlowDirection = FlowDirection.TopDown,
                            AutoSize = true,
                            AutoSizeMode = AutoSizeMode.GrowAndShrink,
                            WrapContents = false,
                            Width = 180,
                            MinimumSize = new Size(240, 0),
                            Padding = new Padding(0),
                        };

                        Label titleLabel = new Label
                        {
                            Text = mtoTitle,
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Bold),
                            ForeColor = Color.Black,
                        };

                        Label nameLabel = new Label
                        {
                            Text = $"Client: {clientName}",
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Regular),
                            ForeColor = Color.Black,
                        };


                        Label feeLabel = new Label
                        {
                            Text = $"Fee: ₱{mtoFee}",
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Regular),
                            ForeColor = Color.DarkGreen,
                        };

                        Label descriptionLabel = new Label
                        {
                            Text = mtoDescription,
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Italic),
                            ForeColor = Color.Gray,
                        };
                        Label statusLabel = new Label
                        {
                            Text = isFinished ? "✔ Completed" : "⏳ In Progress",
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Bold),
                            ForeColor = isFinished ? Color.Green : Color.Red,
                        };

                        flowPanel.Controls.Add(titleLabel);
                        flowPanel.Controls.Add(nameLabel);
                        flowPanel.Controls.Add(feeLabel);
                        flowPanel.Controls.Add(descriptionLabel);
                        flowPanel.Controls.Add(statusLabel);

                        mtoPanel.Controls.Add(flowPanel);

                        mtoListPanel.Controls.Add(mtoPanel);
                    }
                }
            }
        }
        // New Costumes Panel
        private void loadCostumesPanel()
        {
            newCostumesListPanel.Controls.Clear();

            cq = con.getCon();
            cq.Open();
            using (cq)
            {
                string query = "SELECT TOP 5 " +
                    "C.costume_Name, " +
                    "C.costume_Origin, " +
                    "CT.costumeType_Name, " +
                    "CS.costumeSize_Name, " +
                    "CG.costumeGender_Name, " +
                    "C.costume_Price, " +
                    "C.costume_IMG " +  // Include the image column
                    "FROM Costume C " +
                    "JOIN CostumeType CT ON C.costumeType_ID = CT.costumeType_ID " +
                    "JOIN CostumeSize CS ON C.costumeSize_ID = CS.costumeSize_ID " +
                    "JOIN CostumeGender CG ON C.costumeGender_ID = CG.costumeGender_ID " +
                    "ORDER BY C.costume_ID DESC";

                cmd = new SqlCommand(query, cq);
                using (rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string costumeName = rd["costume_Name"].ToString();
                        string costumeOrigin = rd["costume_Origin"].ToString();
                        string costumeType = rd["costumeType_Name"].ToString();
                        string costumeSize = rd["costumeSize_Name"].ToString();
                        string costumeGender = rd["costumeGender_Name"].ToString();
                        string costumePrice = Convert.ToDecimal(rd["costume_Price"]).ToString(); // Format as currency

                        // Retrieve the image
                        byte[] imageData = rd["costume_IMG"] as byte[];
                        Image costumeImage = null;

                        if (imageData != null && imageData.Length > 0)
                        {
                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                costumeImage = Image.FromStream(ms);
                            }
                        }

                        Panel costumePanel = new Panel
                        {
                            Width = 250,
                            AutoSize = true,
                            MinimumSize = new Size(250, 0),
                            AutoSizeMode = AutoSizeMode.GrowAndShrink,
                            BorderStyle = BorderStyle.FixedSingle,
                            Padding = new Padding(5),
                            BackColor = Color.White,
                        };

                        FlowLayoutPanel flowPanel = new FlowLayoutPanel
                        {
                            FlowDirection = FlowDirection.TopDown,
                            AutoSize = true,
                            AutoSizeMode = AutoSizeMode.GrowAndShrink,
                            WrapContents = false,
                            Width = 150,
                            MinimumSize = new Size(150, 0),
                            Padding = new Padding(0),
                        };

                        Label nameLabel = new Label { Text = costumeName, AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.Black };
                        Label originLabel = new Label { Text = costumeOrigin, AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Bold), ForeColor = Color.Black };
                        Label typeLabel = new Label { Text = $"Type: {costumeType}", AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Regular), ForeColor = Color.Black };
                        Label sizeLabel = new Label { Text = $"Size: {costumeSize}", AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Regular), ForeColor = Color.DarkGray };
                        Label genderLabel = new Label { Text = $"Gender: {costumeGender}", AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Italic), ForeColor = Color.Gray };
                        Label priceLabel = new Label { Text = $"Price: ₱{costumePrice}", AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Regular), ForeColor = Color.Green };

                        flowPanel.Controls.Add(nameLabel);
                        flowPanel.Controls.Add(originLabel);
                        flowPanel.Controls.Add(typeLabel);
                        flowPanel.Controls.Add(sizeLabel);
                        flowPanel.Controls.Add(genderLabel);
                        flowPanel.Controls.Add(priceLabel);

                        PictureBox costumePictureBox = new PictureBox
                        {
                            Size = new Size(80, 80), 
                            SizeMode = PictureBoxSizeMode.Zoom, 
                            BorderStyle = BorderStyle.FixedSingle,
                            BackColor = Color.LightGray, 
                            Image = costumeImage 
                        };

                        TableLayoutPanel layoutPanel = new TableLayoutPanel
                        {
                            ColumnCount = 2,
                            RowCount = 1,
                            AutoSize = true,
                            AutoSizeMode = AutoSizeMode.GrowAndShrink
                        };

                        layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                        layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90)); 

                        layoutPanel.Controls.Add(flowPanel, 0, 0);
                        layoutPanel.Controls.Add(costumePictureBox, 1, 0);

                        costumePanel.Controls.Add(layoutPanel);
                        newCostumesListPanel.Controls.Add(costumePanel);
                    }
                }
            }
        }
        // Rented Costumes Panel
        private void loadRentedCostumesPanel()
        {
            rentedCostumesPanel.Controls.Clear();

            cq = con.getCon();
            cq.Open();
            using (cq)
            {
                string query = "SELECT " +
                    "U.client_Name, " +
                    "U.costume_ID, " +
                    "C.costume_Name, " +
                    "C.costume_Origin, " +
                    "U.costume_Fee, " +
                    "U.rentDate, " +
                    "U.returnDate, " +
                    "C.costume_IMG " +
                    "FROM UnreturnedRentals U " +
                    "JOIN Costume C ON U.costume_ID = C.costume_ID " +
                    "ORDER BY U.returnDate ASC";

                cmd = new SqlCommand(query, cq);
                using (rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string clientName = rd["client_Name"].ToString();
                        string costumeName = rd["costume_Name"].ToString();
                        string costumeOrigin = rd["costume_Origin"].ToString();
                        string costumeFee = Convert.ToDecimal(rd["costume_Fee"]).ToString();
                        DateTime rentDate = Convert.ToDateTime(rd["rentDate"]);
                        DateTime returnDate = Convert.ToDateTime(rd["returnDate"]);
                        string rentDateString = rentDate.ToString("yyyy-MM-dd");
                        string returnDateString = returnDate.ToString("yyyy-MM-dd");

                        byte[] imageData = rd["costume_IMG"] as byte[];
                        Image costumeImage = null;

                        if (imageData != null && imageData.Length > 0)
                        {
                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                costumeImage = Image.FromStream(ms);
                            }
                        }

                        Panel rentalPanel = new Panel
                        {
                            Width = 250,
                            MinimumSize = new Size(250, 0),
                            AutoSize = true,
                            AutoSizeMode = AutoSizeMode.GrowAndShrink,
                            BorderStyle = BorderStyle.FixedSingle,
                            Padding = new Padding(5),
                            BackColor = Color.White,
                        };

                        FlowLayoutPanel flowPanel = new FlowLayoutPanel
                        {
                            FlowDirection = FlowDirection.TopDown,
                            AutoSize = true,
                            AutoSizeMode = AutoSizeMode.GrowAndShrink,
                            WrapContents = false,
                            Width = 150,
                            MinimumSize = new Size(150, 0),
                            Padding = new Padding(0),
                        };

                        Label nameLabel = new Label { Text = $"Rented by: \n{clientName}", AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.Black };
                        Label costumeLabel = new Label { Text = costumeName, AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Bold), ForeColor = Color.Black };
                        Label originLabel = new Label { Text = costumeOrigin, AutoSize = true, Font = new Font("Segoe UI", 7, FontStyle.Bold), ForeColor = Color.Black };
                        Label feeLabel = new Label { Text = $"Fee: ₱{costumeFee}", AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Regular), ForeColor = Color.Green };
                        Label rentDateLabel = new Label { Text = $"Rented: {rentDateString}", AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Regular), ForeColor = Color.DarkBlue };

                        Color returnDateColor = DateTime.Now > returnDate ? Color.Red : Color.Black;
                        Label returnDateLabel = new Label { Text = $"Return by: {returnDateString}", AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Regular), ForeColor = returnDateColor };

                        flowPanel.Controls.Add(nameLabel);
                        flowPanel.Controls.Add(costumeLabel);
                        flowPanel.Controls.Add(originLabel);
                        flowPanel.Controls.Add(feeLabel);
                        flowPanel.Controls.Add(rentDateLabel);
                        flowPanel.Controls.Add(returnDateLabel);

                        PictureBox costumePictureBox = new PictureBox
                        {
                            Size = new Size(90, 90),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            BorderStyle = BorderStyle.FixedSingle,
                            BackColor = Color.LightGray,
                            Image = costumeImage
                        };

                        TableLayoutPanel layoutPanel = new TableLayoutPanel
                        {
                            ColumnCount = 2,
                            RowCount = 1,
                            AutoSize = true,
                            AutoSizeMode = AutoSizeMode.GrowAndShrink
                        };

                        layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                        layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));

                        layoutPanel.Controls.Add(flowPanel, 0, 0);
                        layoutPanel.Controls.Add(costumePictureBox, 1, 0);

                        rentalPanel.Controls.Add(layoutPanel);
                        rentedCostumesPanel.Controls.Add(rentalPanel);
                    }
                }
            }
        }
        // Payments Panel
        private void loadTransactionTotalsPanel()
        {
            transactionTotalsPanel.Controls.Clear();

            using (SqlConnection cq = con.getCon())
            {
                cq.Open();
                string query = "SELECT * FROM TransactionTotals WHERE Remaining > 0 ORDER BY transaction_Date DESC";

                using (SqlCommand cmd = new SqlCommand(query, cq))
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        int transactionID = Convert.ToInt32(rd["transaction_ID"]);
                        string clientName = rd["client_Name"].ToString();
                        string transactionDate = Convert.ToDateTime(rd["transaction_Date"]).ToString("yyyy-MM-dd");
                        decimal totalAmount = Convert.ToDecimal(rd["Total"]);
                        decimal remainingBalance = Convert.ToDecimal(rd["Remaining"]);

                        // ✅ Create transaction panel
                        Panel transactionPanel = new Panel
                        {
                            Width = 194,
                            AutoSize = true,
                            AutoSizeMode = AutoSizeMode.GrowAndShrink,
                            BorderStyle = BorderStyle.FixedSingle,
                            Padding = new Padding(5),
                            BackColor = Color.White,
                        };

                        FlowLayoutPanel flowPanel = new FlowLayoutPanel
                        {
                            FlowDirection = FlowDirection.TopDown,
                            AutoSize = true,
                            AutoSizeMode = AutoSizeMode.GrowAndShrink,
                            WrapContents = false,
                            Width = 180,
                            MinimumSize = new Size(240, 0),
                            Padding = new Padding(0),
                        };

                        Label transactionIDLabel = new Label
                        {
                            Text = $"Transaction ID: {transactionID}",
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Bold),
                            ForeColor = Color.Black,
                        };

                        Label clientNameLabel = new Label
                        {
                            Text = $"Client: {clientName}",
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Regular),
                            ForeColor = Color.Black,
                        };

                        Label transactionDateLabel = new Label
                        {
                            Text = $"Date: {transactionDate}",
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Italic),
                            ForeColor = Color.Gray,
                        };

                        Label totalLabel = new Label
                        {
                            Text = $"Total: ₱{totalAmount:N2}",
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Regular),
                            ForeColor = Color.Black,
                        };

                        Label remainingLabel = new Label
                        {
                            Text = $"Remaining: ₱{remainingBalance:N2}",
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Bold),
                            ForeColor = Color.Red,
                        };

                        // ✅ Add elements to the flow panel
                        flowPanel.Controls.Add(transactionIDLabel);
                        flowPanel.Controls.Add(clientNameLabel);
                        flowPanel.Controls.Add(transactionDateLabel);
                        flowPanel.Controls.Add(totalLabel);
                        flowPanel.Controls.Add(remainingLabel);

                        // ✅ Add flow panel to main panel
                        transactionPanel.Controls.Add(flowPanel);
                        transactionTotalsPanel.Controls.Add(transactionPanel);
                    }
                }
            }
        }

        private void monthPicker_MouseLeave(object sender, EventArgs e)
        {
            this.ActiveControl = null; // remove focus when mouse outside cause annoying
        }

        private void totalMTO_Click(object sender, EventArgs e)
        {

        }

        private void totalActiveEvents_Click(object sender, EventArgs e)
        {

        }
    } ////
}
