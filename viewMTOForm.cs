using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace DavaoCosplayShopCMS
{
    public partial class viewMTOForm : Form
    {
        connection con = new connection();
        SqlCommand cmd;
        SqlConnection cq;
        SqlDataReader rd;

        public viewMTOForm()
        {
            InitializeComponent();
            loadMTOOrders();
        }

        private void viewMTOForm_Load(object sender, EventArgs e)
        {
            loadMTOOrders();
        }

        private void loadMTOOrders()
        {
            mtoFlowPanel.Controls.Clear(); // Clear existing items

            cq = con.getCon();
            cq.Open();

            try
            {
                string query = @"
                    SELECT M.mto_ID, M.transaction_ID, C.client_Name, MO.mto_Title, 
                           M.mto_Fee, M.mto_Description, M.mto_Finished
                    FROM MTODetails M
                    JOIN MTO MO ON M.mto_ID = MO.mto_ID
                    JOIN ClientTransactions T ON M.transaction_ID = T.transaction_ID
                    JOIN Client C ON T.client_ID = C.client_ID
                    GROUP BY M.mto_ID, M.transaction_ID, C.client_Name, MO.mto_Title, 
                             M.mto_Fee, M.mto_Description, M.mto_Finished
                    ORDER BY M.mto_Finished ASC, M.mto_ID DESC;";


                using (cmd = new SqlCommand(query, cq))
                using (rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        int mtoID = Convert.ToInt32(rd["mto_ID"]);
                        int transactionID = Convert.ToInt32(rd["transaction_ID"]);
                        string clientName = rd["client_Name"].ToString();
                        string mtoTitle = rd["mto_Title"].ToString();
                        decimal mtoFee = Convert.ToDecimal(rd["mto_Fee"]);
                        string mtoDescription = rd["mto_Description"].ToString();
                        bool isFinished = Convert.ToInt32(rd["mto_Finished"]) == 1;

                        // Create the panel
                        Panel mtoPanel = new Panel
                        {
                            Width = 200,
                            AutoSize = true,
                            MinimumSize = new Size(0, 170),
                            AutoSizeMode = AutoSizeMode.GrowAndShrink,
                            BorderStyle = BorderStyle.FixedSingle,
                            Padding = new Padding(5),
                            BackColor = Color.White,
                        };

                        // Flow panel for layout
                        FlowLayoutPanel flowPanel = new FlowLayoutPanel
                        {
                            FlowDirection = FlowDirection.TopDown,
                            AutoSize = true,
                            AutoSizeMode = AutoSizeMode.GrowAndShrink,
                            WrapContents = false,
                            Width = 180,
                            MinimumSize = new Size(180, 0),
                            Padding = new Padding(0),
                        };

                        // Labels
                        Label mtoIDLabel = new Label
                        {
                            Text = $"Made-to-Order ID: {mtoID}",
                            AutoSize = true,
                            Font = new Font("Segoe UI", 9, FontStyle.Regular),
                            ForeColor = Color.Black,
                        };

                        Label transactionIDLabel = new Label
                        {
                            Text = $"Transaction ID: {transactionID}",
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Regular),
                            ForeColor = Color.Black,
                        };

                        Label clientNameLabel = new Label
                        {
                            Text = $"{clientName}",
                            AutoSize = true,
                            Font = new Font("Segoe UI", 12, FontStyle.Bold),
                            ForeColor = Color.Black,
                        };

                        Label mtoTitleLabel = new Label
                        {
                            Text = mtoTitle,
                            AutoSize = true,
                            Font = new Font("Segoe UI", 9, FontStyle.Bold),
                            ForeColor = Color.DarkBlue,
                        };

                        Label mtoFeeLabel = new Label
                        {
                            Text = $"Fee: ₱{mtoFee:N2}",
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Regular),
                            ForeColor = Color.Green,
                        };

                        Label mtoDescLabel = new Label
                        {
                            Text = mtoDescription,
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Regular),
                            ForeColor = Color.Black,
                            MaximumSize = new Size(180, 40),
                        };

                        Label statusLabel = new Label
                        {
                            Text = isFinished ? "✔ Completed" : "⏳ In Progress",
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Bold),
                            ForeColor = isFinished ? Color.Green : Color.Red,
                        };

                        // Mark as Finished Button
                        Button markFinishedButton = new Button
                        {
                            Text = "Mark as Finished",
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Bold),
                            BackColor = Color.LightGreen,
                            ForeColor = Color.Black,
                            Cursor = Cursors.Hand
                        };
                        markFinishedButton.Click += (sender, e) => MarkAsFinished(mtoID);

                        // Add controls to flow panel
                        flowPanel.Controls.Add(mtoIDLabel);
                        flowPanel.Controls.Add(transactionIDLabel);
                        flowPanel.Controls.Add(clientNameLabel);
                        flowPanel.Controls.Add(mtoTitleLabel);
                        flowPanel.Controls.Add(mtoFeeLabel);
                        flowPanel.Controls.Add(mtoDescLabel);
                        flowPanel.Controls.Add(statusLabel);

                        if (!isFinished)
                        {
                            flowPanel.Controls.Add(markFinishedButton);

                        }

                        // Add flow panel to main panel
                        mtoPanel.Controls.Add(flowPanel);
                        mtoFlowPanel.Controls.Add(mtoPanel);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading MTO orders: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cq.Close();
            }
        }

        private void MarkAsFinished(int mtoID)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to mark this as finished?",
                                      "Confirm",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection cq = con.getCon())
                {
                    cq.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE MTODetails SET mto_Finished = 1 WHERE mto_ID = @mtoID", cq))
                    {
                        cmd.Parameters.AddWithValue("@mtoID", mtoID);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("MTO Details Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadMTOOrders();
            }
        }

    }////
}
