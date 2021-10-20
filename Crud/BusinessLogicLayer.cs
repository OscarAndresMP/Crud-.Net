using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud
{
   public class BusinessLogicLayer
    {
        private DataAccesslayer _dataAccesslayer;

        public BusinessLogicLayer()
        {
            _dataAccesslayer = new DataAccesslayer();
        }

        public Contact SaveContact(Contact contact)
        {
            if (contact.id == 0)
                _dataAccesslayer.InsertContact(contact);
            else
                _dataAccesslayer.UpdateContact(contact);
            return contact;
            
        }

        public List<Contact> GetContacts(string searchText = null)
        {
            return _dataAccesslayer.GetContacts(searchText);
        }

        public void DeleteContact(int id)
        {
            _dataAccesslayer.DeleteContact(id);
        }

    }
}
