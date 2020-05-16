using BLL;
using BLL.Interfaces;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ProjectManagement
{
    /// <summary>
    /// Interaction logic for MaterialWindow.xaml
    /// </summary>
    public partial class MaterialWindow : Window
    {
        private readonly IUsersService _rUsersService;
        private readonly IMaterialService _rMaterialService;
        private readonly ICountryService _rCountryService;
        private int selectedMaterial = 0;

        public ObservableCollection<BoolStringModel> Countries { get; set; }

        public MaterialWindow()
        {
            InitializeComponent();

            _rUsersService = new UserService(new UserRAMRepository());

            _rMaterialService = new MaterialService(new MaterialRAMRepository(),
                                                    new CountryRAMRepository(),
                                                    new UserRAMRepository());

            _rCountryService = new CountryService(new CountryRAMRepository());

            materialsList.ItemsSource = _rMaterialService.GetMaterialViewModel();

            materialsList.SelectedIndex = 0;

            Countries = new ObservableCollection<BoolStringModel>();
            foreach (var countryName in _rCountryService.GetAllCountries().Select(c => c.Name))
            {
                Countries.Add(new BoolStringModel()
                {
                    IsSelected = false,
                    TheText = countryName
                });
            }

            DataContext = this;
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
            MaterialViewModel material = (MaterialViewModel)materialsList.SelectedItem;
            selectedMaterial = materialsList.SelectedIndex;

            Reset(selectedMaterial);

            if (_rMaterialService.GetCreatorByMaterialId(material.Id) != _rUsersService.GetCurrentUser().Id)
            {
                errormessage.Text = "You can update only materials created by you!";
                return;
            }

            textBoxName.Text = material.Name;
            textBoxDescription.Text = material.Description;
            textBoxPricePerGramm.Text = material.PricePerGramm.ToString();
            _UpdateCountriesListBox(material.BannedInCountries.Split(", ").ToList());

            buttonSaveUpdate.Visibility = Visibility.Visible;
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
            => Reset();

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MaterialViewModel material = (MaterialViewModel)materialsList.SelectedItem;

            if (_rMaterialService.GetCreatorByMaterialId(material.Id) != _rUsersService.GetCurrentUser().Id)
            {
                errormessage.Text = "You can delete only materials created by you!";
                return;
            }

            var result = MessageBox.Show("Delete material ?", "Material",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                if (_rMaterialService.DeleteMaterial(material.Id, out string error))
                    Reset();
                else
                    errormessage.Text = error;
            }
        }

        #region Saves

        private void SaveAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = textBoxName.Text;
            string priceText = textBoxPricePerGramm.Text;
            int price = 0;

            if (string.IsNullOrWhiteSpace(name))
            {
                errormessage.Text = "Name can`t be null or empty";
                textBoxName.Focus();
            }
            else if (!string.IsNullOrWhiteSpace(priceText) && !int.TryParse(priceText, out price))
            {
                errormessage.Text = "Price shoul be a naturel number";
                textBoxPricePerGramm.Focus();
            }
            else
            {
                Material newMaterial = new Material()
                {
                    Id = Guid.NewGuid(),
                    Name = name,
                    Description = textBoxDescription.Text,
                    PricePerGramm = price,
                    CreatedBy = _rUsersService.GetCurrentUser().Id,
                    BannedInCountries = _GetSelectedCountries()
                };

                if (_rMaterialService.AddMaterial(newMaterial, out string error))
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
            string priceText = textBoxPricePerGramm.Text;
            int price = 0;

            if (string.IsNullOrWhiteSpace(name))
            {
                errormessage.Text = "Name can`t be null or empty";
                textBoxName.Focus();
            }
            else if (!string.IsNullOrWhiteSpace(priceText) && !int.TryParse(priceText, out price))
            {
                errormessage.Text = "Price shoul be a natural number";
                textBoxPricePerGramm.Focus();
            }
            else
            {
                var oldMaterial = (MaterialViewModel)materialsList.Items[selectedMaterial];

                Material updateMaterial = new Material()
                {
                    Id = oldMaterial.Id,
                    Name = name,
                    Description = textBoxDescription.Text,
                    PricePerGramm = price,
                    CreatedBy = _rUsersService.GetCurrentUser().Id,
                    BannedInCountries = _GetSelectedCountries()
                };

                if (_rMaterialService.UpdateMaterial(updateMaterial, out string error))
                {
                    errormessage.Text = "Succesfully updated!";
                    Reset(selectedMaterial);
                }
                else
                {
                    errormessage.Text = error;
                }
            }
        }

        #endregion

        #region Helpers

        public void Reset(int selectedItem = 0)
        {
            buttonSaveUpdate.Visibility = Visibility.Hidden;
            buttonSaveAdd.Visibility = Visibility.Hidden;
            textBoxName.Text = null;
            textBoxDescription.Text = null;
            textBoxPricePerGramm.Text = null;
            materialsList.ItemsSource = _rMaterialService.GetMaterialViewModel();
            materialsList.SelectedIndex = selectedItem;
            errormessage.Text = null;

            _ForceClearCountriesCheckList();
        }

        private List<Guid> _GetSelectedCountries()
        {
            var selectedCountries = Countries.Where(c => c.IsSelected == true).ToList();

            List<Guid> countries = new List<Guid>();

            foreach (var country in selectedCountries)
                countries.Add(_rCountryService.GetCountryIdByName(country.TheText));

            return countries;
        }

        private void _ForceClearCountriesCheckList()
        {
            foreach (var country in Countries)
                country.IsSelected = false;

            // force refresh
            countriesCheckList.ItemsSource = null;
            countriesCheckList.ItemsSource = Countries;
        }

        private void _UpdateCountriesListBox(List<string> countries)
        {
            _ForceClearCountriesCheckList();

            foreach (var country in Countries)
            {
                if (countries.Contains(country.TheText))
                    country.IsSelected = true;
            }

            // force refresh
            countriesCheckList.ItemsSource = null;
            countriesCheckList.ItemsSource = Countries;
        }

        #endregion
    }
}
