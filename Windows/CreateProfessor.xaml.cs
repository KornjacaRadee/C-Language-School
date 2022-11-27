using SR50_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
using static SR50_2021_POP2022.Windows.ShowProfessor;

namespace SR50_2021_POP2022.Windows
{
    /// <summary>
    /// Interaction logic for CreateProfessor.xaml
    /// </summary>
    public partial class CreateProfessor : Window
    {
        public CreateProfessor()
        {
            
            InitializeComponent();
            AddressesBox();
            SchoolBox();
        }

        public List<string> CreateLanguageList() 
        {

            string languageString = languages.Text;
            List<string> languagesList = new List<string>(languageString.Split(','));  

            


            return languagesList;
        }

        public void AddressesBox()
        {
            var addresses = Data.Instance.AddressService.GetActiveAddresses();
            foreach (Address address in addresses)
            {
                string toWriteAddress = address.Id +", "+ address.Street +" "+ address.StreetNumber + ", "+ address.City + ", " + address.Country;
                AdressList.Items.Add(toWriteAddress);
            }
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

        private School FindSelectedSchool()
        {
            string selectedSchoolString = (String)SchoolList.SelectedItem;
            string[] schoolSelectionSplit = selectedSchoolString.Split(',');
            string schoolId = schoolSelectionSplit[0];
            var school = Data.Instance.SchoolService.GetSchoolById(schoolId);
            School finalSchool = school[0];


            return finalSchool;
        }

        private Address FindSelectedAddress() { 

            string selectedAddressString = (String)AdressList.SelectedItem;
            string[] addressSelectionSplit = selectedAddressString.Split(',');
            string addressId = addressSelectionSplit[0];
            Address address = Data.Instance.AddressService.GetById(addressId);



            return address;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
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

            ListBoxItem tipItem = (ListBoxItem)tip.SelectedItem;
            string tipStr = tipItem.Content.ToString();
            EUserType tipo = (EUserType)Enum.Parse(typeof(EUserType), tipStr); // TIPO JE TIP KONACAN

            Address adresa1 = FindSelectedAddress();
            School skola1 = FindSelectedSchool();



            User user = new User { 
            Email = emailo,
            Password = passwordo,
            FirstName = firstnameo,
            LastName = lastnameo,
            JMBG = jmbgo,
            Address = adresa1,
            Gender = poli,
            UserType = tipo,
            IsActive = true
            
            };


            List<string> jeziki = new List<string>(CreateLanguageList());
            MessageBox.Show(jeziki[0]);




            if (tipStr.Equals("PROFESOR")) {
                Professor professor = new Professor()
                {
                    User = user,
                    school = skola1,
                    languages = jeziki
                };
                Data.Instance.ProfessorService.Add(professor);
                Data.Instance.Initialize();
                MessageBox.Show("Uspesno dodat profesor!");
                

            }
            else if (tipStr.Equals("STUDENT"))
            {
                Data.Instance.StudentService.Add(user);
                Data.Instance.Initialize();
                MessageBox.Show("Uspesno dodat STUDENT!");
            }


        }

        private void tip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem tipItem = (ListBoxItem)tip.SelectedItem;
            string tipStr = tipItem.Content.ToString();

            if (tipStr.Equals("PROFESOR")) {
                languagesRectangle.Visibility = Visibility.Hidden;
            }
            else
            {
                languagesRectangle.Visibility = Visibility.Visible;
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

    }
}
