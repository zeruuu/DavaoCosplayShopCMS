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
    public partial class clientsForm : Form
    {
        connection con = new connection();
        SqlCommand cmd = new SqlCommand();
        SqlConnection cq;
        SqlDataReader rd;
        public clientsForm()
        {
            InitializeComponent();
            loadClientsPanel();
        }

        addClientForm clientForm;

        private void openClientForm()
        {
            if (clientForm == null)
            {
                clientForm = new addClientForm();
                clientForm.FormClosed += Clientform_FormClosed;
                clientForm.FormClosed += (s, args) => loadClientsPanel(); // refresh after close
                clientForm.Show();
            }
            else
            {
                clientForm.Activate();
            }
        }

        private void Clientform_FormClosed(object sender, FormClosedEventArgs e)
        {
            clientForm = null;
        }

        private void loadClientsPanel(string searchQuery = "")
        {
            clientsListPanel.Controls.Clear();

            cq = con.getCon();
            cq.Open();

            try
            {
                string query = "SELECT * FROM Client WHERE client_Name LIKE @searchQuery OR client_Address LIKE @searchQuery ORDER BY client_Name ASC";

                using (SqlCommand cmd = new SqlCommand(query, cq))
                {
                    cmd.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");

                    using (rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            int clientID = Convert.ToInt32(rd["client_ID"]);
                            string clientName = rd["client_Name"].ToString();
                            string clientAddress = rd["client_Address"].ToString();
                            int clientAge = Convert.ToInt32(rd["client_Age"]);
                            string clientCellphone = rd["client_Cellphone"].ToString();
                            string clientFacebook = rd["client_Facebook"].ToString();
                            string clientOccupation = rd["client_Occupation"].ToString();

                            Panel clientPanel = new Panel
                            {
                                MinimumSize = new Size(350, 0),
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
                                MinimumSize = new Size(240, 0),
                                Padding = new Padding(0),
                            };

                            Label idLabel = new Label
                            {
                                Text = $"ID: {clientID}",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                                ForeColor = Color.Black,
                            };

                            Label nameLabel = new Label
                            {
                                Text = clientName,
                                AutoSize = true,
                                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                                ForeColor = Color.Black,
                            };

                            Label addressLabel = new Label
                            {
                                Text = $"Address: {clientAddress}",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                                ForeColor = Color.DarkGray,
                            };

                            Label ageLabel = new Label
                            {
                                Text = $"Age: {clientAge}",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                                ForeColor = Color.Black,
                            };

                            Label cellphoneLabel = new Label
                            {
                                Text = $"Cellphone: {clientCellphone}",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                                ForeColor = Color.Black,
                            };

                            Label facebookLabel = new Label
                            {
                                Text = $"{clientFacebook}",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 8, FontStyle.Underline),
                                ForeColor = Color.Blue,
                                Cursor = Cursors.Hand,
                            };

                            facebookLabel.Click += (sender, e) =>
                            {
                                try
                                {
                                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                                    {
                                        FileName = clientFacebook,
                                        UseShellExecute = true
                                    });
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Failed to open link: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            };

                            Label occupationLabel = new Label
                            {
                                Text = $"Occupation: {clientOccupation}",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                                ForeColor = Color.Black,
                            };

                            Button useClientButton = new Button
                            {
                                Text = "New Transaction",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                                BackColor = Color.LightGreen,
                                ForeColor = Color.Black,
                                Cursor = Cursors.Hand,
                            };

                            useClientButton.Click += (sender, e) =>
                            {
                                existingClientForm existingForm = new existingClientForm(clientID);
                                existingForm.ShowDialog();
                            };

                            Button deleteClientButton = new Button
                            {
                                Text = "Delete Client",
                                AutoSize = true,
                                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                                BackColor = Color.LightCoral,
                                ForeColor = Color.Black,
                                Cursor = Cursors.Hand,
                            };

                            deleteClientButton.Click += (sender, e) =>
                            {
                                DeleteClient(clientID, clientName, clientPanel);
                            };

                            flowPanel.Controls.Add(idLabel);
                            flowPanel.Controls.Add(nameLabel);
                            flowPanel.Controls.Add(addressLabel);
                            flowPanel.Controls.Add(ageLabel);
                            flowPanel.Controls.Add(cellphoneLabel);
                            flowPanel.Controls.Add(facebookLabel);
                            flowPanel.Controls.Add(occupationLabel);
                            flowPanel.Controls.Add(useClientButton);
                            flowPanel.Controls.Add(deleteClientButton);

                            clientPanel.Controls.Add(flowPanel);
                            clientsListPanel.Controls.Add(clientPanel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading clients: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cq.Close();
            }
        }
        private void DeleteClient(int clientID, string clientName, Panel clientPanel)
        {
            using (SqlConnection cq = con.getCon())
            {
                cq.Open();

                // 🔹 Step 1: Check if the client has any unreturned rentals
                string checkRentQuery = @"
            SELECT R.costume_ID 
            FROM Rents R
            JOIN Transactions T ON R.transaction_ID = T.transaction_ID
            WHERE T.client_ID = @clientID AND R.returnDate > GETDATE()"; // Active rental condition

                List<int> rentedCostumeIDs = new List<int>();

                using (SqlCommand checkCmd = new SqlCommand(checkRentQuery, cq))
                {
                    checkCmd.Parameters.AddWithValue("@clientID", clientID);
                    using (SqlDataReader reader = checkCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rentedCostumeIDs.Add(Convert.ToInt32(reader["costume_ID"]));
                        }
                    }
                }

                // 🔹 Step 2: If the client has rented costumes, confirm before deleting
                if (rentedCostumeIDs.Count > 0)
                {
                    DialogResult rentResult = MessageBox.Show(
                        $"This client has {rentedCostumeIDs.Count} unreturned rental(s).\n\n" +
                        "Are you sure you want to delete this client?\n" +
                        "The costume(s) availability will be reset before deletion.",
                        "Unreturned Rentals Found", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (rentResult == DialogResult.No) return;

                    // 🔹 Step 3: Reset costume_Available to 1 for all rented costumes
                    foreach (int costumeID in rentedCostumeIDs)
                    {
                        string updateCostumeQuery = "UPDATE Costume SET costume_Available = 1 WHERE costume_ID = @costumeID";
                        using (SqlCommand updateCmd = new SqlCommand(updateCostumeQuery, cq))
                        {
                            updateCmd.Parameters.AddWithValue("@costumeID", costumeID);
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                }

                // 🔹 Step 4: Confirm final deletion
                DialogResult confirmResult = MessageBox.Show(
                    $"Are you sure you want to delete this client?\n" +
                    "All transaction, rent, charges, and service records will also be deleted.\n\n" +
                    $"Client ID: {clientID}\nClient Name: {clientName}",
                    "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (confirmResult == DialogResult.Yes)
                {
                    try
                    {
                        // 🔹 Step 5: Delete client and all related transactions
                        string deleteQuery = "DELETE FROM Client WHERE client_ID = @clientID";
                        using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, cq))
                        {
                            deleteCmd.Parameters.AddWithValue("@clientID", clientID);
                            int rowsAffected = deleteCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Client deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loadClientsPanel(); // Refresh client list
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete client. It may not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting client: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void addClientButton_Click(object sender, EventArgs e)
        {
            openClientForm();
        }

        private void searchClientTextBox_TextChanged(object sender, EventArgs e)
        {
            loadClientsPanel(searchClientTextBox.Text.Trim()); // Reload clients with search filter
        }


        //////
    }
}
