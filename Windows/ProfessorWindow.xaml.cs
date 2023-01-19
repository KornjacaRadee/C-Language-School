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
    /// Interaction logic for ProfessorWindow.xaml
    /// </summary>
    public partial class ProfessorWindow : Window
    {
        public ProfessorWindow()
        {
            InitializeComponent();
        }



            private void Button_Click(object sender, RoutedEventArgs e)
        {
            var title = this.Title;
            string[] content = title.Split(',');
            string professorString = content[0];
            Professor foundProfessor = Data.Instance.ProfessorService.GetActiveProfessorsByEmail(professorString);
            ProfessorUpdate window12 = new ProfessorUpdate();
            MessageBox.Show(foundProfessor.User.Email);
            window12.Title = foundProfessor.User.Email + ","+ "update";
            window12.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CreateLesson window12 = new CreateLesson();
            window12.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var title = this.Title;
            string[] content = title.Split(',');
            string professorString = content[0];
            Professor foundProfessor = Data.Instance.ProfessorService.GetActiveProfessorsByEmail(professorString);
            ShowProfessorLessons window123 = new ShowProfessorLessons();
            window123.Title = foundProfessor.User.Email + "," + "update";
            window123.Show();
        }
    }
}
