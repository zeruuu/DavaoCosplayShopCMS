using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DavaoCosplayShopCMS
{
    public partial class transactionHistory : Form
    {
        connection con = new connection();
        SqlCommand cmd = new SqlCommand();
        SqlConnection cq;
        SqlDataReader rd;

        public transactionHistory()
        {
            InitializeComponent();
            loadTransactions();
            transactionHistoryGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            transactionHistoryGridView.MultiSelect = false;
        }

        private void loadTransactions()
        {
            transactionHistoryGridView.Rows.Clear();
            cq = con.getCon();
            cq.Open();

            cmd = new SqlCommand("SELECT * FROM ClientTransactions ORDER BY transaction_Date DESC, transaction_ID DESC", cq);
            rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                string transactionID = rd["transaction_ID"].ToString();
                string clientID = rd["client_ID"].ToString();
                string clientName = rd["client_Name"].ToString();
                string transactionType = rd["TransactionType"].ToString();

                // Display Full Date + Time for Debugging
                string transactionDate = Convert.ToDateTime(rd["transaction_Date"]).ToString("yyyy-MM-dd HH:mm:ss");

                string amount = Convert.ToDecimal(rd["Amount"]).ToString(); // Format as currency

                transactionHistoryGridView.Rows.Add(transactionID, clientID, clientName, transactionDate, transactionType, amount);
            }

            cq.Close();
        }

    }
}
