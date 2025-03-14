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
    public partial class editEventForm : Form
    {
        connection con = new connection();
        SqlCommand cmd = new SqlCommand();
        SqlConnection cq;
        SqlDataReader rd;
        public editEventForm(int id)
        {
            InitializeComponent();
            eventIDTextBox.Text = id.ToString();

            using (SqlConnection cq = con.getCon())
            {
                cq.Open();
                string query = "SELECT event_Name, event_Date, event_Location, event_Active FROM Event WHERE event_ID = @id";

                using (SqlCommand cmd = new SqlCommand(query, cq))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read()) 
                        {
                            eventNameTextBox.Text = rd["event_Name"].ToString();

                            if (!(rd["event_Date"] is DBNull)) 
                            {
                                eventDatePicker.Value = Convert.ToDateTime(rd["event_Date"]);
                            }

                            eventLocationTextBox.Text = rd["event_Location"].ToString();

                            int eventActive = Convert.ToInt32(rd["event_Active"]);
                            activeButton.Checked = (eventActive == 1);
                            inactiveButton.Checked = (eventActive == 0);
                        }
                    }
                }
            }
        }
        private void doneEditButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(eventNameTextBox.Text) &&
                !string.IsNullOrWhiteSpace(eventLocationTextBox.Text))
            {
                try
                {
                    using (SqlConnection cq = con.getCon()) // Use 'using' to ensure connection disposal
                    {
                        cq.Open();
                        int eventID = Convert.ToInt32(eventIDTextBox.Text);

                        string query = "UPDATE Event SET event_Name = @name, event_Location = @location, event_Date = @date, event_Active = @active WHERE event_ID = @id";

                        using (SqlCommand cmd = new SqlCommand(query, cq))
                        {
                            cmd.Parameters.AddWithValue("@name", eventNameTextBox.Text);
                            cmd.Parameters.AddWithValue("@location", eventLocationTextBox.Text);
                            cmd.Parameters.AddWithValue("@date", eventDatePicker.Value.ToString("yyyy-MM-dd")); // Format date correctly
                            cmd.Parameters.AddWithValue("@active", activeButton.Checked ? 1 : 0); // Set active/inactive
                            cmd.Parameters.AddWithValue("@id", eventID);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Event Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Error: All fields must be filled!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void cancelEditButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void activeButton_CheckedChanged(object sender, EventArgs e)
        {
            if (activeButton.Checked)
            {
                inactiveButton.Checked = false;
            }
        }

        private void inactiveButton_CheckedChanged(object sender, EventArgs e)
        {
            if (inactiveButton.Checked)
            {
                activeButton.Checked = false;
            }
        }
    }
}
