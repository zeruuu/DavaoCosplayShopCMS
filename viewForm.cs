using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using System.Security.Policy;

namespace DavaoCosplayShopCMS
{
    public partial class viewForm : Form
    {
        connection con = new connection();
        SqlCommand cmd = new SqlCommand();
        SqlConnection cq;
        SqlDataReader rd;
        public viewForm()
        {
            InitializeComponent();
            if (sortBox.InvokeRequired)
            {
                sortBox.Invoke(new Action(() => sortBox.SelectedItem = "None"));
            }
            else
            {
                sortBox.SelectedItem = "None";
            }
        }

        private async void viewForm_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            loadCostumes();
            sortBox.Items.Clear();
            sortBox.Items.Add("None");
            sortBox.Items.Add("Name");
            sortBox.Items.Add("Price");
            sortBox.Items.Add("Available");
            sortBox.SelectedItem = "None";
        }

        public async void loadCostumes(string searchQuery = "")
        {
            viewCostumesFlowLayoutPanel.Controls.Clear();

            await Task.Run(() =>
            {
                try
                {
                    using (SqlConnection cq = con.getCon())
                    {
                        cq.Open();
                        string query = "SELECT * FROM Costume WHERE costume_Name LIKE @searchQuery OR costume_Origin LIKE @searchQuery";

                        // Apply sorting
                        string sortOption = "";
                        if (sortBox.InvokeRequired)
                        {
                            sortBox.Invoke(new Action(() => sortOption = sortBox.SelectedItem?.ToString()));
                        }
                        else
                        {
                            sortOption = sortBox.SelectedItem?.ToString();
                        }

                        if (sortOption == "Name")
                            query += " ORDER BY costume_Name ASC";
                        else if (sortOption == "Price")
                            query += " ORDER BY costume_Price ASC";
                        else if (sortOption == "Available")
                            query += " ORDER BY costume_Available ASC";

                        using (SqlCommand cmd = new SqlCommand(query, cq))
                        {
                            cmd.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");

                            using (SqlDataReader rd = cmd.ExecuteReader())
                            {
                                List<Control> items = new List<Control>(); // Store items to add later

                                while (rd.Read())
                                {
                                    int costume_ID = (int)rd["costume_ID"];
                                    string costume_Name = rd["costume_Name"].ToString();
                                    string costume_Origin = rd["costume_Origin"].ToString();
                                    int costumeType_ID = (int)rd["costumeType_ID"];
                                    int costumeSize_ID = (int)rd["costumeSize_ID"];
                                    int costumeGender_ID = (int)rd["costumeGender_ID"];
                                    decimal costume_Price = (decimal)rd["costume_Price"];
                                    string costume_Inclusions = rd["costume_Inclusions"].ToString();
                                    bool costume_Available = (bool)rd["costume_Available"];
                                    byte[] costume_IMG = rd["costume_IMG"] as byte[];

                                    Image costumeImage = null;
                                    if (costume_IMG != null && costume_IMG.Length > 0)
                                    {
                                        using (MemoryStream ms = new MemoryStream(costume_IMG))
                                        {
                                            costumeImage = Image.FromStream(ms);
                                        }
                                    }

                                    Control itemPanel = CreateItemPanel(
                                        costume_ID, costume_Name, costume_Origin,
                                        costumeType_ID, costumeSize_ID, costumeGender_ID,
                                        costume_Price, costume_Inclusions, costumeImage, costume_Available
                                    );

                                    items.Add(itemPanel);
                                }

                                // Update UI safely
                                this.Invoke(new Action(() =>
                                {
                                    viewCostumesFlowLayoutPanel.Controls.AddRange(items.ToArray());
                                }));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Invoke(new Action(() => MessageBox.Show("Error loading costumes: " + ex.Message)));
                }
            });
        }

        private Control CreateItemPanel(int itemID, string name, string origin, int typeID, int sizeID, int genderID, decimal price, string inclusions, Image image, bool isAvailable)
        {
            FlowLayoutPanel itemPanel = new FlowLayoutPanel
            {
                Width = 190,
                Height = 440,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = isAvailable ? Color.LightBlue : Color.MistyRose,
                FlowDirection = FlowDirection.TopDown
            };

            PictureBox pictureBox = new PictureBox
            {
                Image = image ?? DavaoCosplayShopCMS.Properties.Resources.davaoCosplayShopIcon,
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 180,
                Height = 180,
                BorderStyle = BorderStyle.FixedSingle
            };

            Label nameLabel = new Label { Text = name, Font = new Font("Segoe UI", 10, FontStyle.Bold), Width = 180, BackColor = Color.White };
            Label originLabel = new Label { Text = $"Origin: {origin}", Width = 180 };
            Label sizeLabel = new Label { Text = $"Size: {GetSizeName(sizeID)}", Width = 180 };
            Label genderLabel = new Label { Text = $"Gender: {GetGenderName(genderID)}", Width = 180 };
            Label priceLabel = new Label { Text = $"Price: ₱{price}", Font = new Font("Segoe UI", 9, FontStyle.Bold), Width = 180 };
            Label inclusionsLabel = new Label { Text = $"Inclusions: {inclusions}", Width = 180, Height = 80, AutoSize = false, TextAlign = ContentAlignment.TopLeft };

            itemPanel.Controls.AddRange(new Control[] { pictureBox, nameLabel, originLabel, sizeLabel, genderLabel, priceLabel, inclusionsLabel });

            if (isAvailable)
            {
                Button editButton = new Button { Text = "EDIT COSTUME", Width = 180, Height = 30, BackColor = Color.LightGray };
                editButton.Click += (sender, e) =>
                {
                    editCostumeForm editForm = new editCostumeForm(itemID);
                    editForm.FormClosed += (s, args) => loadCostumes();
                    editForm.ShowDialog();
                };
                itemPanel.Controls.Add(editButton);
            }
            else
            {
                Label rentedLabel = new Label { Text = "CURRENTLY RENTED", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.Red, Width = 180, Height = 30, TextAlign = ContentAlignment.MiddleCenter };
                itemPanel.Controls.Add(rentedLabel);
            }

            return itemPanel;
        }

        private string GetSizeName(int sizeID)
        {
            if (sizeID == 1)
            {
                return "KIDS";
            }
            else if (sizeID == 2)
            {
                return "SMALL";
            }
            else if (sizeID == 3)
            {
                return "SMALL - MEDIUM";
            }
            else if (sizeID == 4)
            {
                return "MEDIUM";
            }
            else if (sizeID == 5)
            {
                return "MEDIUM - LARGE";
            } else if (sizeID == 6)
            {
                return "LARGE";
            } else if (sizeID == 7)
            {
                return "XLARGE";
            } else if (sizeID == 8)
            {
                return "XXLARGE";
            } else if (sizeID == 9)
            {
                return "ALL SIZES";
            } else if (sizeID == 10)
            {
                return "NOT APPLICABLE";
            } else
            {
                return "NOT APPLICABLE";
            }
        }

        private string GetGenderName(int genderID)
        {
            if (genderID == 1)
            {
                return "Male";
            } else if (genderID == 2)
            {
                return "Female";
            } else
            {
                return "n/a";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) // search textbox
        {
            loadCostumes(textBox1.Text.Trim()); // load costumes based on search input
        }

        private void addCostumeButton_Click(object sender, EventArgs e)
        {
            addCostumeForm addForm = new addCostumeForm();
            addForm.FormClosed += (s, args) => loadCostumes(); // refresh costumes after closing
            addForm.Show();
        } // add costume form

        private void sortBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCostumes(textBox1.Text.Trim());
        }

        private void refreshCostumesButton_Click(object sender, EventArgs e)
        {
            loadCostumes();
        }
    }
}
