using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Data.SqlClient;

//Author 
//St10083869
//SHEREL DAVARAJ

//This project contains 3 windows and 1 class libraray 
namespace WPFPROG
{
    /// <summary>
    /// Interaction logic for reg.xaml
    /// </summary>
    public partial class reg : Window
    {
        //CODE ATTRBUTION
        //Connect to a MSSQL database (no date).
        /// <summary>
        /// /https://docs.devart.com/studio-for-sql-server/getting-started/connect-to-sql-database.html.
      
        public reg()
        {
            InitializeComponent();
        }
        //SQL CONNECTION STRING
        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\lab_services_student\\Documents\\registerDB.mdf;Integrated Security=True;Connect Timeout=30");
        
        //registration button actiavte for when it is clicked on
        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //If else statement presented
                if (Usenametxt.Text != "" && passwordBox.Password != "" && Firstnametxt.Text != "" && Lastnametxt.Text != "")
                {
                    if (passwordBox.Password == conpasswordBox.Password)
                    {
                        int v = check(Usenametxt.Text);
                        if (v != 1)
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand("INSERT INTO registerDB VALUES (@username, @password, @first_name, @last_name)", connection);
                            command.Parameters.AddWithValue("@username", Usenametxt.Text);
                            command.Parameters.AddWithValue("@password", passwordBox.Password);
                            command.Parameters.AddWithValue("@first_name", Firstnametxt.Text);
                            command.Parameters.AddWithValue("@last_name", Lastnametxt.Text);
                            command.ExecuteNonQuery();
                            connection.Close();
                            MessageBox.Show("Registered Successfully!");
                            Usenametxt.Text = "";
                            passwordBox.Password = "";
                            Firstnametxt.Text = "";
                            Lastnametxt.Text = "";

                            Login l = new Login();
                            l.Show();

                        }
                        else
                        {//message will show if successful
                            MessageBox.Show("You are already registered");


                        }
                    }
                    else
                    {
                        //if password does not match
                        MessageBox.Show("Password does not match");
                    }
                }
                else
                {
                    MessageBox.Show("Fill in all the boxes");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //for the SQL
        int check(string username)
        {
            connection.Open();
            string query = "SELECT COUNT(*) FROM registerDB WHERE username = '" + username + "'";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            int v = (int)command.ExecuteScalar();
            connection.Close();
            return v;
        }


    }
}
