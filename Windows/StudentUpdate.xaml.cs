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
    /// Interaction logic for StudentUpdate.xaml
    /// </summary>
    public partial class StudentUpdate : Window
    {
        public StudentUpdate()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProfessor();
            AddressesBox();
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
            Student foundProfessor = Data.Instance.StudentService.GetActiveStudentsByEmail(professorString);
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


            if (AdressList.SelectedIndex != -1)
            {
                Address foundAddress = FindSelectedAddress();
                foundProfessor.User.Address = foundAddress;
            }
            else
            {

            }

            Data.Instance.StudentService.Update(emaila, foundProfessor);
            MessageBox.Show("Student uspesno izmenjen");



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
        public void LoadProfessor()
        {
            var title = this.Title;
            string[] content = title.Split(',');
            string professorString = content[0];
            Student foundProfessor = Data.Instance.StudentService.GetActiveStudentsByEmail(professorString);
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
           


        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddressRectangle.Visibility = Visibility.Hidden;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateProfessor();
        }
    }
}
