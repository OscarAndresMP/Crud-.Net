﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crud
{
   public class DataAccesslayer
    {
        private SqlConnection conn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=WinFormsContacts;Data Source=LAPTOP-JUEIRQRS\\SQLEXPRESS");
        public void InsertContact(Contact contact)
        {
            try
            {
                conn.Open();
                string query = @"
                                INSERT INTO Contacts (FirstName, LastName, Phone, Address)
                                VALUES (@FirstName, @LastName, @Phone, @Address)";
                SqlParameter firstName = new SqlParameter("@FirstName", contact.FirstName);
                SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Address", contact.Address);

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Contact> GetContacts(string searchText = null)
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                conn.Open();
                string query = @" SELECT Id, FirstName, LastName, Phone, Address FROM Contacts ";

                SqlCommand command = new SqlCommand();

                if(!string.IsNullOrEmpty(searchText))
                {
                    query += @"WHERE FirstName LIKE @Search OR LastName LIKE @Search OR Phone LIKE @Search
                                     OR Address LIKE @Search";
                    command.Parameters.Add(new SqlParameter("@Search", $"%{searchText}%"));
                }

                command.CommandText = query;
                command.Connection = conn;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    contacts.Add(new Contact
                    {
                        id = int.Parse(reader["Id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString()
                    }); 
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }

            return contacts;
        }

        public void UpdateContact(Contact contact)
        {
            try
            {
                conn.Open();
                string query = @"UPDATE Contacts SET FirstName = @FirstName, LastName = @LastName,
                                                     Phone = @Phone, Address= @Address WHERE Id = @Id";
                SqlParameter id = new SqlParameter("@Id", contact.id);
                SqlParameter firstName = new SqlParameter("@FirstName", contact.FirstName);
                SqlParameter lastName = new SqlParameter("@LastName", contact.LastName);
                SqlParameter phone = new SqlParameter("@Phone", contact.Phone);
                SqlParameter address = new SqlParameter("@Address", contact.Address);

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(id);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
        }

        public void DeleteContact(int id)
        {
            try
            {
                conn.Open();
                string query = @"DELETE Contacts WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();



            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
        }
    }
}
