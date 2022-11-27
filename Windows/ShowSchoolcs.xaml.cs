using SR50_2021_POP2022.CustomExceptions;
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
    /// Interaction logic for ShowSchoolcs.xaml
    /// </summary>
    public partial class ShowSchoolcs : Window
    {
        public ShowSchoolcs()
        {
            InitializeComponent();
            AddSchoolsData();
        }


        public class ShowProfessorItem
        {
            public string id { get; set; }
            public string name { get; set; }
            public string address { get; set; }
            public string languages { get; set; }
        }
        public string SelectedId()
        {
            var cellInfo = professorGridShow.SelectedCells[0];
            var idTable = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;
            return idTable;
        }
        public void AddSchoolsData()
        {
            var schools = Data.Instance.SchoolService.GetActiveSchools();


            foreach (School school in schools)
            {
                string adresaCela = school.Address.Street + ", " + school.Address.StreetNumber + ", " + school.Address.City;
                List<string> languagess = school.Languages;
                string ceoJezik = "";
                foreach(string language in languagess)
                {
                    ceoJezik += language + ", ";
                }



                professorGridShow.Items.Add(new ShowProfessorItem() { id = school.Id, name = school.Name, address = adresaCela, languages = ceoJezik});
            }
        }

        private void deleteSchoolBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Data.Instance.SchoolService.Delete(SelectedId());
            }
            catch (UserNotFoundException)
            {
                MessageBox.Show("Skola ne postoji");
            }

            professorGridShow.Items.Clear();

            AddSchoolsData();
        }

        private void updateSchoolBtn_Click(object sender, RoutedEventArgs e)
        {
            SchoolUpdate window1 = new SchoolUpdate();
            window1.Title = SelectedId() + ", " + "Update";
            window1.Show();
        }
    }
}
