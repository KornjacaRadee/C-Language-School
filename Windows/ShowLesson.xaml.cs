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
    /// Interaction logic for ShowLesson.xaml
    /// </summary>
    public partial class ShowLesson : Window
    {
        public ShowLesson()
        {
            InitializeComponent();
            AddLessonsData();
        }

        public class ShowProfessorItemStudent
        {
            public string id { get; set; }
            public string professor { get; set; }
            public string date { get; set; }
            public string duration { get; set; }
            public string student { get; set; }
            public string isreserved { get; set; }
        }
        public class ShowProfessorItem
        {
            public string id { get; set; }
            public string professor { get; set; }
            public string date { get; set; }
            public string duration { get; set; }
            public string isreserved { get; set; }
        }



        public void AddLessonsData()
        {
            var lessons = Data.Instance.LessonService.GetActiveLessons();

            foreach (Lesson professor in lessons)
            {
                string reserved = "";
                if (professor.IsReserved)
                {
                    reserved = "Da";
                    professorGridShow.Items.Add(new ShowProfessorItemStudent() { id = professor.Id, professor = professor.Professor.ToString(), date = professor.Date.ToString(), duration = professor.Duration, student = professor.Student.ToString(), isreserved = reserved });
                }
                else
                {
                    reserved = "Ne";
                    professorGridShow.Items.Add(new ShowProfessorItem() { id = professor.Id, professor = professor.Professor.ToString(), date = professor.Date.ToString(), duration = professor.Duration, isreserved = reserved });
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


            try
            {
                Data.Instance.LessonService.Delete(SelectedEmail());
            }
            catch (UserNotFoundException)
            {
                MessageBox.Show("Skola ne postoji");
            }

            professorGridShow.Items.Clear();

            AddLessonsData();








        }


    }
}
