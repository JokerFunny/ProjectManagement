using BLL;
using BLL.Interfaces;
using DAL;
using Model;
using System;
using System.Windows;

namespace ProjectManagement
{
    /// <summary>
    /// Interaction logic for FormulaWindow.xaml
    /// </summary>
    public partial class FormulaWindow : Window
    {
        private readonly IUsersService _rUsersService;
        private readonly IFormulaService _rFormulaService;
        private int selectedProject = 0;

        public FormulaWindow()
        {
            InitializeComponent();

            _rUsersService = new UserService(new UserRAMRepository());

            _rFormulaService = new FormulaService(new FormulaRAMRepository(), 
                                                  new CompanyRAMRepository(),
                                                  new UserRAMRepository(), 
                                                  new MaterialRAMRepository());

            Reset();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
            => Close();

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Reset();

            buttonSaveAdd.Visibility = Visibility.Visible;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            FormulaViewModel formula = (FormulaViewModel)formulasList.SelectedItem;

            if (_rFormulaService.GetFormulaById(formula.Id).CreatedBy != _rUsersService.GetCurrentUser().Id)
            {
                errormessage.Text = "You can only update formulas created by you!";
                return;
            }

            selectedProject = formulasList.SelectedIndex;

            Reset(selectedProject);

            textBoxName.Text = formula.Name;
            textBoxDescription.Text = formula.Description;
            textBoxWeight.Text = formula.WeightInGramms.ToString();

            buttonSaveUpdate.Visibility = Visibility.Visible;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
            => Reset();

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            FormulaViewModel formula = (FormulaViewModel)formulasList.SelectedItem;

            if (_rFormulaService.GetFormulaById(formula.Id).CreatedBy != _rUsersService.GetCurrentUser().Id)
            {
                errormessage.Text = "You can only delete formulas created by you!";
                return;
            }

            var result = MessageBox.Show("Delete formula ?", "Formula",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (_rFormulaService.DeleteFormula(formula.Id, out string error))
                {
                    Reset();
                    errormessage.Text = "Succesfully deleted!";
                }
                else
                    errormessage.Text = error;
            }
        }

        private void SaveAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxName.Text;
            string description = textBoxDescription.Text;
            string weightText = textBoxWeight.Text;
            decimal weight = 0M;

            if (string.IsNullOrWhiteSpace(name))
            {
                errormessage.Text = "Name can`t be null or empty";
                textBoxName.Focus();
            }
            else if (!string.IsNullOrWhiteSpace(weightText) && !decimal.TryParse(weightText, out weight) && weight > 0)
            {
                errormessage.Text = "Price shoul be a naturel number";
                textBoxWeight.Focus();
            }
            else
            {
                Formula newFormula = new Formula()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = description,
                    WeightInGramms = weight,
                    CreatedBy = _rUsersService.GetCurrentUser().Id
                };

                if (_rFormulaService.AddFormula(newFormula, out string error))
                {
                    Reset();
                    errormessage.Text = "Succesfully added!";
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
            string weightText = textBoxWeight.Text;
            decimal weight = 0M;

            if (string.IsNullOrWhiteSpace(name))
            {
                errormessage.Text = "Name can`t be null or empty";
                textBoxName.Focus();
            }
            else if (!string.IsNullOrWhiteSpace(weightText) && !decimal.TryParse(weightText, out weight) && weight > 0)
            {
                errormessage.Text = "Price shoul be a naturel number";
                textBoxWeight.Focus();
            }
            else
            {
                var oldFormula = (FormulaViewModel)formulasList.Items[selectedProject];

                Formula newFormula = new Formula()
                {
                    Id = oldFormula.Id,
                    Name = name,
                    Description = description,
                    WeightInGramms = weight,
                    CreatedBy = _rUsersService.GetCurrentUser().Id
                };

                if (_rFormulaService.UpdateFormula(newFormula, out string error))
                {
                    Reset();
                    errormessage.Text = "Succesfully added!";
                }
                else
                {
                    errormessage.Text = error;
                }
            }
        }

        public void Reset(int selectedItem = 0)
        {
            buttonSaveUpdate.Visibility = Visibility.Hidden;
            buttonSaveAdd.Visibility = Visibility.Hidden;
            textBoxName.Text = null;
            textBoxDescription.Text = null;
            textBoxWeight.Text = null;
            formulasList.ItemsSource = _rFormulaService.GetFormulasViewModel();
            formulasList.SelectedIndex = selectedItem;
            errormessage.Text = null;
        }
    }
}
