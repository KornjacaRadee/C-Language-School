using SR50_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ProfessorUpdate.xaml
    /// </summary>
    public partial class ProfessorUpdate : Window
    {
        public ProfessorUpdate()
        {
            InitializeComponent();


        }

        //OVDE SAM STAO NAMESTIO PASSOVANJE EMAILA
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProfessor();
            AddressesBox();
            SchoolBox();
        }

        private School FindSelectedSchool()
        {
            string selectedSchoolString = (String)SchoolList.SelectedItem;
            string[] schoolSelectionSplit = selectedSchoolString.Split(',');
            string schoolId = schoolSelectionSplit[0];
            var school = Data.Instance.SchoolService.GetSchoolById(schoolId);
            School finalSchool = school[0];


            return finalSchool;
        }

        private Address FindSelectedAddress()
        {

            string selectedAddressString = (String)AdressList.SelectedItem;
            string[] addressSelectionSplit = selectedAddressString.Split(',');
            string addressId = addressSelectionSplit[0];
            Address address = Data.Instance.AddressService.GetById(addressId);



            return address;
        }

        public void UpdateProfessor()
        {
            var title = this.Title;
            string[] content = title.Split(',');
            string professorString = content[0];
            Professor foundProfessor = Data.Instance.ProfessorService.GetActiveProfessorsByEmail(professorString);
            string emailo = email.Text;
            string passwordo = password.Text;
            string firstnameo = firstname.Text;
            string lastnameo = lastname.Text;
            string jmbgo = jmbg.Text;


            var address = Data.Instance.AddressService.GetActiveAddresses();




            // listbox conv pol
            ListBoxItem polItem = (ListBoxItem)pol.SelectedItem;
            string polStr = polItem.Content.ToString();
            EGender poli = (EGender)Enum.Parse(typeof(EGender), polStr); // POLI JE POL KONACAN
            string emaila = foundProfessor.User.Email;
            

                foundProfessor.User.Email = emailo;
                foundProfessor.User.Password = passwordo;
                foundProfessor.User.FirstName = firstnameo;
            foundProfessor.User.LastName = lastnameo;
                foundProfessor.User.JMBG = jmbgo;
                foundProfessor.User.Gender = poli;
            

            if(AdressList.SelectedIndex != -1)
            {
                Address foundAddress = FindSelectedAddress();
                foundProfessor.User.Address = foundAddress;
            }
            else
            {

            }
            if (SchoolList.SelectedIndex != -1)
            {
                School schoolFound = FindSelectedSchool();
                foundProfessor.school = schoolFound;
            }
            else
            {

            }

            Data.Instance.ProfessorService.Update(emaila, foundProfessor);
            MessageBox.Show("Profesor uspesno izmenjen");



        }
        public void LoadProfessor()
        {
            var title = this.Title;
            string[] content = title.Split(',');
            string professorString = content[0];
            Professor foundProfessor = Data.Instance.ProfessorService.GetActiveProfessorsByEmail(professorString);
            email.Text = foundProfessor.User.Email;
            password.Text = foundProfessor.User.Password;
            firstname.Text = foundProfessor.User.FirstName;
            lastname.Text = foundProfessor.User.LastName;
            jmbg.Text = foundProfessor.User.JMBG;
            string polo = foundProfessor.User.Gender.ToString();
            if (polo.Equals("ZENSKO"))
            {

            pol.SelectedIndex = 1;

            }
            else
            {
                pol.SelectedIndex = 0;
            }
            List<String> languagess = foundProfessor.languages;
            string jeziki = "";
            foreach(string language in languagess)
            {
                jeziki += language + ",";
            }
            languages.Text = jeziki;

            
        }

        public void SchoolBox()
        {
            var schools = Data.Instance.SchoolService.GetActiveSchools();
            foreach (School school in schools)
            {
                string toWriteAddress = school.Id + ", " + school.Name + ", " + school.Address.ToString();
                SchoolList.Items.Add(toWriteAddress);
            }
        }
        public void AddressesBox()
        {
            var addresses = Data.Instance.AddressService.GetActiveAddresses();
            foreach (Address address in addresses)
            {
                string toWriteAddress = address.Id + ", " + address.Street + " " + address.StreetNumber + ", " + address.City + ", " + address.Country;
                AdressList.Items.Add(toWriteAddress);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddressRectangle.Visibility = Visibility.Hidden;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateProfessor();
        }



        private void AddAddress_click(object sender, RoutedEventArgs e)
        {
            string streeto = street.Text;
            string streetnumbero = streetnumber.Text;
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

            AdressList.Items.Clear();
            AddressesBox();


        }
    }
}
