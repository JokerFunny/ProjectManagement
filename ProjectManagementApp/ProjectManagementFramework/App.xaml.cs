using Autofac;
using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using System;
using System.Windows;

namespace ProjectManagementFramework
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
            RegistrationWithServices window = new RegistrationWithServices();

            window.Show();
        }

        private void _InitContainer()
        {
            var builder = new ContainerBuilder();

            // DBContext
            builder.RegisterType<ProjectManagementContext>().AsSelf();

            // DAL registration
            builder.RegisterType<CompanyDBRepository>().As<ICompanyRepository>();

            // BLL registration
            builder.RegisterType<CompanyService>().As<ICompanyService>();

            IoC.BuildContainer(builder);
        }
    }
}
