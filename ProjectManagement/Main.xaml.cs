using Model;
using RAMStorage;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ProjectManagement
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            
            var project = Storage.Projects[0];
            ProjectViewModel projectViewModel = new ProjectViewModel()
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                FormulaName = Storage.Formulas.Find(f => f.Id == project.Formula).Name,
                Weight = Storage.Formulas.Find(f => f.Id == project.Formula).WeightInGramms,
                TotalPrice = 100,
                DevelopedByCompany = Storage.Companies.Find(c => c.Id == project.DevelopedByCompany).Name
            };
            projectsList.ItemsSource = new List<ProjectViewModel>() { projectViewModel };

            projectsList.SelectedIndex = 0;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
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
