using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DavaoCosplayShopCMS
{
    public partial class selectCostumeForm : Form
    {
        connection con = new connection();
        SqlCommand cmd;
        SqlConnection cq;
        SqlDataReader rd;

        public string SelectedCostumeID { get; private set; }

        public selectCostumeForm()
        {
            InitializeComponent();
            costumesList.FullRowSelect = true;
            costumesList.ItemActivate += costumesList_ItemActivate;
            searchTextBox.TextChanged += searchTextBox_TextChanged; // Bind event
            loadCostumes();
        }

        private void loadCostumes(string searchQuery = "")
        {
            costumesList.View = View.LargeIcon;
            costumesList.FullRowSelect = true;
            costumesList.GridLines = true;

            costumesList.Columns.Clear();
            costumesList.Items.Clear();

            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(100, 100);
            costumesList.LargeImageList = imageList;

            try
            {
                using (cq = con.getCon()) // Assign connection object
                {
                    cq.Open();
                    string query = @"
                        SELECT costume_ID, costume_Name, costume_Origin, costume_IMG 
                        FROM Costume 
                        WHERE costume_Available = 1 
                        AND (costume_Name LIKE @search OR costume_Origin LIKE @search)
                        ORDER BY costume_Name";

                    using (cmd = new SqlCommand(query, cq)) // Assign command object
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + searchQuery + "%");

                        using (rd = cmd.ExecuteReader()) // Assign reader object
                        {
                            int imageIndex = 0;
                            while (rd.Read())
                            {
                                byte[] imgData = rd["costume_IMG"] as byte[];
                                if (imgData != null && imgData.Length > 0)
                                {
                                    using (MemoryStream ms = new MemoryStream(imgData))
                                    {
                                        imageList.Images.Add(Image.FromStream(ms));
                                    }
                                }
                                else
                                {
                                    imageList.Images.Add(Properties.Resources.davaoCosplayShopIcon);
                                }

                                ListViewItem item = new ListViewItem($"{rd["costume_Name"]} ({rd["costume_Origin"]})");
                                item.ImageIndex = imageIndex;
                                item.Tag = rd["costume_ID"];
                                costumesList.Items.Add(item);
                                imageIndex++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading costumes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void costumesList_ItemActivate(object sender, EventArgs e)
        {
            if (costumesList.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = costumesList.SelectedItems[0];
                SelectedCostumeID = selectedItem.Tag.ToString();
                DialogResult = DialogResult.OK;
            }
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            loadCostumes(searchTextBox.Text.Trim()); // Reload list with search filter
        }
    }
}
