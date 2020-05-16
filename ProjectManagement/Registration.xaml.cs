using System.Windows;
using System.Text.RegularExpressions;
using BLL.Interfaces;
using BLL;
using DAL;
using Model;

namespace ProjectManagement
{
    /// <summary>  
    /// Interaction logic for Registration.xaml  
    /// </summary>  
    public partial class Registration : Window
    {
        private readonly IUsersService _rUserService;
        private readonly ICompanyService _rCompanyService;

        public Registration()
        {
            _rUserService = new UserService(new UserRAMRepository());
            _rCompanyService = new CompanyService(new CompanyRAMRepository());

            InitializeComponent();

            comboBoxCompany.ItemsSource = _rCompanyService.GetAllCompaniesNames();

            comboBoxCompany.SelectedIndex = 0;
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