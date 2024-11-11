using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*cmd.ExecuteReader(),cmd.ExecuteScalar(),cmd.ExecuteNonQuery()*/

namespace DbContactList
{
    public class Contact
    {
        public string? name { get; private set; }
        public string? firstName { get; private set; }
        public string? phoneNumber { get; private set; }

        public Contact(string name, string firstName, string phoneNumber)
        {
            if((name.Length > 10)||(firstName.Length>10)||(phoneNumber.Length>10)) 
            {
                throw new ArgumentException("la longueur des elements ne doit pas depasser 10");
            }
            if(phoneNumber.All(char.IsDigit)==false)
                { throw new ArgumentException("le numero doit etre compose uniquement des chiffres"); }
            
            this.name = name;
            this.firstName = firstName;
            this.phoneNumber = phoneNumber;
        }
        public static void GetContact(string name,SqlConnection connection)
        {
            string query = $"select * from Contact where nom = @name";
            using (SqlCommand cmd = new SqlCommand(query, connection))
               
            {
                cmd.Parameters.AddWithValue("@name", name);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"nom:{reader["nom"]} prenom:{reader["prenom"]} telephone:{reader["telephone"]}");
                    }
                }
            }
        }
        public static void GetAlistOfContact(SqlConnection connection)
        {
            string query = "select * from Contact";
            using(SqlCommand cmd = new SqlCommand(query, connection))
            {
                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"nom:{reader["nom"]} prenom:{reader["prenom"]} telephone:{reader["telephone"]}");
                    }
                }

            }
        }
        public void InsertContact(SqlConnection connection)
        {
            string query = "insert into Contact values (@prenom,@nom,@telephone)";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@nom", this.name);
                cmd.Parameters.AddWithValue("@prenom", this.firstName);
                cmd.Parameters.AddWithValue("@telephone", this.phoneNumber);
                cmd.ExecuteNonQuery();

            }
        }
        public static void DeleteContact(string name,  SqlConnection connection)
        {
            string query = "Delete from Contact where nom = @nom";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@nom", name);
                
                cmd.ExecuteNonQuery();

            }
        }
        public  void UpdateContact(string oldName, SqlConnection connection)
        {
            string query = "update Contact set nom=@nom,prenom=@prenom,telephone=@telephone where nom=@ancienNom";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@nom", this.name);
                cmd.Parameters.AddWithValue("@prenom", this.firstName);
                cmd.Parameters.AddWithValue("@telephone", this.phoneNumber);
                cmd.Parameters.AddWithValue("@ancienNom", oldName);
                cmd.ExecuteNonQuery();

            }
        }

    }
}
