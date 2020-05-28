using Autofac;
using BLL.Interfaces;
using System;
using System.Windows;
using System.Text.RegularExpressions;
using Model;

namespace ProjectManagement
{
    /// <summary>  
    /// Interaction logic for Registration.xaml  
    /// </summary>  
    public partial class Registration : Window
    {
        private readonly IUsersService _rUsersService;
        private readonly ICompanyService _rCompanyService;

        public Registration()
        {
            _rUsersService = IoC.Container.Resolve<IUsersService>();

            _rCompanyService = IoC.Container.Resolve<ICompanyService>();

            InitializeComponent();

            Reset();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
            => Reset();

        public void Reset()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            comboBoxCompany.ItemsSource = _rCompanyService.GetAllCompaniesNames();
            comboBoxCompany.SelectedIndex = 0;
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string companyName = comboBoxCompany.SelectedItem.ToString();
            if (string.IsNullOrWhiteSpace(textBoxFirstName.Text))
            {
                errormessage.Text = "Enter an first name.";
                textBoxFirstName.Focus();
            }
            else if (string.IsNullOrWhiteSpace(textBoxLastName.Text))
            {
                errormessage.Text = "Enter an last name.";
                textBoxFirstName.Focus();
            }
            else if (textBoxEmail.Text.Length == 0)
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
            else if (string.IsNullOrWhiteSpace(companyName))
            {
                errormessage.Text = "Select your company!";
                comboBoxCompany.Focus();
            }
            else
            {
                NewUser newUser = new NewUser()
                {
                    FirstName = textBoxFirstName.Text,
                    LastName = textBoxLastName.Text,
                    Email = textBoxEmail.Text,
                    Password = passwordBox1.Password,
                    CompanyId = _rCompanyService.GetCompanyIdByName(companyName)
                };

                //System.Runtime.Serialization.Json.DataContractJsonSerializer ser =
                //    new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(NewUser));
                //System.IO.MemoryStream mem = new System.IO.MemoryStream();
                //ser.WriteObject(mem, newUser);
                //string data =
                //    Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                //System.Net.WebClient webClient = new System.Net.WebClient();
                //webClient.Headers["Content-type"] = "application/json";
                //webClient.Encoding = System.Text.Encoding.UTF8;
                //var result = webClient.UploadString("http://localhost:8081/RegistrationService.svc/users", "POST", data);

                //if (string.IsNullOrWhiteSpace(result.ToString()))
                //{
                //    errormessage.Text = "";
                //    MessageBox.Show("You have Registered successfully!");
                //}
                //else
                //    errormessage.Text = result.ToString();

                //RegistrationServiceMQClient client = new RegistrationServiceMQClient();


                var result = _rUsersService.RegisterNewUser(newUser, out string errorMessage);

                if (!result)
                    errormessage.Text = errorMessage;
                else
                {
                    errormessage.Text = "";

                    MessageBox.Show("You have Registered successfully!");
                    Reset();
                }
            }
        }
    }
}