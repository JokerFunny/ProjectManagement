﻿using Autofac;
using BLL.Interfaces;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using Model;

namespace ProjectManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly IUsersService _rUsersService;

        public Login()
        {
            _rUsersService = IoC.Container.Resolve<IUsersService>();

            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
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
                LoginModel loginModel = new LoginModel()
                {
                    Email = textBoxEmail.Text,
                    Password = passwordBox1.Password
                };

                var result = _rUsersService.Connect(loginModel, out string errorMessage);

                if (!result)
                {
                    errormessage.Text = errorMessage;
                }
                else
                {
                    string userName = _rUsersService.GetUserName(loginModel.Email);

                    Main main = new Main();
                    main.textBlockName.Text = $"Welcome, {userName}";
                    main.Show();
                    Close();
                }

                ////SqlConnection con = new SqlConnection("Data Source=TESTPURU;Initial Catalog=Data;User ID=sa;Password=wintellect");
                ////con.Open();
                ////SqlCommand cmd = new SqlCommand("Select * from Registration where Email='" + email + "'  and password='" + password + "'", con);
                ////cmd.CommandType = CommandType.Text;
                ////SqlDataAdapter adapter = new SqlDataAdapter();
                ////adapter.SelectCommand = cmd;
                ////DataSet dataSet = new DataSet();
                ////adapter.Fill(dataSet);
                ////if (dataSet.Tables[0].Rows.Count > 0)
                ////{
                ////    string username = dataSet.Tables[0].Rows[0]["FirstName"].ToString() + " " + dataSet.Tables[0].Rows[0]["LastName"].ToString();
                ////    welcome.TextBlockName.Text = username;//Sending value from one form to another form.  
                ////    main.Show();
                ////    Close();
                ////}
                //else
                //{
                //    errormessage.Text = "Sorry! Please enter existing emailid/password.";
                //}
                ////con.Close();
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration(); 
            registration.Show();
            Close();
        }
    }
}
