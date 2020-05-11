using System.Windows;
using System.Text.RegularExpressions;
using RAMStorage;
using System.Linq;
using BLL.Interfaces;
using BLL;
using DAL;
using Model;
using System;

namespace ProjectManagement
{
    /// <summary>  
    /// Interaction logic for Registration.xaml  
    /// </summary>  
    public partial class Registration : Window
    {
        private readonly IUsersService _rUserService;

        public Registration()
        {
            _rUserService = new UserService(new UserRAMRepository());
            InitializeComponent();

            comboBoxCompany.ItemsSource = Storage.Companies.Select(c => c.Name); // handle with service
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            comboBoxCompany.ItemsSource = Storage.Companies.Select(c => c.Name); // change to service impl
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Enter an email.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                NewUser newUser = new NewUser()
                {
                    FirstName = textBoxFirstName.Text,
                    LastName = textBoxLastName.Text,
                    Email = textBoxEmail.Text,
                    Password = passwordBox1.Password,
                    CompanyId = Guid.Empty //textBlockCompany.Text and with service
                };

                var result = _rUserService.RegisterNewUser(newUser, out string errorMessage);

                if (!result)
                    errormessage.Text = errorMessage;
                else
                {
                    errormessage.Text = "";

                    MessageBox.Show("You have Registered successfully!");
                    Reset();
                }

                //SqlConnection con = new SqlConnection("Data Source=TESTPURU;Initial Catalog=Data;User ID=sa;Password=wintellect");
                //con.Open();
                //SqlCommand cmd = new SqlCommand("Insert into Registration (FirstName,LastName,Email,Password,Address) values('" + firstname + "','" + lastname + "','" + email + "','" + password + "','" + address + "')", con);
                //cmd.CommandType = CommandType.Text;
                //cmd.ExecuteNonQuery();
                //con.Close();
            }
        }
    }
}