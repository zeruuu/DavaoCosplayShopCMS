using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DavaoCosplayShopCMS
{
    public partial class clientChargesForm : Form
    {
        private int transactionID;
        private int clientID; // Store client ID
        private connection con = new connection();
        private SqlCommand cmd = new SqlCommand();
        private SqlConnection cq;
        private SqlDataReader rd;

        public clientChargesForm(int clientID, int transactionID)
        {
            InitializeComponent();
            this.transactionID = transactionID;
            this.clientID = clientID;

            LoadClientName();  // Fetch and display client name
            LoadChargeTypes(); // Load charge types
        }

        private void LoadClientName()
        {
            try
            {
                using (SqlConnection cq = con.getCon())
                {
                    cq.Open();
                    string query = "SELECT client_Name FROM Client WHERE client_ID = @clientID";

                    using (SqlCommand cmd = new SqlCommand(query, cq))
                    {
                        cmd.Parameters.AddWithValue("@clientID", clientID);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            clientNameTextBox.Text = result.ToString(); // Display client name
                        }
                        else
                        {
                            clientNameTextBox.Text = "Unknown Client";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving client name: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChargeTypes()
        {
            chargeNameComboBox.Items.Add("Late");        // ID 1
            chargeNameComboBox.Items.Add("Damages");     // ID 2
            chargeNameComboBox.Items.Add("Lost");        // ID 3
            chargeNameComboBox.Items.Add("Additional");  // ID 4
            chargeNameComboBox.Items.Add("Others");      // ID 5
            chargeNameComboBox.SelectedIndex = 0; // Default to first charge type
        }

        private void submitChargeButton_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(chargeTextBox.Text, out decimal chargeAmount) || chargeAmount <= 0)
            {
                MessageBox.Show("Please enter a valid charge amount.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int chargeID = chargeNameComboBox.SelectedIndex + 1;
            string chargeDescription = descriptionTextBox.Text.Trim();

            DialogResult result = MessageBox.Show(
                $"Confirm adding a charge:\n\nClient: {clientNameTextBox.Text}\nType: {chargeNameComboBox.SelectedItem}\nAmount: ₱{chargeAmount:N2}\nDescription: {chargeDescription}",
                "Confirm Charge",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection cq = con.getCon())
                    {
                        cq.Open();
                        string insertQuery = @"
                            INSERT INTO ChargeDetails (transaction_ID, charge_ID, charge_Fee, charge_Description) 
                            VALUES (@transactionID, @chargeID, @chargeFee, @chargeDescription)";

                        using (SqlCommand cmd = new SqlCommand(insertQuery, cq))
                        {
                            cmd.Parameters.AddWithValue("@transactionID", transactionID);
                            cmd.Parameters.AddWithValue("@chargeID", chargeID);
                            cmd.Parameters.AddWithValue("@chargeFee", chargeAmount);
                            cmd.Parameters.AddWithValue("@chargeDescription", chargeDescription);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Charge successfully added.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Failed to add charge.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
