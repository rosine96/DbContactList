using System;
using System.Data.SqlClient;
//importer System.Data.SqlClient dans le program

namespace DbContactList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int choix = 0,decision=1;
            string name=null;
            string firstname = null;
            string telephoneNumber = null;

            string settings = "Server =DESKTOP-MPQKL6T\\SQLEXPRESS01;Database=ContactList;Integrated Security=True;";
           
            using (SqlConnection connection = new SqlConnection(settings))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("connexion reussi");
                    do
                    {
                        Console.WriteLine(@"welcome to contactList!!!what do you want to do?
                             1-get all of contact
                             2-get one contact
                             3-add a contact
                             4-delete a contact
                             5-update a contact");
                        choix = int.Parse(Console.ReadLine());
                        switch (choix)
                        {
                            case 1:
                                Contact.GetAlistOfContact(connection);
                                break;
                            case 2:
                                Console.WriteLine("entrer le nom");
                                 name = Console.ReadLine();
                                Contact.GetContact(name, connection);
                                break;
                            case 3:
                                Console.WriteLine("entrer le prenom");
                                 firstname = Console.ReadLine();
                                Console.WriteLine("entrer le nom");
                                name = Console.ReadLine();
                                Console.WriteLine("entrer le telephone");
                                telephoneNumber = Console.ReadLine();
                                Contact monContact=new Contact(name,firstname, telephoneNumber);
                                monContact.InsertContact(connection);
                                Console.WriteLine("ajout reussi");
                                break;
                            case 4:
                                Console.WriteLine("entrer le nom");
                                name = Console.ReadLine();
                                Contact.DeleteContact(name, connection);
                                Console.WriteLine("contact supprime");
                                break;
                            case 5:
                                Console.WriteLine("entrer le nom actuel");
                                string actName = Console.ReadLine();
                                Console.WriteLine("entrer le prenom");
                                firstname = Console.ReadLine();
                                Console.WriteLine("entrer le nom");
                                name = Console.ReadLine();
                                Console.WriteLine("entrer le telephone");
                                telephoneNumber = Console.ReadLine();
                                Contact myContact = new Contact(name, firstname, telephoneNumber);
                                myContact.UpdateContact(actName,connection);
                                Console.WriteLine("mise a jour complete avec succes");
                                break;
                            default:
                                Console.WriteLine("choix non disponible");
                                break;

                        }
                        Console.WriteLine(@"do you want to do anything else?
                                              1-yes
                                              2-no");
                        decision = int.Parse(Console.ReadLine());
                    } while (decision == 1);
                    
                    
                }
                catch (SqlException err)
                {
                    Console.WriteLine(err.Message);
                }
                catch (Exception e) 
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
