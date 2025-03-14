using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DavaoCosplayShopCMS
{
    public partial class editCostumeForm : Form
    {
        private connection con = new connection();
        private int costumeID;
        private string imageLocation = "";

        public editCostumeForm(int costumeID)
        {
            InitializeComponent();
            this.costumeID = costumeID;

            costumeTypeComboBox.SelectedIndex = 0;
            costumeSizeComboBox.SelectedIndex = 0;
            genderComboBox.SelectedIndex = 0;

            loadCostumeDetails();
        }

        private void loadCostumeDetails()
        {
            try
            {
                using (SqlConnection cq = con.getCon())
                {
                    cq.Open();
                    string query = @"SELECT costume_ID, costume_Name, costume_Origin, costumeType_ID, costumeSize_ID, 
                                    costumeGender_ID, costume_Price, costume_Inclusions, costume_IMG
                                    FROM Costume WHERE costume_ID = @costumeID";

                    using (SqlCommand cmd = new SqlCommand(query, cq))
                    {
                        cmd.Parameters.AddWithValue("@costumeID", costumeID);
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                costumeIDTextBox.Text = rd["costume_ID"].ToString();
                                nameTextBox.Text = rd["costume_Name"].ToString();
                                originTextBox.Text = rd["costume_Origin"].ToString();
                                costumeTypeComboBox.SelectedIndex = Convert.ToInt32(rd["costumeType_ID"]) - 1;
                                costumeSizeComboBox.SelectedIndex = Convert.ToInt32(rd["costumeSize_ID"]) - 1;
                                genderComboBox.SelectedIndex = Convert.ToInt32(rd["costumeGender_ID"]) - 1;
                                priceTextBox.Text = rd["costume_Price"].ToString();
                                inclusionsTextBox.Text = rd["costume_Inclusions"].ToString();

                                // Load Image
                                if (!(rd["costume_IMG"] is DBNull) && rd["costume_IMG"] != null)
                                {
                                    byte[] imgBytes = (byte[])rd["costume_IMG"];
                                    using (MemoryStream ms = new MemoryStream(imgBytes))
                                    {
                                        if (costumePictureBox.Image != null)
                                        {
                                            costumePictureBox.Image.Dispose();
                                        }
                                        costumePictureBox.Image = Image.FromStream(ms);
                                    }
                                }
                                else
                                {
                                    costumePictureBox.Image = DavaoCosplayShopCMS.Properties.Resources.davaoCosplayShopIcon;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading costume: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void browseButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif|All Files|*.*"
                };

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imageLocation = dialog.FileName;

                    // Load image without locking the file
                    using (FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read))
                    {
                        if (costumePictureBox.Image != null)
                        {
                            costumePictureBox.Image.Dispose();
                        }
                        costumePictureBox.Image = Image.FromStream(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private byte[] imageToByteArray(Image imageIn)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Make a copy of the image to prevent locking issues
                using (Bitmap bmp = new Bitmap(imageIn))
                {
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                return ms.ToArray();
            }
        }

        private void cancelButton_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void doneButton_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to update this costume?",
                                                  "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            try
            {
                using (SqlConnection cq = con.getCon())
                {
                    cq.Open();

                    string query = @"UPDATE Costume SET costume_Name = @name, costume_Origin = @origin, 
                                    costumeType_ID = @costumeType, costumeSize_ID = @costumeSize, 
                                    costumeGender_ID = @gender, costume_Price = @price, 
                                    costume_Inclusions = @inclusions, costume_IMG = @costumeIMG 
                                    WHERE costume_ID = @costumeID";

                    using (SqlCommand cmd = new SqlCommand(query, cq))
                    {
                        cmd.Parameters.AddWithValue("@name", nameTextBox.Text);
                        cmd.Parameters.AddWithValue("@origin", originTextBox.Text);
                        cmd.Parameters.AddWithValue("@costumeType", costumeTypeComboBox.SelectedIndex + 1);
                        cmd.Parameters.AddWithValue("@costumeSize", costumeSizeComboBox.SelectedIndex + 1);
                        cmd.Parameters.AddWithValue("@gender", genderComboBox.SelectedIndex + 1);

                        if (decimal.TryParse(priceTextBox.Text, out decimal price))
                        {
                            cmd.Parameters.AddWithValue("@price", price);
                        }
                        else
                        {
                            MessageBox.Show("Invalid price format. Please enter a valid number.");
                            return;
                        }

                        cmd.Parameters.AddWithValue("@inclusions", inclusionsTextBox.Text);
                        cmd.Parameters.AddWithValue("@costumeID", costumeID);

                        byte[] imageBytes;
                        if (!string.IsNullOrEmpty(imageLocation) && File.Exists(imageLocation))
                        {
                            using (FileStream fs = new FileStream(imageLocation, FileMode.Open, FileAccess.Read))
                            {
                                imageBytes = imageToByteArray(Image.FromStream(fs));
                            }
                        }
                        else
                        {
                            imageBytes = imageToByteArray(costumePictureBox.Image);
                        }

                        cmd.Parameters.AddWithValue("@costumeIMG", imageBytes);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Costume Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteButton_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this costume?",
                                                  "Confirm Deletion",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection cq = con.getCon())
                    {
                        cq.Open();
                        string query = "DELETE FROM Costume WHERE costume_ID = @costumeID";

                        using (SqlCommand cmd = new SqlCommand(query, cq))
                        {
                            cmd.Parameters.AddWithValue("@costumeID", costumeID);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Costume deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Failed to delete costume. It may not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting costume: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
