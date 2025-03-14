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
    public partial class viewEventsForm : Form
    {
        connection con = new connection();
        SqlCommand cmd = new SqlCommand();
        SqlConnection cq;
        SqlDataReader rd;

        public viewEventsForm()
        {
            InitializeComponent();
            loadEvents();
            eventDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            eventDataGridView.MultiSelect = false; 

        }

        private void addEventButton_Click(object sender, EventArgs e)
        {
            addEventForm addEventForm = new addEventForm();
            addEventForm.FormClosed += (s, args) => loadEvents(); // refresh costumes after closing
            addEventForm.Show();
        }

        private void loadEvents()
        {
            eventDataGridView.Rows.Clear();
            cq = con.getCon();
            cq.Open();

            cmd = new SqlCommand("SELECT * FROM Event", cq);
            rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                string eventID = rd[0].ToString();
                string eventName = rd[1].ToString();
                string eventDate = Convert.ToDateTime(rd[2]).ToString("yyyy-MM-dd"); // Format as YYYY-MM-DD
                string eventLocation = rd[3].ToString();
                int active = Convert.ToInt32(rd[4]);
                string eventActive = "";
                if (active == 1) {
                    eventActive = "Active";
                } else
                {
                    eventActive = "Inactive";
                }

                eventDataGridView.Rows.Add(eventID, eventName, eventDate, eventLocation, eventActive);
            }

            cq.Close();
        }


        int selectedID = -1; // initial selected
        string selectedEventName = "";

        private void eventDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (eventDataGridView.SelectedRows.Count > 0) 
            {
                DataGridViewRow row = eventDataGridView.SelectedRows[0]; // get the first selected row

                if (row.Cells[0].Value != null && int.TryParse(row.Cells[0].Value.ToString(), out int id))
                {
                    selectedID = id;
                    selectedEventName = row.Cells[1].Value?.ToString() ?? "Unknown Event";
                }
            }
        }

        private void eventDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (eventDataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow row = eventDataGridView.SelectedRows[0]; // get the first selected row

                if (row.Cells[0].Value != null && int.TryParse(row.Cells[0].Value.ToString(), out int id))
                {
                    selectedID = id;
                    selectedEventName = row.Cells[1].Value?.ToString() ?? "Unknown Event";
                }
            }
        }

        // removed deleteButton

        private void editEventButton_Click(object sender, EventArgs e)
        {
            if (selectedID == -1)
            {
                MessageBox.Show("Please select an event to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            } else
            {
                editEventForm editEventForm = new editEventForm(selectedID);
                editEventForm.FormClosed += (s, args) => loadEvents(); // refresh costumes after closing
                editEventForm.Show();
            }

        }

    }////
}
