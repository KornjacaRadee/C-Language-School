using SR50_2021_POP2022.Models;
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
    /// Interaction logic for SchoolUpdate.xaml
    /// </summary>
    public partial class SchoolUpdate : Window
    {
        public SchoolUpdate()
        {
            InitializeComponent();
            AddressesBox();
        }
        public List<string> CreateLanguageList()
        {

            string languageString = languages.Text;
            List<string> languagesList = new List<string>(languageString.Split(','));




            return languagesList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddressRectangle.Visibility = Visibility.Hidden;
        }
        public void AddressesBox()
        {
            var addresses = Data.Instance.AddressService.GetActiveAddresses();
            foreach (Address address in addresses)
            {
                string toWriteAddress = address.Id + ", " + address.Street + " " + address.StreetNumber + ", " + address.City + ", " + address.Country;
                AddressList.Items.Add(toWriteAddress);
            }
        }
        
        public void LoadSchool()
        {
            var title = this.Title;
            string[] content = title.Split(',');
            string professorString = content[0];
            School foundSchool = Data.Instance.SchoolService.GetSchoolById(professorString)[0];
            id.Text = foundSchool.Id;
            name.Text = foundSchool.Name;
            List<string> langs = foundSchool.Languages;
            string languagesa = "";
            foreach(string language in langs)
            {
                languagesa += language + ", ";
            }
            languages.Text = languagesa;
            
        }
        private Address FindSelectedAddress()
        {

            string selectedAddressString = (String)AddressList.SelectedItem;
            string[] addressSelectionSplit = selectedAddressString.Split(',');
            string addressId = addressSelectionSplit[0];
            Address address = Data.Instance.AddressService.GetById(addressId);



            return address;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSchool();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // adresa
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string ide = id.Text;
            string namee = name.Text;
            List<String> langus = CreateLanguageList();
            var title = this.Title;
            string[] content = title.Split(',');
            string professorString = content[0];
            School foundSchool = Data.Instance.SchoolService.GetSchoolById(professorString)[0];
            string IDD = foundSchool.Id;

            foundSchool.Name = namee;
            foundSchool.Id = ide;
            foundSchool.Languages = langus;
            
            if (AddressList.SelectedIndex != -1)
            {
                Address adresica = FindSelectedAddress();
                foundSchool.Address = adresica;

            }
            Data.Instance.SchoolService.Update(IDD, foundSchool);



        }
    }
}
