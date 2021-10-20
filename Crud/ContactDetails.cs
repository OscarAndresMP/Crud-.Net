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
    public partial class ContactDetails : Form
    {
        private BusinessLogicLayer _businessLogicLayer;
        private Contact _contact;
        public ContactDetails()
        {
            InitializeComponent();
            _businessLogicLayer = new BusinessLogicLayer();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            SaveContact();
            this.Close();
            ((Main)this.Owner).PopulateConatacts();

        }

        private void SaveContact()
        {
            Contact contact = new Contact();
            contact.FirstName = txtFirstName.Text;
            contact.LastName = txtLastname.Text;
            contact.Phone = txtPhone.Text;
            contact.Address = txtaddress.Text;

            contact.id = _contact != null ? _contact.id : 0;
            _businessLogicLayer.SaveContact(contact);
        }

        public void LoadCantact(Contact contact)
        {
            _contact = contact;
            if (contact != null)
            {
                ClearForm();
                txtFirstName.Text = contact.FirstName;
                txtLastname.Text = contact.LastName;
                txtPhone.Text = contact.Phone;
                txtaddress.Text = contact.Address;
            }
        }
        private void ClearForm()
        {
            txtFirstName.Text = string.Empty;
            txtLastname.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtaddress.Text = string.Empty;
        }
    }
}
