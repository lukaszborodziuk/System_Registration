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
using System.IO;
using SystemRegistration.Model;

namespace SystemRegistration
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void logBt_Click(object sender, RoutedEventArgs e)
        {
            UserData ud = new UserData();
            ud.Nick = nickTxt.Text;
            ud.Password = passTxt.Password;
            ProfileData.Name = nickTxt.Text;

            using (registerDbEntities db = new registerDbEntities())
            {
                if (ud.Nick != string.Empty || ud.Password != string.Empty)
                {
                    var login = db.Users.FirstOrDefault(a => a.nick.Equals(ud.Nick));

                    if (login != null)
                    {
                        if (login.nick.Equals(ud.Nick) && login.password.Equals(ud.Password))
                        {
                            
                            Profile winProfile = new Profile();
                            winProfile.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            winProfile.WindowState = WindowState.Maximized;
                            winProfile.Show();
                            this.Close();
                        }

                        else if (nickTxt.Text == string.Empty || passTxt.Password == string.Empty)
                        {
                            MessageBox.Show("Please complete the data");
                        }

                        else
                        {
                            MessageBox.Show("Login or password is incorrect");
                        }

                    }
                    else if (nickTxt.Text == string.Empty || passTxt.Password == string.Empty)
                    {
                        MessageBox.Show("Please complete the data");
                    }

                    else
                    {
                        MessageBox.Show("Login or password is incorrect");
                    }
                }
                else
                {
                    if (nickTxt.Text == string.Empty || passTxt.Password == string.Empty)
                    {
                        MessageBox.Show("Please complete the data");
                    }
                    else if (nickTxt.Text == string.Empty && passTxt.Password == string.Empty)
                    {
                        MessageBox.Show("Please complete the data");
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
        }
    }
}