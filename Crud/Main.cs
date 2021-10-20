using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crud
{
    public partial class Main : Form
    {
        private BusinessLogicLayer _businessLogicLayer;
        public Main()
        {
            InitializeComponent();
            _businessLogicLayer = new BusinessLogicLayer();
        }
        private void btnadd_Click(object sender, EventArgs e)
        {
            OpenShowDialog();
        }



        private void OpenShowDialog()
        {
            ContactDetails contactDetails = new ContactDetails();
            contactDetails.ShowDialog(this);

        }

        private void Main_Load(object sender, EventArgs e)
        {
            PopulateConatacts();
        }
        public void PopulateConatacts(string searchText = null)
        {
            List<Contact> contacts = _businessLogicLayer.GetContacts(searchText);
            gridContacs.DataSource = contacts;
        }

        private void gridContacs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewLinkCell cell = (DataGridViewLinkCell)gridContacs.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value.ToString() == "Edit")
            {
                ContactDetails contactDetails = new ContactDetails();
                contactDetails.LoadCantact(new Contact
                {
                    id = int.Parse(gridContacs.Rows[e.RowIndex].Cells[0].Value.ToString()),
                    FirstName = gridContacs.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    LastName = gridContacs.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    Phone = gridContacs.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Address = gridContacs.Rows[e.RowIndex].Cells[4].Value.ToString(),
                });
                contactDetails.ShowDialog(this);
            }
            else if(cell.Value.ToString() == "Delete")
            {
                DeleteContact(int.Parse(gridContacs.Rows[e.RowIndex].Cells[0].Value.ToString()));
                PopulateConatacts();
            }
        }

        private void DeleteContact(int id)
        {
            _businessLogicLayer.DeleteContact(id);
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            PopulateConatacts(txtsearch.Text);
            txtsearch.Text = string.Empty;
        }
    }
}
