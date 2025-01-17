﻿using System;
using System.IO;
using System.Windows;
using Autofac;
using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using RAMStorage;
using RAMStorage.Serialization;

namespace ProjectManagement
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Default ctor
        /// </summary>
        public App()
        { }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _InitContainer();

            // add-migration ForeignKeys -ProjectName DAL -StartUpProjectName ProjectManagement
            Login login = new Login();

            login.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\RAMStorage\"));

            Storage.Save(path);
        }

        private void _InitContainer()
        {
            var builder = new ContainerBuilder();

            // DBContext
            builder.RegisterType<ProjectManagementContext>().AsSelf();

            // DAL registration
            builder.RegisterType<CompanyDBRepository>().As<ICompanyRepository>();
            builder.RegisterType<CountryDBRepository>().As<ICountryRepository>();
            builder.RegisterType<FormulaDBRepository>().As<IFormulaRepository>();
            builder.RegisterType<MaterialDBRepository>().As<IMaterialRepository>();
            builder.RegisterType<ProjectDBRepository>().As<IProjectRepository>();
            builder.RegisterType<UserDBRepository>().As<IUserRepository>();

            // BLL registration
            builder.RegisterType<CompanyService>().As<ICompanyService>();
            builder.RegisterType<CountryService>().As<ICountryService>();
            builder.RegisterType<FormulaService>().As<IFormulaService>();
            builder.RegisterType<MaterialService>().As<IMaterialService>();
            builder.RegisterType<ProjectService>().As<IProjectService>();
            builder.RegisterType<UserService>().As<IUsersService>();

            // Serialization
            builder.RegisterType<JSONDataContractSerialization>().As<ISerialize>();

            IoC.BuildContainer(builder);
        }
    }
}
