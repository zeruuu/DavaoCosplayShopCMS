using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DavaoCosplayShopCMS
{
    public partial class paymentsForm : Form
    {
        connection con = new connection();
        SqlCommand cmd = new SqlCommand();
        SqlConnection cq;
        SqlDataReader rd;
        public paymentsForm()
        {
            InitializeComponent();
            loadPaymentsPanel();
        }

        private void paymentsForm_Load(object sender, EventArgs e)
        {
            loadPaymentsPanel();
        }
        private void loadPaymentsPanel()
        {
            paymentsFlowPanel.Controls.Clear();

            using (SqlConnection cq = con.getCon()) // Ensures proper disposal
            {
                cq.Open();
                string query = "SELECT * FROM TransactionTotals ORDER BY transaction_ID DESC";

                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, cq))
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            int transactionID = Convert.ToInt32(rd["transaction_ID"]);
                            int clientID = Convert.ToInt32(rd["client_ID"]);
                            string clientName = rd["client_Name"].ToString();
                            string transactionDate = Convert.ToDateTime(rd["transaction_Date"]).ToString("yyyy-MM-dd");
                            decimal balance = Convert.ToDecimal(rd["balance"]);
                            decimal total = Convert.ToDecimal(rd["Total"]);
                            decimal remaining = Convert.ToDecimal(rd["Remaining"]);

                            Panel clientPanel = new Panel
                            {
                                Width = 220,
                                AutoSize = true,
                                MinimumSize = new Size(0, 200),
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
                                Width = 240,
                                MinimumSize = new Size(240, 0),
                                Padding = new Padding(0),
                            };

                            Label transactionIDLabel = new Label
                            {
                                Text = $"Transaction ID: {transactionID}",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                                ForeColor = Color.Black,
                            };

                            Label nameLabel = new Label
                            {
                                Text = $"{clientName} [ID: {clientID}]",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                                ForeColor = Color.Black,
                            };

                            Label transactionDateLabel = new Label
                            {
                                Text = transactionDate,
                                AutoSize = true,
                                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                                ForeColor = Color.DarkGray,
                            };

                            Label balanceLabel = new Label
                            {
                                Text = $"Balance: ₱{balance}",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                                ForeColor = Color.Green,
                            };

                            Label totalLabel = new Label
                            {
                                Text = $"Total: ₱{total}",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                                ForeColor = Color.Black,
                            };

                            Label remainingLabel = new Label
                            {
                                Text = $"Remaining: ₱{remaining}",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                                ForeColor = (remaining <= 0) ? Color.DarkGray : Color.Red
                            };

                            // "View Details" Button
                            Button detailsButton = new Button
                            {
                                Text = "View Details",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                                BackColor = Color.LightBlue,
                                ForeColor = Color.Black,
                                Cursor = Cursors.Hand
                            };
                            detailsButton.Click += (sender, e) => ShowPaymentDetails(clientID);

                            // "Make Payment" Button
                            Button makePaymentButton = new Button
                            {
                                Text = "Make Payment",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                                BackColor = Color.LightGreen,
                                ForeColor = Color.Black,
                                Cursor = Cursors.Hand,
                                Enabled = (remaining > 0),
                                Visible = (remaining > 0)
                            };

                            if (remaining > 0)
                            {
                                makePaymentButton.Click += (sender, e) => OpenClientPaymentForm(clientID, transactionID);
                            }

                            // "Add Charges" Button
                            Button addChargesButton = new Button
                            {
                                Text = "Add Charges",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                                BackColor = Color.Orange,
                                ForeColor = Color.Black,
                                Cursor = Cursors.Hand
                            };
                            addChargesButton.Click += (sender, e) => OpenClientChargesForm(clientID, transactionID);

                            // Add controls to panel (Ensure correct order)
                            flowPanel.Controls.Add(transactionIDLabel);
                            flowPanel.Controls.Add(nameLabel);
                            flowPanel.Controls.Add(transactionDateLabel);
                            flowPanel.Controls.Add(balanceLabel);
                            flowPanel.Controls.Add(totalLabel);
                            flowPanel.Controls.Add(remainingLabel);
                            flowPanel.Controls.Add(detailsButton);
                            flowPanel.Controls.Add(makePaymentButton);
                            flowPanel.Controls.Add(addChargesButton); // Add "Add Charges" button

                            // Load rental-related details
                            LoadRentedCostumes(transactionID, flowPanel);
                            LoadMTOCostumes(transactionID, flowPanel);
                            LoadAvailedServices(transactionID, flowPanel);
                            LoadTransactionCharges(transactionID, flowPanel);

                            clientPanel.Controls.Add(flowPanel);
                            paymentsFlowPanel.Controls.Add(clientPanel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading payments: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadRentedCostumes(int transactionID, FlowLayoutPanel panel)
        {
            using (SqlConnection cq = con.getCon())
            {
                cq.Open();
                string query = @"SELECT C.costume_Name FROM Rents R
                         JOIN Costume C ON R.costume_ID = C.costume_ID
                         WHERE R.transaction_ID = @transactionID";

                using (SqlCommand cmd = new SqlCommand(query, cq))
                {
                    cmd.Parameters.AddWithValue("@transactionID", transactionID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Label rentLabel = new Label
                            {
                                Text = $"🟢 Rented: {reader["costume_Name"]}",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                                ForeColor = Color.DarkBlue,
                            };
                            panel.Controls.Add(rentLabel);
                        }
                    }
                }
            }
        }

        private void LoadMTOCostumes(int transactionID, FlowLayoutPanel panel)
        {
            using (SqlConnection cq = con.getCon())
            {
                cq.Open();
                string query = @"SELECT MO.mto_Title FROM MTODetails M
                         JOIN MTO MO ON M.mto_ID = MO.mto_ID
                         WHERE M.transaction_ID = @transactionID";

                using (SqlCommand cmd = new SqlCommand(query, cq))
                {
                    cmd.Parameters.AddWithValue("@transactionID", transactionID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Label mtoLabel = new Label
                            {
                                Text = $"🟡 MTO: {reader["mto_Title"]}",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                                ForeColor = Color.DarkOrange,
                            };
                            panel.Controls.Add(mtoLabel);
                        }
                    }
                }
            }
        }

        private void LoadAvailedServices(int transactionID, FlowLayoutPanel panel)
        {
            using (SqlConnection cq = con.getCon())
            {
                cq.Open();
                string query = @"SELECT ST.serviceType_Name FROM ServiceAvail SA
                         JOIN ServiceType ST ON SA.serviceType_ID = ST.serviceType_ID
                         WHERE SA.transaction_ID = @transactionID";

                using (SqlCommand cmd = new SqlCommand(query, cq))
                {
                    cmd.Parameters.AddWithValue("@transactionID", transactionID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Label serviceLabel = new Label
                            {
                                Text = $"🔵 Service: {reader["serviceType_Name"]}",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                                ForeColor = Color.DarkGreen,
                            };
                            panel.Controls.Add(serviceLabel);
                        }
                    }
                }
            }
        }

        private void LoadTransactionCharges(int transactionID, FlowLayoutPanel panel)
        {
            using (SqlConnection cq = con.getCon())
            {
                cq.Open();
                string query = @"
            SELECT C.Charge_Name, CD.charge_Fee, CD.charge_Description
            FROM ChargeDetails CD
            JOIN Charge C ON CD.charge_ID = C.charge_ID
            WHERE CD.transaction_ID = @transactionID";

                using (SqlCommand cmd = new SqlCommand(query, cq))
                {
                    cmd.Parameters.AddWithValue("@transactionID", transactionID);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string chargeName = reader["Charge_Name"].ToString();
                            decimal chargeFee = Convert.ToDecimal(reader["charge_Fee"]);
                            string chargeDescription = reader["charge_Description"].ToString();

                            Label chargeLabel = new Label
                            {
                                Text = $"💰 Charge: {chargeName} - ₱{chargeFee:N2} ({chargeDescription})",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 8, FontStyle.Italic),
                                ForeColor = Color.DarkRed,
                            };
                            panel.Controls.Add(chargeLabel);
                        }
                    }
                }
            }
        }

        private void OpenClientChargesForm(int clientID, int transactionID)
        {
            clientChargesForm chargesForm = new clientChargesForm(clientID, transactionID);
            chargesForm.FormClosed += (s, args) => loadPaymentsPanel(); // Refresh payments after closing
            chargesForm.ShowDialog();
        }

        private void OpenClientPaymentForm(int clientID, int transactionID)
        {
            clientPaymentForm paymentForm = new clientPaymentForm(clientID, transactionID);
            paymentForm.FormClosed += (s, args) => loadPaymentsPanel(); // refresh payments after closing
            paymentForm.ShowDialog();
        }

        private void ShowPaymentDetails(int clientID)
        {
            using (SqlConnection connection = con.getCon())
            {
                connection.Open();
                string query = @"
                SELECT TransactionType, SUM(Amount) as TotalAmount 
                FROM ClientTransactions
                WHERE client_ID = @clientID
                GROUP BY TransactionType

                UNION ALL

                SELECT 'Services' AS TransactionType, SUM(service_Amount) 
                FROM ServiceAvail
                WHERE transaction_ID IN (
                    SELECT transaction_ID FROM ClientTransactions WHERE client_ID = @clientID
                )";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@clientID", clientID);

                    Dictionary<string, decimal> transactionSums = new Dictionary<string, decimal>(); 

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string type = reader["TransactionType"].ToString();
                            decimal amount = reader["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalAmount"]) : 0;

                            transactionSums[type] = amount;
                        }
                    }

                    // Extracting amounts or defaulting to 0
                    decimal rent = transactionSums.ContainsKey("Rent") ? transactionSums["Rent"] : 0;
                    decimal charges = transactionSums.ContainsKey("Charge") ? transactionSums["Charge"] : 0;
                    decimal mto = transactionSums.ContainsKey("MTO") ? transactionSums["MTO"] : 0;
                    decimal services = transactionSums.ContainsKey("Services") ? transactionSums["Services"] : 0;
                    decimal payments = transactionSums.ContainsKey("Payment") ? transactionSums["Payment"] : 0;
                    decimal remaining = (rent + charges + mto + services) + payments; // Payments are negative

                    string details = $"Client ID: {clientID}\n\n" +
                                     $"Rent: ₱{rent}\n" +
                                     $"Charges: ₱{charges}\n" +
                                     $"MTO: ₱{mto}\n" +
                                     $"Services: ₱{services}\n" +
                                     $"Total Payments: ₱{payments}\n" +
                                     $"Remaining Balance to Pay: ₱{remaining}";

                    MessageBox.Show(details, "Payment Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void viewHistoryButton_Click(object sender, EventArgs e)
        {
            transactionHistory transactionHistoryForm = new transactionHistory();
            transactionHistoryForm.ShowDialog();
        }
    }
} ////
