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
    /// Interaction logic for ShowProfessorLessons.xaml
    /// </summary>
    public partial class ShowProfessorLessons : Window
    {
        public ShowProfessorLessons()
        {
            InitializeComponent();
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddLessonsData();

        }
        public class ShowProfessorItemStudent
        {
            public string Id { get; set; }
            public string professor { get; set; }
            public string date { get; set; }
            public string duration { get; set; }
            public string student { get; set; }
            public string isreserved { get; set; }
        }
        public class ShowProfessorItem
        {
            public string Id { get; set; }
            public string professor { get; set; }
            public string date { get; set; }
            public string duration { get; set; }
            public string isreserved { get; set; }
        }



        public void AddLessonsData()
        {
            var lessons = Data.Instance.LessonService.GetActiveLessons();
            var title = this.Title;
            string[] content = title.Split(',');
            string professorString = content[0];
            Professor foundProfessor = Data.Instance.ProfessorService.GetActiveProfessorsByEmail(professorString);

            foreach (Lesson professor in lessons)
            {
                if (professor.Professor.User.Email.Contains( professorString))
                {
                    string reserved = "";
                    if (professor.IsReserved)
                    {
                        reserved = "Da";
                        MessageBox.Show(professor.Id);
                        professorGridShow.Items.Add(new ShowProfessorItemStudent() { Id = professor.Id, professor = professor.Professor.ToString(), date = professor.Date.ToString(), duration = professor.Duration, student = professor.Student.ToString(), isreserved = reserved });
                    }
                    else
                    {
                        reserved = "Ne";
                        professorGridShow.Items.Add(new ShowProfessorItem() { Id = professor.Id, professor = professor.Professor.ToString(), date = professor.Date.ToString(), duration = professor.Duration, isreserved = reserved });
                    }

                }


            }
        }

        public string SelectedEmail()
        {
            var cellInfo = professorGridShow.SelectedCells[0];
            var emailTable = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;
            return emailTable;
        }


        private void updateProfessorBtn_Click(object sender, RoutedEventArgs e)
        {
            LessonUpdate window1 = new LessonUpdate();
            string email = SelectedEmail();
            window1.Title = email + ", " + "update";
            window1.Show();
        }

        private void deleteProfessorBtn_Click(object sender, RoutedEventArgs e)
        {

            Lesson lesi = Data.Instance.LessonService.GetActiveLessonsById(SelectedEmail())[0];
            if (lesi.IsReserved == false)
            {
                try
                {
                    Data.Instance.LessonService.Delete(SelectedEmail());
                }
                catch (UserNotFoundException)
                {
                    MessageBox.Show("Skola ne postoji");
                }
            }
            else {
                MessageBox.Show("ZAKAZAN CAS NE MOZE DA SE BRISE!!!!!!!!!!!!!!!!!!!!");
            }

            professorGridShow.Items.Clear();

            AddLessonsData();








        }
    }
}
