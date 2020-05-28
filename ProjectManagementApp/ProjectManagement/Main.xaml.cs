using Autofac;
using BLL.Interfaces;
using System;
using System.Windows;

namespace ProjectManagement
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private readonly IProjectService _rProjectService;
        private readonly IUsersService _rUsersService;

        public Main()
        {
            InitializeComponent();

            _rUsersService = IoC.Container.Resolve<IUsersService>();

            _rProjectService = IoC.Container.Resolve<IProjectService>();

            projectsList.ItemsSource = _rProjectService.GetProjectViewModelByCompany(_rUsersService.GetCurrentUser().CompanyId);
            projectsList.SelectedIndex = 0;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();

            _rUsersService.ClearCurrentUser();

            Close();
        }

        private void Material_Click(object sender, RoutedEventArgs e)
            => _CreateTargetWindowWithOpenCheck<MaterialWindow>(nameof(MaterialWindow));

        private void Formula_Click(object sender, RoutedEventArgs e)
            => _CreateTargetWindowWithOpenCheck<FormulaWindow>(nameof(FormulaWindow));

        private void Project_Click(object sender, RoutedEventArgs e)
            => _CreateTargetWindowWithOpenCheck<ProjectWindow>(nameof(ProjectWindow));

        private void _CreateTargetWindowWithOpenCheck<T>(string windowName) where T : Window
        {
            if (WindowHelper.IsWindowOpen<T>())
            {
                MessageBox.Show($"{windowName} already opened!");
            }
            else
            {
                T targetWindow = (T)Activator.CreateInstance(typeof(T));
                targetWindow.Show();
            }
        }
    }
}
