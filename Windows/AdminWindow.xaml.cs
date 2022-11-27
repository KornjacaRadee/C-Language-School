using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            
        }

        private void createProfessor_Click(object sender, RoutedEventArgs e)
        {
            CreateProfessor window1 = new CreateProfessor();
            window1.Show();

        }
        private void showProfessor_Click(object sender, RoutedEventArgs e)
        {

            ShowProfessor window1 = new ShowProfessor();
            window1.Show();

        }
        private void showStudents_Click(object sender, RoutedEventArgs e)
        {

            StudentShow window1 = new StudentShow();
            window1.Show();

        }
        private void showLessons_Click(object sender, RoutedEventArgs e)
        {

            ShowLesson window1 = new ShowLesson();
            window1.Show();

        }

        private void showSchools_Click(object sender, RoutedEventArgs e)
        {

            ShowSchoolcs window1 = new ShowSchoolcs();
            window1.Show();

        }

        private void createClass_Click(object sender, RoutedEventArgs e)
        {

            CreateLesson window1 = new CreateLesson();
            window1.Show();

        }
        private void createSchool_Click(object sender, RoutedEventArgs e)
        {

            CreateSchool window1 = new CreateSchool();
            window1.Show();

        }


    }
}
