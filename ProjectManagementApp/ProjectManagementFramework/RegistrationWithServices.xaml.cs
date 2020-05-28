using Autofac;
using BLL.Interfaces;
using ProjectManagementFramework.RegistrationMQService;
using ProjectManagementFramework.RegistrationASMXService;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using Model;

namespace ProjectManagementFramework
{
    /// <summary>
    /// Interaction logic for RegistrationWithServices.xaml
    /// </summary>
    public partial class RegistrationWithServices : Window
    {
        private readonly ICompanyService _rCompanyService;

        public RegistrationWithServices()
        {
            _rCompanyService = IoC.Container.Resolve<ICompanyService>();

            InitializeComponent();

            Reset();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
          => Reset();

        private void Cancel_Click(object sender, RoutedEventArgs e)
            => Close();

        public void Reset()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            comboBoxCompany.ItemsSource = _rCompanyService.GetAllCompaniesNames().ToList();
            comboBoxCompany.SelectedIndex = 0;
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (_CheckInput())
            {
                NewUser newUser = new NewUser()
                {
                    FirstName = textBoxFirstName.Text,
                    LastName = textBoxLastName.Text,
                    Email = textBoxEmail.Text,
                    Password = passwordBox1.Password,
                    CompanyId = _rCompanyService.GetCompanyIdByName(comboBoxCompany.SelectedItem.ToString())
                };

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(NewUser));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, newUser);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                System.Net.WebClient webClient = new System.Net.WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;

                var result = webClient.UploadString("http://localhost:8081/RegistrationService.svc/create", "POST", data);

                webClient.Dispose();

                if (result.ToString() == "\"\"")
                {
                    errormessage.Text = "";
                    MessageBox.Show("You have Registered successfully!");
                }
                else
                {
                    errormessage.Text = result.ToString();
                }
            }
        }

        private void SubmitMQ_Click(object sender, RoutedEventArgs e)
        {
            if (_CheckInput())
            {
                NewUser newUser = new NewUser()
                {
                    FirstName = textBoxFirstName.Text,
                    LastName = textBoxLastName.Text,
                    Email = textBoxEmail.Text,
                    Password = passwordBox1.Password,
                    CompanyId = _rCompanyService.GetCompanyIdByName(comboBoxCompany.SelectedItem.ToString())
                };

                using (RegistrationServiceMQClient client = new RegistrationServiceMQClient())
                {
                    RegisterUser registerUser = new RegisterUser(newUser);

                    client.RegisterUser(registerUser);
                }

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(NewUser));
                MemoryStream mem = new MemoryStream();
                ser.WriteObject(mem, textBoxEmail.Text);
                string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
                System.Net.WebClient webClient = new System.Net.WebClient();
                webClient.Headers["Content-type"] = "application/json";
                webClient.Encoding = Encoding.UTF8;
                var result = webClient.UploadString("http://localhost:8081/RegistrationService.svc/getByEmail", "POST", data);

                if (bool.Parse(result))
                {
                    errormessage.Text = "";
                    MessageBox.Show("You have Registered successfully!");
                }
                else
                {
                    errormessage.Text = $"Error occured. Email user with email {textBoxEmail.Text} already exist or problems with pass";
                }
            }
        }

        private void SubmitASMX_Click(object sender, RoutedEventArgs e)
        {
            if (_CheckInput())
            {
                NewUserDTO newUser = new NewUserDTO()
                {
                    FirstName = textBoxFirstName.Text,
                    LastName = textBoxLastName.Text,
                    Email = textBoxEmail.Text,
                    Password = passwordBox1.Password,
                    CompanyId = _rCompanyService.GetCompanyIdByName(comboBoxCompany.SelectedItem.ToString())
                };

                var client = new RegistrationASMXServiceSoapClient();

                var result = client.RegisterUser(newUser);

                if (string.IsNullOrWhiteSpace(result))
                {
                    errormessage.Text = "";
                    MessageBox.Show("You have Registered successfully!");
                }
                else
                {
                    errormessage.Text = result.ToString();
                }
            }
        }

        private bool _CheckInput()
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
                return true;
            }

            return false;
        }
    }
}
