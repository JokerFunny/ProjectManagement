﻿using BLL;
using BLL.Interfaces;
using DAL;
using Model;
using System;
using System.Windows;

namespace ProjectManagement
{
    /// <summary>
    /// Interaction logic for ProjectWindow.xaml
    /// </summary>
    public partial class ProjectWindow : Window
    {
        private readonly IProjectService _rProjectService;
        private readonly IUsersService _rUsersService;
        private readonly IFormulaService _rFormulaService;
        private readonly ICompanyService _rCompanyService;
        private int selectedProject = 0;
        private int selectedItemFormula = 0;

        public ProjectWindow()
        {
            InitializeComponent();

            _rUsersService = new UserService(new UserRAMRepository());

            _rProjectService = new ProjectService(new ProjectRAMRepository(),
                                                  new FormulaRAMRepository(),
                                                  new CompanyRAMRepository(),
                                                  new MaterialRAMRepository());

            _rFormulaService = new FormulaService(new FormulaRAMRepository());

            _rCompanyService = new CompanyService(new CompanyRAMRepository());

            Reset();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Reset();

            buttonSaveAdd.Visibility = Visibility.Visible;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            ProjectViewModel project = (ProjectViewModel)projectsList.SelectedItem;

            selectedProject = projectsList.SelectedIndex;
            selectedItemFormula = comboBoxFormula.Items.IndexOf(project.FormulaName);

            Reset(selectedProject, selectedItemFormula);

            textBoxName.Text = project.Name;
            textBoxDescription.Text = project.Description;

            buttonSaveUpdate.Visibility = Visibility.Visible;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
            => Reset();

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            ProjectViewModel project = (ProjectViewModel)projectsList.SelectedItem;

            if (_rCompanyService.GetCompanyIdByName(project.DevelopedByCompany) != _rUsersService.GetCurrentUser().CompanyId)
            {
                errormessage.Text = "You can only delete projects created in your company!";
                return;
            }

            var result = MessageBox.Show("Delete project ?", "Project",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (_rProjectService.DeleteProject(project.Id, out string error))
                    Reset();
                else
                    errormessage.Text = error;
            }
        }

        private void SaveAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxName.Text;
            string description = textBoxDescription.Text;
            string selectedFormulaName = comboBoxFormula.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(name))
            {
                errormessage.Text = "Name can`t be null or empty";
                textBoxName.Focus();
            }
            else if (string.IsNullOrWhiteSpace(selectedFormulaName))
            {
                errormessage.Text = "You must select formula, that will be a base for your project!";
                comboBoxFormula.Focus();
            }
            else
            {
                Project newProject = new Project()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = description,
                    Formula = _rFormulaService.GetFormulaIdByName(selectedFormulaName),
                    DevelopedByCompany = _rUsersService.GetCurrentUser().CompanyId
                };

                if (_rProjectService.AddPorject(newProject, out string error))
                {
                    errormessage.Text = "Succesfully added!";
                    Reset();
                }
                else
                {
                    errormessage.Text = error;
                }
            }
        }

        private void SaveUpdate_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxName.Text;
            string description = textBoxDescription.Text;
            string selectedFormulaName = comboBoxFormula.SelectedItem.ToString();

            if (string.IsNullOrWhiteSpace(name))
            {
                errormessage.Text = "Name can`t be null or empty";
                textBoxName.Focus();
            }
            else if (string.IsNullOrWhiteSpace(selectedFormulaName))
            {
                errormessage.Text = "You must select formula, that will be a base for your project!";
                comboBoxFormula.Focus();
            }
            else
            {
                var oldProject = (ProjectViewModel)projectsList.Items[selectedProject];

                Project newProject = new Project()
                {
                    Id = oldProject.Id,
                    Name = name,
                    Description = description,
                    Formula = _rFormulaService.GetFormulaIdByName(selectedFormulaName),
                    DevelopedByCompany = oldProject.Id
                };

                if (_rProjectService.UpdateProject(newProject, out string error))
                {
                    errormessage.Text = "Succesfully added!";
                    Reset();
                }
                else
                {
                    errormessage.Text = error;
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
            => Close();

        public void Reset(int selectedItem = 0, int selectedFormula = 0)
        {
            buttonSaveUpdate.Visibility = Visibility.Hidden;
            buttonSaveAdd.Visibility = Visibility.Hidden;
            textBoxName.Text = null;
            textBoxDescription.Text = null;
            comboBoxFormula.ItemsSource = _rFormulaService.GetAllFormulasNames();
            comboBoxFormula.SelectedIndex = selectedFormula;
            projectsList.ItemsSource = _rProjectService.GetProjectViewModelByCompany(_rUsersService.GetCurrentUser().CompanyId);
            projectsList.SelectedIndex = selectedItem;
            errormessage.Text = null;
        }
    }
}
