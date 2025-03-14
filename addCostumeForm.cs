using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DavaoCosplayShopCMS
{
    public partial class addCostumeForm : Form
    {
        connection con = new connection();
        SqlCommand cmd = new SqlCommand();
        SqlConnection cq;
        SqlDataReader rd;
        String imageLocation = "";
        public addCostumeForm()
        {
            InitializeComponent();
            costumeTypeComboBox.SelectedIndex = 0;
            costumeSizeComboBox.SelectedIndex = 0;
            genderComboBox.SelectedIndex = 0;
        }

        private void browseButton_Click(object sender, EventArgs e) // browse image
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "All Files(*.*)|*.*| PNG files(*.png)|*.png| jpg files(*.jpg)|*.jpg";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;
                    costumePictureBox.ImageLocation = imageLocation;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error Occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addCostumeForm_Load(object sender, EventArgs e) //
        {
            //
        }

        public byte[] imageToByteArray(Image imageIn) // convert image to byte array to database
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void registerButton_Click(object sender, EventArgs e) // register costume 
        {
            try
            {
                using (SqlConnection cq = con.getCon()) 
                {
                    cq.Open();

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Costume (costume_Name, costume_Origin, costumeType_ID, costumeSize_ID, costumeGender_ID, costume_Price, costume_Inclusions, costume_IMG) " +
                         "VALUES (@name, @origin, @costumeType, @costumeSize, @gender, @price, @inclusions, @costumeIMG)", cq))
                    {
                        // Validate and assign parameters
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

                        byte[] imageBytes;
                        if (!string.IsNullOrEmpty(imageLocation) && File.Exists(imageLocation))
                        {
                            imageBytes = imageToByteArray(Image.FromFile(imageLocation));
                        }
                        else
                        {
                            imageBytes = imageToByteArray(DavaoCosplayShopCMS.Properties.Resources.davaoCosplayShopIcon);
                        }

                        cmd.Parameters.AddWithValue("@costumeIMG", imageBytes);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Costume Registered Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
