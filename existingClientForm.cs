using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DavaoCosplayShopCMS
{
    public partial class existingClientForm : Form
    {
        connection con = new connection();
        SqlCommand cmd;
        SqlConnection cq;
        SqlDataReader rd;
        private int clientID;

        public existingClientForm(int clientID)
        {
            InitializeComponent();
            this.clientID = clientID;

            DateTime currentDate = DateTime.Today;
            rentDatePicker.Format = DateTimePickerFormat.Custom;
            returnDatePicker.Format = DateTimePickerFormat.Custom;
            rentDatePicker.CustomFormat = "MMMM dd, yyyy";
            returnDatePicker.CustomFormat = "MMMM dd, yyyy";
            rentDatePicker.Value = currentDate;
            returnDatePicker.Value = currentDate.AddDays(3); // Expected return after 3 days

            LoadClientDetails();

            makeupFeeTextBox.TextChanged += ValidateDecimalInput;
            mascotFeeTextBox.TextChanged += ValidateDecimalInput;
            wigFixingFeeTextBox.TextChanged += ValidateDecimalInput;
            wigStylingFeeTextBox.TextChanged += ValidateDecimalInput;
            preorderFeeTextBox.TextChanged += ValidateDecimalInput;
            othersFeeTextBox.TextChanged += ValidateDecimalInput;
        }
        private void rentDatePicker_ValueChanged(object sender, EventArgs e)
        {
            returnDatePicker.Value = rentDatePicker.Value.AddDays(3); // expected return after 3 days
        } // change return date by 3 days

        private void LoadClientDetails()
        {
            using (SqlConnection cq = con.getCon())
            {
                cq.Open();
                string query = "SELECT * FROM Client WHERE client_ID = @clientID";

                using (SqlCommand cmd = new SqlCommand(query, cq))
                {
                    cmd.Parameters.AddWithValue("@clientID", clientID);
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            nameTextBox.Text = rd["client_Name"].ToString();
                            addressTextBox.Text = rd["client_Address"].ToString();
                            ageTextBox.Text = rd["client_Age"].ToString();
                            phoneTextBox.Text = rd["client_Cellphone"].ToString();
                            facebookTextBox.Text = rd["client_Facebook"].ToString();
                            occupationTextBox.Text = rd["client_Occupation"].ToString();
                        }
                        else
                        {
                            MessageBox.Show("Client not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Close();
                        }
                    }
                }
            }
        }

        private void registerTransactionButton_Click(object sender, EventArgs e) // register transaction 
        {
            if (!rentCheckBox.Checked && !mtoCheckBox.Checked && !servicesCheckBox.Checked)
            {
                MessageBox.Show("Client should have at least one service selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 

            try
            {
                using (SqlConnection cq = con.getCon())
                {
                    cq.Open();

                    // Insert Transaction for the existing client
                    int transactionID;
                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Transactions(client_ID, transaction_Date) " +
                        "VALUES (@clientID, @transactionDate); " +
                        "SELECT SCOPE_IDENTITY();", cq))
                    {
                        cmd.Parameters.AddWithValue("@clientID", clientID);
                        cmd.Parameters.AddWithValue("@transactionDate", DateTime.Today.ToString("yyyy-MM-dd"));
                        transactionID = Convert.ToInt32(cmd.ExecuteScalar()); // ✅ Get Transaction ID
                    }

                    // ✅ Rent Section
                    if (rentCheckBox.Checked)
                    {
                        using (SqlTransaction transaction = cq.BeginTransaction())
                        {
                            try
                            {
                                if (rentCheckBox.Checked)
                                {
                                    if (string.IsNullOrWhiteSpace(costumeIDTextBox.Text) ||
                                        string.IsNullOrWhiteSpace(eventIDTextBox.Text) ||
                                        string.IsNullOrWhiteSpace(costumePriceTextBox.Text))
                                    {
                                        MessageBox.Show("Please enter Costume and Event rental.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }

                                    if (!decimal.TryParse(costumePriceTextBox.Text, out decimal _))
                                    {
                                        MessageBox.Show("Costume Price must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }

                                if (mtoCheckBox.Checked)
                                {
                                    if (string.IsNullOrWhiteSpace(mtoTitleTextBox.Text) ||
                                        string.IsNullOrWhiteSpace(mtoFeeTextBox.Text))
                                    {
                                        MessageBox.Show("MTO Title and MTO Fee must not be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }

                                    if (!decimal.TryParse(mtoFeeTextBox.Text, out decimal _))
                                    {
                                        MessageBox.Show("MTO Fee must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }

                                if (servicesCheckBox.Checked)
                                {
                                    bool hasValidService = false;

                                    if (!string.IsNullOrWhiteSpace(makeupFeeTextBox.Text) ||
                                        !string.IsNullOrWhiteSpace(mascotFeeTextBox.Text) ||
                                        !string.IsNullOrWhiteSpace(wigFixingFeeTextBox.Text) ||
                                        !string.IsNullOrWhiteSpace(wigStylingFeeTextBox.Text) ||
                                        !string.IsNullOrWhiteSpace(preorderFeeTextBox.Text) ||
                                        !string.IsNullOrWhiteSpace(othersFeeTextBox.Text))
                                    {
                                        hasValidService = true;
                                    }

                                    if (!hasValidService)
                                    {
                                        MessageBox.Show("At least one service fee must be entered if Services is checked.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }


                                if (!int.TryParse(costumeIDTextBox.Text, out int costumeID))
                                    throw new Exception("Invalid Costume ID. Please enter a valid number.");

                                if (!decimal.TryParse(costumePriceTextBox.Text, out decimal costumeFee))
                                    throw new Exception("Invalid Costume Fee. Please enter a valid price.");

                                int? eventID = null;
                                if (!string.IsNullOrWhiteSpace(eventIDTextBox.Text) && int.TryParse(eventIDTextBox.Text, out int parsedEventID))
                                    eventID = parsedEventID;

                                using (SqlCommand cmd = new SqlCommand(
                                    "INSERT INTO Rents(transaction_ID, costume_ID, costume_Fee, rentDate, returnDate, valid_ID, event_ID) " +
                                    "VALUES (@transactionID, @costumeID, @costumeFee, @rentDate, @returnDate, @validID, @eventID)", cq, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@transactionID", transactionID);
                                    cmd.Parameters.AddWithValue("@costumeID", costumeID);
                                    cmd.Parameters.AddWithValue("@costumeFee", costumeFee);
                                    cmd.Parameters.AddWithValue("@rentDate", rentDatePicker.Value.ToString("yyyy-MM-dd"));
                                    cmd.Parameters.AddWithValue("@returnDate", returnDatePicker.Value.ToString("yyyy-MM-dd"));
                                    cmd.Parameters.AddWithValue("@validID", validIDCheckBox.Checked ? 1 : 0);
                                    cmd.Parameters.AddWithValue("@eventID", (object)eventID ?? DBNull.Value);

                                    cmd.ExecuteNonQuery();
                                }

                                using (SqlCommand cmd = new SqlCommand(
                                    "UPDATE Costume SET costume_Available = 0 WHERE costume_ID = @costumeID", cq, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@costumeID", costumeID);
                                    cmd.ExecuteNonQuery();
                                }

                                if (!string.IsNullOrWhiteSpace(additionalsTextBox.Text) && !string.IsNullOrWhiteSpace(additionalDescriptionTextBox.Text))
                                {
                                    if (!decimal.TryParse(additionalsTextBox.Text, out decimal chargeFee))
                                        throw new Exception("Invalid Additional Charge. Please enter a valid amount.");

                                    using (SqlCommand cmd = new SqlCommand(
                                        "INSERT INTO ChargeDetails(transaction_ID, charge_ID, charge_Fee, charge_Description) " +
                                        "VALUES (@transactionID, 4, @chargeFee, @chargeDescription)", cq, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@transactionID", transactionID);
                                        cmd.Parameters.AddWithValue("@chargeFee", chargeFee);
                                        cmd.Parameters.AddWithValue("@chargeDescription", additionalDescriptionTextBox.Text);

                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                MessageBox.Show($"Error processing rental: {ex.Message}", "Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    // ✅ Made-to-Order (MTO) Section
                    if (mtoCheckBox.Checked)
                    {
                        int mtoID;
                        using (SqlCommand cmd = new SqlCommand(
                            "INSERT INTO MTO(mto_Title) VALUES (@title); SELECT SCOPE_IDENTITY();", cq))
                        {
                            cmd.Parameters.AddWithValue("@title", mtoTitleTextBox.Text);
                            mtoID = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        using (SqlCommand cmd = new SqlCommand(
                            "INSERT INTO MTODetails(mto_ID, transaction_ID, mto_Fee, mto_Description) " +
                            "VALUES (@mtoID, @transactionID, @mtoFee, @mtoDescription);", cq))
                        {
                            cmd.Parameters.AddWithValue("@mtoID", mtoID);
                            cmd.Parameters.AddWithValue("@transactionID", transactionID); // ✅ Corrected
                            cmd.Parameters.AddWithValue("@mtoFee", mtoFeeTextBox.Text);
                            cmd.Parameters.AddWithValue("@mtoDescription", mtoDescriptionTextBox.Text);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    // ✅ Services Section
                    if (servicesCheckBox.Checked)
                    {
                        void InsertService(int serviceTypeID, TextBox amountTextBox)
                        {
                            if (!string.IsNullOrWhiteSpace(amountTextBox.Text)) // Ensure it's not empty
                            {
                                if (decimal.TryParse(amountTextBox.Text, out decimal serviceAmount))
                                {
                                    using (SqlCommand cmd = new SqlCommand(
                                        "INSERT INTO ServiceAvail(transaction_ID, serviceType_ID, service_Amount) " +
                                        "VALUES (@transactionID, @serviceTypeID, @amount);", cq))
                                    {
                                        cmd.Parameters.AddWithValue("@transactionID", transactionID);
                                        cmd.Parameters.AddWithValue("@serviceTypeID", serviceTypeID);
                                        cmd.Parameters.AddWithValue("@amount", serviceAmount); // ✅ Converted to decimal

                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    throw new Exception($"Invalid amount entered for Service Type {serviceTypeID}. Please enter a valid number.");
                                }
                            }
                        }

                        if (makeupCheckBox.Checked) InsertService(1, makeupFeeTextBox);
                        if (mascotCheckBox.Checked) InsertService(2, mascotFeeTextBox);
                        if (wigFixingCheckBox.Checked) InsertService(3, wigFixingFeeTextBox);
                        if (wigStylingCheckBox.Checked) InsertService(4, wigStylingFeeTextBox);
                        if (preorderCheckBox.Checked) InsertService(5, preorderFeeTextBox);
                        if (othersCheckBox.Checked) InsertService(6, othersFeeTextBox);
                    }

                    // ✅ Insert Payment if balance is provided
                    if (!string.IsNullOrWhiteSpace(balanceTextBox.Text))
                    {
                        if (decimal.TryParse(balanceTextBox.Text, out decimal paymentAmount))
                        {
                            using (SqlCommand cmd = new SqlCommand(
                                "INSERT INTO Payment(transaction_ID, payment_Date, payment_Amount, payment_Remarks) " +
                                "VALUES (@transactionID, GETDATE(), @amount, @remarks)", cq))
                            {
                                cmd.Parameters.AddWithValue("@transactionID", transactionID);
                                cmd.Parameters.AddWithValue("@amount", paymentAmount);
                                cmd.Parameters.AddWithValue("@remarks", remarksTextBox.Text);

                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            throw new Exception("Invalid payment amount. Please enter a valid number.");
                        }
                    }

                    MessageBox.Show("Transaction for Existing Client Registered!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // checkboxes

        private void rentCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (rentCheckBox.Checked)
            {
                rentPanel.Enabled = true;
                rentPanel.Visible = true;
                additionalsTextBox.Enabled = true;
            }
            else
            {
                rentPanel.Enabled = false;
                rentPanel.Visible = false;
                additionalsTextBox.Enabled = false;
            }
        }

        private void servicesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (servicesCheckBox.Checked)
            {
                addServicesPanel.Enabled = true;
                addServicesPanel.Visible = true;
            }
            else
            {
                addServicesPanel.Enabled = false;
                addServicesPanel.Visible = false;
            }
        }

        private void mtoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (mtoCheckBox.Checked)
            {
                mtoPanel.Enabled = true;
                mtoPanel.Visible = true;
            }
            else
            {
                mtoPanel.Enabled = false;
                mtoPanel.Visible = false;
            }
        }

        // browse event // rent details
        private void browseEventButton_Click(object sender, EventArgs e)
        {
            using (selectEventForm eventForm = new selectEventForm())
            {
                if (eventForm.ShowDialog() == DialogResult.OK)
                {
                    eventTextBox.Text = eventForm.SelectedEvent;
                    eventIDTextBox.Text = eventForm.SelectedEventID;
                }
            }
        }

        // checkboxes
        private void makeupCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ToggleTextBox(makeupCheckBox, makeupFeeTextBox);
        }

        private void mascotCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ToggleTextBox(mascotCheckBox, mascotFeeTextBox);
        }

        private void wigFixingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ToggleTextBox(wigFixingCheckBox, wigFixingFeeTextBox);
        }

        private void wigStylingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ToggleTextBox(wigStylingCheckBox, wigStylingFeeTextBox);
        }

        private void preorderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ToggleTextBox(preorderCheckBox, preorderFeeTextBox);
        }

        private void othersCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ToggleTextBox(othersCheckBox, othersFeeTextBox);
        }

        private void ToggleTextBox(CheckBox checkBox, TextBox textBox)
        {
            textBox.Enabled = checkBox.Checked;
            if (!checkBox.Checked)
                textBox.Text = "";
        }

        private void ValidateDecimalInput(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null) return;

            string text = textBox.Text;

            if (!Regex.IsMatch(text, @"^\d*\.?\d*$"))
            {
                MessageBox.Show("Only numeric values and a single decimal point are allowed.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox.Text = Regex.Replace(text, @"[^0-9.]", "");
            }

            int dotCount = textBox.Text.Count(c => c == '.');
            if (dotCount > 1)
            {
                MessageBox.Show("Only one decimal point is allowed.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox.Text = textBox.Text.Remove(textBox.Text.LastIndexOf('.'), 1);
            }

            textBox.SelectionStart = textBox.Text.Length;
        }

        // services fees
        private void makeupFeeTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateTotalFee();
        }

        private void mascotFeeTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateTotalFee();
        }

        private void wigFixingFeeTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateTotalFee();
        }

        private void wigStylingFeeTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateTotalFee();
        }

        private void preorderFeeTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateTotalFee();
        }

        private void othersFeeTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateTotalFee();
        }

        private void UpdateTotalFee()
        {
            decimal total = 0;

            total += GetDecimalValue(makeupFeeTextBox);
            total += GetDecimalValue(mascotFeeTextBox);
            total += GetDecimalValue(wigFixingFeeTextBox);
            total += GetDecimalValue(wigStylingFeeTextBox);
            total += GetDecimalValue(preorderFeeTextBox);
            total += GetDecimalValue(othersFeeTextBox);

            totalFeeTextBox.Text = total.ToString("0.00"); // Ensures 2 decimal places
        }

        private decimal GetDecimalValue(TextBox textBox)
        {
            if (decimal.TryParse(textBox.Text, out decimal value))
            {
                return value;
            }
            return 0;
        }

        private void browseCostumeButton_Click_1(object sender, EventArgs e)
        {
            using (selectCostumeForm costumeForm = new selectCostumeForm())
            {
                if (costumeForm.ShowDialog() == DialogResult.OK)
                {
                    costumeIDTextBox.Text = costumeForm.SelectedCostumeID;
                }
            }
        }

        private void costumeIDTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(costumeIDTextBox.Text))
            {
                ClearFields();
                return;
            }

            if (int.TryParse(costumeIDTextBox.Text, out int costumeID))
            {
                LoadCostumeDetails(costumeID);
            }
            else
            {
                MessageBox.Show("Invalid Costume ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCostumeDetails(int costumeID)
        {
            using (SqlConnection cq = con.getCon())
            {
                cq.Open();
                string query = @"
            SELECT 
                C.costume_Name, 
                T.costumeType_Name, 
                S.costumeSize_Name, 
                G.costumeGender_Name, 
                C.costume_Inclusions, 
                C.costume_Price, 
                C.costume_IMG
            FROM Costume C
            JOIN CostumeType T ON C.costumeType_ID = T.costumeType_ID
            JOIN CostumeSize S ON C.costumeSize_ID = S.costumeSize_ID
            JOIN CostumeGender G ON C.costumeGender_ID = G.costumeGender_ID
            WHERE C.costume_ID = @costumeID";

                using (SqlCommand cmd = new SqlCommand(query, cq))
                {
                    cmd.Parameters.AddWithValue("@costumeID", costumeID);
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            costumeNameTextBox.Text = rd["costume_Name"].ToString();
                            costumeTypeTextBox.Text = rd["costumeType_Name"].ToString(); // From CostumeType table
                            costumeSizeTextBox.Text = rd["costumeSize_Name"].ToString(); // From CostumeSize table
                            costumeGenderTextBox.Text = rd["costumeGender_Name"].ToString(); // From CostumeGender table
                            costumeInclusionsTextBox.Text = rd["costume_Inclusions"].ToString();
                            costumePriceTextBox.Text = Convert.ToDecimal(rd["costume_Price"]).ToString("N2");

                            // Load image if available
                            if (rd["costume_IMG"] != DBNull.Value)
                            {
                                byte[] imgData = (byte[])rd["costume_IMG"];
                                using (MemoryStream ms = new MemoryStream(imgData))
                                {
                                    costumePictureBox.Image = Image.FromStream(ms);
                                }
                            }
                            else
                            {
                                costumePictureBox.Image = Properties.Resources.davaoCosplayShopIcon; // Default image
                            }
                        }
                        else
                        {
                            MessageBox.Show("Costume not found!", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFields();
                        }
                    }
                }
            }
        }
        private void ClearFields()
        {
            costumeNameTextBox.Clear();
            costumeTypeTextBox.Clear();
            costumeSizeTextBox.Clear();
            costumeGenderTextBox.Clear();
            costumeInclusionsTextBox.Clear();
            costumePriceTextBox.Clear();
            costumePictureBox.Image = Properties.Resources.davaoCosplayShopIcon;
        }

        private void cancelButton_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void existingClientForm_SizeChanged(object sender, EventArgs e)
        {
            if (rentCheckBox.Checked || mtoCheckBox.Checked || servicesCheckBox.Checked)
            {
                balanceTextBox.Enabled = true;
                balanceLabel.Enabled = true;
                remarksTextBox.Enabled = true;
                remarksLabel.Enabled = true;
            }
            else
            {
                balanceTextBox.Enabled = false;
                balanceLabel.Enabled = false;
                remarksTextBox.Enabled = false;
                remarksLabel.Enabled = false;
            }
        }
    }////
}
