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

namespace DavaoCosplayShopCMS
{
    public partial class rentsForm : Form
    {
        connection con = new connection();
        SqlCommand cmd = new SqlCommand();
        SqlConnection cq;
        SqlDataReader rd;
        public rentsForm()
        {
            InitializeComponent();
            loadRentsPanel();
        }

        private void rentsForm_Load(object sender, EventArgs e)
        {
            loadRentsPanel();
        }

        private void refreshRentsButton_Click(object sender, EventArgs e)
        {
            loadRentsPanel();
        }

        private void loadRentsPanel()
        {
            rentsListPanel.Controls.Clear();

            using (SqlConnection cq = con.getCon())
            {
                cq.Open();
                string query = "SELECT * FROM RentDetailsView ORDER BY costume_Returned ASC"; // Only show active rentals

                using (SqlCommand cmd = new SqlCommand(query, cq))
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        int transactionID = Convert.ToInt32(rd["transaction_ID"]);
                        int clientID = Convert.ToInt32(rd["client_ID"]);
                        string clientName = rd["client_Name"].ToString();
                        int costumeID = Convert.ToInt32(rd["costume_ID"]);
                        string costumeName = rd["costume_Name"].ToString();
                        decimal totalFee = Convert.ToDecimal(rd["total_Fee"]); // Now includes borrow charge
                        DateTime rentDate = Convert.ToDateTime(rd["rentDate"]);
                        DateTime returnDate = Convert.ToDateTime(rd["returnDate"]);
                        bool hasValidID = Convert.ToInt32(rd["valid_ID"]) == 1;
                        int isReturned = Convert.ToInt32(rd["costume_returned"]);

                        byte[] imageData = rd["costume_IMG"] as byte[];
                        Image costumeImage = null;

                        if (imageData != null && imageData.Length > 0)
                        {
                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                costumeImage = Image.FromStream(ms);
                            }
                        }

                        Panel rentPanel = new Panel
                        {
                            Width = 290,
                            MaximumSize = new Size(290, 125),
                            AutoSize = true,
                            AutoSizeMode = AutoSizeMode.GrowAndShrink,
                            BorderStyle = BorderStyle.FixedSingle,
                            Padding = new Padding(5),
                            BackColor = isReturned == 1 ? Color.White : Color.GhostWhite,
                        };

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

                        Label transactionIDLabel = new Label { Text = $"Transaction ID: {transactionID}", AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Regular), ForeColor = Color.Black };
                        Label clientLabel = new Label { Text = $"Client: {clientName}", AutoSize = true, Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.Black };
                        Label costumeLabel = new Label { Text = $"Costume: {costumeName}", AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Regular), ForeColor = Color.DarkBlue };
                        Label feeLabel = new Label { Text = $"Total Fee: ₱{totalFee:N2}", AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Regular), ForeColor = Color.Green };
                        Label rentDateLabel = new Label { Text = $"Rented: {rentDate:yyyy-MM-dd}", AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Regular), ForeColor = Color.Black };

                        Color returnDateColor = isReturned == 1 ? Color.DarkGreen : Color.Red;
                        Label returnDateLabel = new Label { Text = $"Return by: {returnDate:yyyy-MM-dd}", AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Bold), ForeColor = returnDateColor };
                        Label returnedLabel = new Label { Text = "Costume is Returned!", AutoSize = true, Font = new Font("Segoe UI", 8, FontStyle.Italic), ForeColor = Color.Gray };


                        Label validIDLabel = new Label
                        {
                            Text = hasValidID ? "✔ Valid ID Provided" : "❌ No Valid ID",
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Bold),
                            ForeColor = hasValidID ? Color.Green : Color.Red
                        };

                        Button returnButton = new Button
                        {
                            Text = "Mark as Returned",
                            AutoSize = true,
                            Font = new Font("Segoe UI", 8, FontStyle.Bold),
                            BackColor = Color.LightBlue,
                            ForeColor = Color.Black,
                            Cursor = Cursors.Hand
                        };

                        returnButton.Click += (sender, e) => MarkAsReturned(transactionID, costumeID, rentPanel);

                        PictureBox costumePictureBox = new PictureBox
                        {
                            Size = new Size(100, 100),
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

                        flowPanel.Controls.Add(transactionIDLabel);
                        flowPanel.Controls.Add(clientLabel);
                        flowPanel.Controls.Add(costumeLabel);
                        flowPanel.Controls.Add(feeLabel);
                        flowPanel.Controls.Add(rentDateLabel);
                        flowPanel.Controls.Add(returnDateLabel);
                        flowPanel.Controls.Add(validIDLabel);

                        if (isReturned != 1)
                        {
                            flowPanel.Controls.Add(returnButton);
                        } else
                        {
                            flowPanel.Controls.Add(returnedLabel);
                        }

                        rentPanel.Controls.Add(layoutPanel);
                        rentsListPanel.Controls.Add(rentPanel);
                    }
                }
            }
        }

        private void MarkAsReturned(int transactionID, int costumeID, Panel rentPanel)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to mark this rental as returned?",
                                                  "Confirm Return", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection cq = con.getCon())
                {
                    cq.Open();
                    using (SqlTransaction transaction = cq.BeginTransaction())
                    {
                        try
                        {
                            // ✅ Update Rents
                            string updateRentQuery = "UPDATE Rents SET costume_returned = 1 WHERE transaction_ID = @transactionID";
                            using (SqlCommand cmd = new SqlCommand(updateRentQuery, cq, transaction))
                            {
                                cmd.Parameters.AddWithValue("@transactionID", transactionID);
                                cmd.ExecuteNonQuery();
                            }

                            // ✅ Update Costume Availability
                            string updateCostumeQuery = "UPDATE Costume SET costume_Available = 1 WHERE costume_ID = @costumeID";
                            using (SqlCommand cmd = new SqlCommand(updateCostumeQuery, cq, transaction))
                            {
                                cmd.Parameters.AddWithValue("@costumeID", costumeID);
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Rental marked as returned!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // ✅ Remove from UI
                            loadRentsPanel();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Error marking as returned: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

    } ////
}
