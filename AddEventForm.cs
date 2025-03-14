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
    public partial class addEventForm : Form
    {
        connection con = new connection();
        SqlCommand cmd = new SqlCommand();
        SqlConnection cq;
        SqlDataReader rd;
        public addEventForm()
        {
            InitializeComponent();
            DateTime currentDate = DateTime.Today;
            eventDatePicker.Value = currentDate;
        }

        private void addEventButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(eventNameTextBox.Text))
            {
                try
                {
                    cq = con.getCon();
                    cq.Open();

                    cmd = new SqlCommand("INSERT INTO Event (event_Name, event_Location, event_Date) VALUES (@name, @location, @date)", cq); // event_Active default value to 1

                    cmd.Parameters.AddWithValue("@name", eventNameTextBox.Text);
                    cmd.Parameters.AddWithValue("@location", eventLocationTextBox.Text);

                    string eventDate = eventDatePicker.Value.ToString("yyyy-MM-dd");

                    cmd.Parameters.AddWithValue("@date", eventDate);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Event Registered Successfully!");

                    cq.Close();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            } else
            {
                MessageBox.Show("Error: All fields must be filled!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }////
}
