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
using System.Xml.Linq;
using Microsoft.Data.SqlClient;

namespace WPFPROG
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        //CODE ATTRUBUTION 
        //Easy learning program
        //YOUTUBE VIDEO:https://www.youtube.com/watch?v=uK4z0JT_TUA
        public Login()
        {
            InitializeComponent();
        }
        //SQL CONNECTION STRING
        SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\lab_services_student\\Documents\\registerDB.mdf;Integrated Security=True;Connect Timeout=30");

        //Login button
        private void button_Click(object sender, RoutedEventArgs e)
        {

            if (Usenametxt.Text != "" && passwordBox.Password != "")
            {
                string query = "select count(*) from registerDB where username = '" + Usenametxt.Text + "' and " +
                    "password='" + passwordBox.Password + "'";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                int v = (int)command.ExecuteScalar();
                if (v != 1)
                {//Messages for errors
                    MessageBox.Show("Error in the username or password");
                }
                else
                {//Message if sucessful
                    MessageBox.Show("Welcome to your profile");
                    Usenametxt.Text = "";
                    passwordBox.Password = "";
                    MainWindow mn = new MainWindow();
                    mn.Show();


                }

            }
            else
            {//Message if fields are not inputted
                MessageBox.Show("Fill in all the boxes");
            }
        }
    }
}
