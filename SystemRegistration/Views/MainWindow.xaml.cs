using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SystemRegistration;
using SystemRegistration.Model;

namespace SystemRegistration
{
    public partial class MainWindow : Window
    {
        public registerDbEntities db;
        public UserData ud;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void regBt_Click(object sender, RoutedEventArgs e)
        {
            ud = new UserData();
            ud.Name = nameTxt.Text;
            ud.Surname = surnameTxt.Text;
            ud.Nick = nickTxt.Text;
            ud.Email = emailTxt.Text;
            ud.Password = passbox.Password;
            ud.PassRepeat = rPassbox.Password;
            ud.DateBirth = dataBox.Text;
            ud.TypeAccount = accountSelect.Text;
            
            ProfileData.Name = ud.Nick;
            ProfileData.Account = ud.TypeAccount;
            SaveData sd = new SaveData();
            sd.DataToSave(ud, "registerData.xml");

            using (db = new registerDbEntities())
            {
                ud.Users = new List<User>();

                if (nameTxt.Text != string.Empty)
                {
                    if (surnameTxt.Text != string.Empty)
                    {
                        if (nickTxt.Text != string.Empty)
                        {
                            if (emailTxt.Text != string.Empty)
                            {
                                if (dataBox.Text != string.Empty)
                                {
                                    if (passbox.Password != string.Empty)
                                    {
                                        if (rPassbox.Password != string.Empty)
                                        {
                                            ud.Users.Add(new User()
                                            {
                                                name = ud.Name,
                                                surname = ud.Surname,
                                                nick = ud.Nick,
                                                email = ud.Email,
                                                data_birth = Convert.ToDateTime(ud.DateBirth),
                                                password = ud.Password,
                                                pass_repeat = ud.PassRepeat
                                            });
                                        }
                                        else
                                        {
                                            MessageBox.Show("Please repeat password");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Password is required");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Date of birth is required");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Email is required");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nick is required");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Surname is required");
                    }
                }
                else
                {
                    MessageBox.Show("Name is required");
                }
                foreach (var user in ud.Users)
                {
                    if (user != null)
                    {
                        if (!NotEmpty(ud.Name, ud.Surname, ud.Name, ud.Email, ud.DateBirth, ud.Password, ud.PassRepeat))
                        {
                            MessageBox.Show("Please complete all the data");
                        }
                        else
                        {
                            if (IsValid(emailTxt.Text))
                            {
                                if (passbox.Password == rPassbox.Password)
                                {
                                    user.name = ud.Name;
                                    user.surname = ud.Surname;
                                    user.nick = ud.Nick;
                                    user.email = ud.Email;
                                    user.password = ud.Password;
                                    user.pass_repeat = ud.PassRepeat;
                                    user.data_birth = Convert.ToDateTime(ud.DateBirth);
                                    db.Users.Add(user);
                                    db.SaveChanges();
                                }
                            }
                            else
                            {
                                MessageBox.Show("It's not address email");
                            }
                        }
                    }
                }
            }
        }
        private void logBt_Click(object sender, RoutedEventArgs e)
        {
            Login logWindow = new Login();
            logWindow.WindowState = WindowState.Maximized;
            logWindow.Show();
        }

        private void Register_Initialized(object sender, EventArgs e)
        {
            Register.WindowState = WindowState.Maximized;
        }

        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private bool NotEmpty(string Name, string Surname, string Nick, string Email, string DateBirth,string Password, string PassRepeat )
        {
                Name = nameTxt.Text;
                Surname = surnameTxt.Text;
                Nick = nickTxt.Text;
                Email = emailTxt.Text;
                DateBirth = dataBox.Text;
                Password = passbox.Password;
                PassRepeat = rPassbox.Password;
                return true;
        }    
    } 
}
