using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DavaoCosplayShopCMS
{
    public partial class clientPaymentForm : Form
    {
        private int clientID;
        private int transactionID;
        private connection con = new connection();

        public clientPaymentForm(int clientID, int transactionID)
        {
            InitializeComponent();
            this.clientID = clientID;
            this.transactionID = transactionID;
            LoadClientPaymentDetails();
        }

        private void LoadClientPaymentDetails()
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
                        clientNameTextBox.Text = result.ToString(); // Set client name in text box
                    }
                }
            }
        }

        private decimal GetRemainingBalance()
        {
            using (SqlConnection cq = con.getCon())
            {
                cq.Open();
                string query = "SELECT Remaining FROM TransactionTotals WHERE transaction_ID = @transactionID";
                using (SqlCommand cmd = new SqlCommand(query, cq))
                {
                    cmd.Parameters.AddWithValue("@transactionID", transactionID);
                    object result = cmd.ExecuteScalar();
                    if (result != null && decimal.TryParse(result.ToString(), out decimal remainingBalance))
                    {
                        return remainingBalance;
                    }
                }
            }
            return 0; // Default to 0 if no valid data is found
        }

        private void submitPaymentButton_Click(object sender, EventArgs e)
        {

            if (decimal.TryParse(paymentTextBox.Text, out decimal paymentAmount) && paymentAmount > 0)
            {
                DialogResult result = MessageBox.Show("Confirm Payment??",
                                      "Confirm Return", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    decimal remainingBalance = GetRemainingBalance();

                    if (paymentAmount > remainingBalance)
                    {
                        DialogResult confirmOverpay = MessageBox.Show(
                            $"The payment amount (₱{paymentAmount:N2}) is greater than the remaining balance (₱{remainingBalance:N2}).\n\n" +
                            "Are you sure you want to proceed?",
                            "Confirm Overpayment",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (confirmOverpay == DialogResult.No)
                        {
                            return; // Cancel the payment
                        }
                    }

                    using (SqlConnection cq = con.getCon())
                    {
                        cq.Open();
                        string insertQuery = @"
                            INSERT INTO Payment (transaction_ID, payment_Date, payment_Amount, payment_Remarks) 
                            VALUES (@transactionID, @paymentDate, @amount, @remarks)";

                        using (SqlCommand cmd = new SqlCommand(insertQuery, cq))
                        {
                            cmd.Parameters.AddWithValue("@transactionID", transactionID);
                            cmd.Parameters.AddWithValue("@paymentDate", DateTime.Now); // Store the current date
                            cmd.Parameters.AddWithValue("@amount", paymentAmount);
                            cmd.Parameters.AddWithValue("@remarks", remarksTextBox.Text.Trim()); // Assuming there is a textbox for remarks

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Payment successfully recorded.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close(); // Close the form after submitting
                            }
                            else
                            {
                                MessageBox.Show("Failed to record payment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid payment amount.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
