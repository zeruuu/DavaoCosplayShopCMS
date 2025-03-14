using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DavaoCosplayShopCMS
{
    public partial class addClientForm : Form
    {
        connection con = new connection();
        SqlCommand cmd = new SqlCommand();
        SqlConnection cq;
        SqlDataReader rd;
        public addClientForm()
        {
            InitializeComponent();
            DateTime currentDate = DateTime.Today;

            rentDatePicker.Format = DateTimePickerFormat.Custom;
            returnDatePicker.Format = DateTimePickerFormat.Custom;

            rentDatePicker.CustomFormat = "MMMM dd, yyyy";
            returnDatePicker.CustomFormat = "MMMM dd, yyyy";

            rentDatePicker.Value = currentDate;
            returnDatePicker.Value = currentDate.AddDays(3); // expected return after 3 days (can be changed)

            // validate decimal inputs on feeTextBoxes
            makeupFeeTextBox.TextChanged += ValidateDecimalInput;
            mascotFeeTextBox.TextChanged += ValidateDecimalInput;
            wigFixingFeeTextBox.TextChanged += ValidateDecimalInput;
            wigStylingFeeTextBox.TextChanged += ValidateDecimalInput;
            preorderFeeTextBox.TextChanged += ValidateDecimalInput;
            othersFeeTextBox.TextChanged += ValidateDecimalInput;
        }

        private void rentDatePicker_ValueChanged(object sender, EventArgs e)
        {
            returnDatePicker.Value = rentDatePicker.Value.AddDays(3); // expected return after 3 days (can be changed)
        } 
        private void addClientRenterForm_SizeChanged(object sender, EventArgs e) // enable balance and remarks input when form size changes 
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
        private void registerClientButton_Click(object sender, EventArgs e)
        {
            try
            {
                // 🔹 Step 1: Validate Client Details (Always Required)
                if (string.IsNullOrWhiteSpace(nameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(ageTextBox.Text))
                {
                    MessageBox.Show("Please fill in all required fields (Name and Age).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(ageTextBox.Text, out int age))
                {
                    MessageBox.Show("Please enter a valid number for age!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

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

                // 🔹 Step 3: Validate MTO if Checked
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

                // 🔹 Step 4: Validate Services if Checked
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

                using (SqlConnection cq = con.getCon())
                {
                    cq.Open();

                    // 🔹 Step 5: Insert Client
                    int clientID;
                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO Client(client_Name, client_Address, client_Age, client_Cellphone, client_Facebook, client_Occupation) " +
                        "VALUES (@name, @address, @age, @cellphone, @facebook, @occupation); " +
                        "SELECT SCOPE_IDENTITY();", cq))
                    {
                        cmd.Parameters.AddWithValue("@name", nameTextBox.Text);
                        cmd.Parameters.AddWithValue("@address", addressTextBox.Text);
                        cmd.Parameters.AddWithValue("@age", age);
                        cmd.Parameters.AddWithValue("@cellphone", phoneTextBox.Text);
                        cmd.Parameters.AddWithValue("@facebook", facebookTextBox.Text);
                        cmd.Parameters.AddWithValue("@occupation", occupationTextBox.Text);

                        clientID = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // 🔹 Step 6: Insert Transactions if Rent, MTO, or Services are checked
                    if (rentCheckBox.Checked || mtoCheckBox.Checked || servicesCheckBox.Checked)
                    {
                        int transactionID;
                        using (SqlCommand cmd = new SqlCommand(
                            "INSERT INTO Transactions(client_ID, transaction_Date) " +
                            "VALUES (@clientID, @transactionDate); " +
                            "SELECT SCOPE_IDENTITY();", cq))
                        {
                            cmd.Parameters.AddWithValue("@clientID", clientID);
                            cmd.Parameters.AddWithValue("@transactionDate", DateTime.Now);

                            transactionID = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // 🔹 Step 7: Insert Rental Details if checked
                        if (rentCheckBox.Checked)
                        {
                            using (SqlCommand cmd = new SqlCommand(
                                "INSERT INTO Rents(transaction_ID, costume_ID, costume_Fee, rentDate, returnDate, valid_ID, event_ID) " +
                                "VALUES (@transactionID, @costumeID, @costumeFee, @rentDate, @returnDate, @validID, @eventID)", cq))
                            {
                                cmd.Parameters.AddWithValue("@transactionID", transactionID);
                                cmd.Parameters.AddWithValue("@costumeID", int.Parse(costumeIDTextBox.Text));
                                cmd.Parameters.AddWithValue("@costumeFee", decimal.Parse(costumePriceTextBox.Text));
                                cmd.Parameters.AddWithValue("@rentDate", rentDatePicker.Value.ToString("yyyy-MM-dd"));
                                cmd.Parameters.AddWithValue("@returnDate", returnDatePicker.Value.ToString("yyyy-MM-dd"));
                                cmd.Parameters.AddWithValue("@validID", validIDCheckBox.Checked ? 1 : 0);
                                cmd.Parameters.AddWithValue("@eventID", int.Parse(eventIDTextBox.Text));

                                cmd.ExecuteNonQuery();
                            }
                        }

                        // 🔹 Step 8: Insert MTO Details if checked
                        if (mtoCheckBox.Checked)
                        {
                            int mtoID;
                            using (SqlCommand cmd = new SqlCommand(
                                "INSERT INTO MTO(mto_Title) VALUES (@title); " +
                                "SELECT SCOPE_IDENTITY();", cq))
                            {
                                cmd.Parameters.AddWithValue("@title", mtoTitleTextBox.Text);
                                mtoID = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            using (SqlCommand cmd = new SqlCommand(
                                "INSERT INTO MTODetails(mto_ID, transaction_ID, mto_Fee, mto_Description) " +
                                "VALUES (@mtoID, @transactionID, @mtoFee, @mtoDescription);", cq))
                            {
                                cmd.Parameters.AddWithValue("@mtoID", mtoID);
                                cmd.Parameters.AddWithValue("@transactionID", transactionID);
                                cmd.Parameters.AddWithValue("@mtoFee", decimal.Parse(mtoFeeTextBox.Text));
                                cmd.Parameters.AddWithValue("@mtoDescription", mtoDescriptionTextBox.Text);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageBox.Show("Client Registered!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
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
                    eventDate.Value = DateTime.Parse(eventForm.SelectedDate);
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
        } // check if decimal

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
                loadCostumeDetails(costumeID);
            }
            else
            {
                MessageBox.Show("Invalid Costume ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadCostumeDetails(int costumeID)
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
                            costumeTypeTextBox.Text = rd["costumeType_Name"].ToString();
                            costumeSizeTextBox.Text = rd["costumeSize_Name"].ToString();
                            costumeGenderTextBox.Text = rd["costumeGender_Name"].ToString();
                            costumeInclusionsTextBox.Text = rd["costume_Inclusions"].ToString();
                            costumePriceTextBox.Text = Convert.ToDecimal(rd["costume_Price"]).ToString("N2");

                            // load image if available
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
                                costumePictureBox.Image = Properties.Resources.davaoCosplayShopIcon; // default image
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

        // Clears the text fields and image when no valid costume is found
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

    }
}

