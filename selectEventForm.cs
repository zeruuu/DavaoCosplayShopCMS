using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DavaoCosplayShopCMS
{
    public partial class selectEventForm : Form
    {
        connection con = new connection();
        SqlCommand cmd = new SqlCommand();
        SqlConnection cq;
        SqlDataReader rd;
        public string SelectedEvent { get; private set; } = "";
        public string SelectedEventID { get; private set; }
        public string SelectedDate { get; private set; }

        public selectEventForm()
        {
            InitializeComponent();
            eventsList.FullRowSelect = true; // selected row
            eventsList.ItemActivate += eventsList_ItemActivate; // event handler
            loadEvents();
        }
        private void loadEvents()
        {
            eventsList.View = View.Details; 
            eventsList.FullRowSelect = true; 
            eventsList.GridLines = true; 

            eventsList.Columns.Clear();
            eventsList.Items.Clear();

            eventsList.Columns.Add("Event ID", 30);
            eventsList.Columns.Add("Event Name", 200);
            eventsList.Columns.Add("Event Location", 150);
            eventsList.Columns.Add("Date", 100);

            using (SqlConnection cq = con.getCon())
            {
                cq.Open();
                string query = "SELECT event_ID, event_Name, event_Location, event_Date FROM Event ORDER BY event_Date DESC";
                using (SqlCommand cmd = new SqlCommand(query, cq))
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        ListViewItem item = new ListViewItem(rd["event_ID"].ToString());
                        item.SubItems.Add(rd["event_Name"].ToString());
                        item.SubItems.Add(rd["event_Location"].ToString()); 
                        item.SubItems.Add(Convert.ToDateTime(rd["event_Date"]).ToString("yyyy-MM-dd")); 

                        item.Tag = rd["event_ID"]; 
                        eventsList.Items.Add(item);
                    }
                }
            }
        }

        private void eventsList_ItemActivate(object sender, EventArgs e) // selecting
        {
            if (eventsList.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = eventsList.SelectedItems[0]; // Get first selected row
                SelectedEventID = selectedItem.SubItems[0].Text;  // First column: Event ID
                SelectedEvent = selectedItem.SubItems[1].Text;    // Second column: Event Name
                SelectedDate = DateTime.Parse(selectedItem.SubItems[3].Text).ToString("yyyy-MM-dd");
                DialogResult = DialogResult.OK; // return selected 
            }
        }

        private void addEventButton_Click(object sender, EventArgs e)
        {
            addEventForm addEventForm = new addEventForm();
            addEventForm.FormClosed += (s, args) => loadEvents(); // refresh costumes after closing
            addEventForm.Show();
        }
    }
}
