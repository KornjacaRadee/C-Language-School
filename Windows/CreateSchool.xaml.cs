using SR50_2021_POP2022.Models;
using SR50_2021_POP2022.Repositories;
using SR50_2021_POP2022.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SR50_2021_POP2022.Windows
{
    /// <summary>
    /// Interaction logic for CreateSchool.xaml
    /// </summary>
    public partial class CreateSchool : Window
    {
        public CreateSchool()
        {
            InitializeComponent();
            AddressesBox();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddressRectangle.Visibility = Visibility.Hidden;
        }

        public List<string> CreateLanguageList()
        {

            string languageString = languages.Text;
            List<string> languagesList = new List<string>(languageString.Split(','));




            return languagesList;
        }

        private void AddressesBox()
        {
            var addresses = Data.Instance.AddressService.GetActiveAddresses();
            foreach (Address address in addresses)
            {
                string toWriteAddress = address.Id + ", " + address.Street + " " + address.StreetNumber + ", " + address.City + ", " + address.Country;
                AddressList.Items.Add(toWriteAddress);
            }
        }

        private Address FindSelectedAddress()
        {

            string selectedAddressString = (String)AddressList.SelectedItem;
            string[] addressSelectionSplit = selectedAddressString.Split(',');
            string addressId = addressSelectionSplit[0];
            Address address = Data.Instance.AddressService.GetById(addressId);



            return address;
        }

        private void AddAddress_click(object sender, RoutedEventArgs e)
        {
            string streeto = street.Text;
            string streetnumbero = streetNumber.Text;
            string cityo = city.Text;
            string countryo = country.Text;
            string ido = id.Text;



            Address adresa = new Address
            {
                Street = streeto,
                StreetNumber = streetnumbero,
                City = cityo,
                Country = countryo,
                Id = ido,
                IsActive = true

            };
            Data.Instance.AddressService.Add(adresa);
            Data.Instance.Initialize();

            AddressList.Items.Clear();
            AddressesBox();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string ide = id.Text;
            string namee = name.Text;
            Address adressa = FindSelectedAddress();
            List<String> langs =    new List<String>(CreateLanguageList());
            School schoola = new School()
            {
                Id = ide,
                Name = namee,
                Address = adressa,
                Languages = langs
            };

            Data.Instance.SchoolService.Add(schoola);
            Data.Instance.Initialize();
            MessageBox.Show("Uspesno dodata skola");
        }
    }
}
