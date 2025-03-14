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

namespace DavaoCosplayShopCMS
{
    public partial class mainForm : Form
    {
        connection con = new connection();
        SqlCommand cmd = new SqlCommand();
        SqlConnection cq;
        SqlDataReader rd;

        dashboardForm dashboardForm;
        viewForm viewForm;
        clientsForm clientsForm;
        public paymentsForm paymentsForm;
        public rentsForm rentsForm;
        public mainForm()
        {
            InitializeComponent();
            openDashboard();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void DCSicon_Click(object sender, EventArgs e) // open dashboard
        {
            openDashboard();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openDashboard();
        }

        // Open Forms Methods
        private void openDashboard()
        {
            if (dashboardForm == null)
            {
                dashboardForm = new dashboardForm();
                dashboardForm.FormClosed += Dashboard_FormClosed;
                dashboardForm.MdiParent = this;
                dashboardForm.Dock = DockStyle.Fill;
                dashboardForm.Show();
            }
            else
            {
                // ✅ Reload data when reopening the Dashboard
                dashboardForm.loadDashboardData();
                dashboardForm.Activate();
            }
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            dashboardForm = null;
        }

        private void openViewItems()
        {
            if (viewForm == null)
            {
                viewForm = new viewForm();
                viewForm.FormClosed += Viewform_FormClosed;
                viewForm.MdiParent = this;
                viewForm.Dock = DockStyle.Fill;
                viewForm.Show();
            } else
            {
                viewForm.Activate();
            }
        }

        private void openRents()
        {
            if (rentsForm == null)
            {
                rentsForm = new rentsForm();
                rentsForm.FormClosed += rentsForm_FormClosed;
                rentsForm.MdiParent = this;
                rentsForm.Dock = DockStyle.Fill;
                rentsForm.Show();
            }
            else
            {
                rentsForm.Activate();
            }
        }

        public void rentsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            rentsForm = null;
        }

        private void Viewform_FormClosed(object sender, FormClosedEventArgs e)
        {
            viewForm = null;
        }

        private void openClients()
        {
            if (clientsForm == null)
            {
                clientsForm = new clientsForm();
                clientsForm.FormClosed += Clientsform_FormClosed;
                clientsForm.MdiParent = this;
                clientsForm.Dock = DockStyle.Fill;
                clientsForm.Show();
            }
            else
            {
                clientsForm.Activate();
            }
        }

        private void Clientsform_FormClosed(object sender, FormClosedEventArgs e)
        {
            clientsForm = null;
        }

        //

        private void viewButton_Click_1(object sender, EventArgs e)
        {
            openViewItems();
        }

        private void viewIcon_Click(object sender, EventArgs e)
        {
            openViewItems();
        }
        // clients
        private void clientButton_Click(object sender, EventArgs e)
        {
            openClients();
        }
        private void clientPicture_Click(object sender, EventArgs e)
        {
            openClients();
        }
        // event
        private void eventsButton_Click(object sender, EventArgs e)
        {
            viewEventsForm viewEventsForm = new viewEventsForm();
            viewEventsForm.Show();
        }
        private void eventsPicture_Click(object sender, EventArgs e)
        {
            viewEventsForm viewEventsForm = new viewEventsForm();
            viewEventsForm.Show();
        }
        //
        private void facebookBoxLink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/davaocosplayshop");
        }

        private void paymentsButton_Click(object sender, EventArgs e)
        {
            openPayments();
        }

        private void paymentsPicture_Click(object sender, EventArgs e)
        {
            openPayments();
        }
        private void openPayments()
        {
            if (paymentsForm == null)
            {
                paymentsForm = new paymentsForm();
                paymentsForm.FormClosed += Payments_FormClosed;
                paymentsForm.MdiParent = this;
                paymentsForm.Dock = DockStyle.Fill;
                paymentsForm.Show();
            }
            else
            {
                paymentsForm.Activate();
            }
        }
        public void Payments_FormClosed(object sender, FormClosedEventArgs e)
        {
            paymentsForm = null;
        }

        private void mtoButton_Click(object sender, EventArgs e)
        {
            viewMTOForm viewMTOForm = new viewMTOForm();
            viewMTOForm.Show();
        }

        private void mtoPicture_Click(object sender, EventArgs e)
        {
            viewMTOForm viewMTOForm = new viewMTOForm();
            viewMTOForm.Show();
        }

        private void rentsButton_Click(object sender, EventArgs e)
        {
            openRents();
        }

        private void rentPicture_Click(object sender, EventArgs e)
        {
            openRents();
        }
    } ////
}
