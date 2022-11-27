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
    /// Interaction logic for LessonUpdate.xaml
    /// </summary>
    public partial class LessonUpdate : Window
    {
        public LessonUpdate()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadProfessor();
            ProfessorBox();
            StudentBox();

        }
        private Student FindStudent()
        {
            string selectedStudentString = (String)studentList.SelectedItem;
            string[] studentSelectionSplit = selectedStudentString.Split(',');
            string studentId = studentSelectionSplit[0];
            var professors = Data.Instance.StudentService.GetActiveStudentsByEmail(studentId);
            Student finalStudent = professors[0];
            return finalStudent;
        }
        private Professor FindProfessor()
        {
            string selectedPrfessorString = (String)professorList.SelectedItem;
            string[] professorSelectionSplit = selectedPrfessorString.Split(',');
            string professorId = professorSelectionSplit[0];
            var professors = Data.Instance.ProfessorService.GetActiveProfessorsByEmail(professorId);
            Professor finalProfessor = professors[0];
            return finalProfessor;
        }
        public void LoadProfessor()
        {
            var title = this.Title;
            string[] content = title.Split(',');
            string professorString = content[0];
            Lesson foundProfessor = Data.Instance.LessonService.GetActiveLessonsById(professorString)[0];
            id.Text = foundProfessor.Id;
            duration.Text = foundProfessor.Duration;
            if (foundProfessor.IsReserved)
            {
                isReserved.IsChecked = true;
            }
            


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                string ide = id.Text;
                string trajanje = duration.Text;
                var title = this.Title;
                string[] content = title.Split(',');
                string professorString = content[0];
                Lesson foundProfessor = Data.Instance.LessonService.GetActiveLessonsById(professorString)[0];
            string iddd = foundProfessor.Id;
            string updateId = foundProfessor.Id;
            if (datePicker.SelectedDate != null)
            {   

                string[] list = datePicker.Text.Split('/');
                string[] times = time.Text.Split(':');
                DateTime dateFinal = new DateTime(int.Parse(list[2]), int.Parse(list[0]), int.Parse(list[1]), int.Parse(times[0]), int.Parse(times[1]), 0);


                if (isReserved.IsChecked == true && studentList.SelectedIndex == -1)
                {

                    foundProfessor.Id = ide;
                    foundProfessor.Date = dateFinal;
                    foundProfessor.Duration = trajanje;
                    foundProfessor.IsReserved = true;
                    foundProfessor.IsActive = true;

                }
                else if(isReserved.IsChecked == true && studentList.SelectedIndex != -1)
                {
                    Student student = FindStudent();
                    foundProfessor.Id = ide;
                    foundProfessor.Date = dateFinal;
                    foundProfessor.Duration = trajanje;
                    foundProfessor.IsReserved = true;
                    foundProfessor.Student = student;
                    foundProfessor.IsActive = true;

                }
            }
            else
            {
                if (isReserved.IsChecked == true && studentList.SelectedIndex != -1)
                {
                    Student student = FindStudent();
                    foundProfessor.Id = ide;
                    foundProfessor.Duration = trajanje;
                    foundProfessor.IsReserved = true;
                    foundProfessor.IsActive = true;
                    foundProfessor.Student = student;

                }
                else
                {
                    foundProfessor.Id = ide;
                    foundProfessor.Duration = trajanje;
                    
                    foundProfessor.IsReserved = true;
                    foundProfessor.IsActive = true;


                }

            }
            if(professorList.SelectedIndex != -1)
            {
                Professor profesorr = FindProfessor();
                foundProfessor.Professor = profesorr;
            }

            Data.Instance.LessonService.Update(iddd, foundProfessor);
            MessageBox.Show("Uspesno izmenjen cas");
        }

        public void ProfessorBox()
        {
            var professors = Data.Instance.ProfessorService.GetActiveProfessors();
            foreach (Professor professor in professors)
            {
                string toWriteProfessor = professor.User.Email + ", " + professor.User.FirstName + " " + professor.User.LastName;
                professorList.Items.Add(toWriteProfessor);
            }
        }
        public void StudentBox()
        {
            var students = Data.Instance.StudentService.GetActiveStudents();
            foreach (Student student in students)
            {
                string toWriteStudent = student.User.Email + ", " + student.User.FirstName + " " + student.User.LastName;
                studentList.Items.Add(toWriteStudent);
            }
        }
        private void isReserved_Checked(object sender, RoutedEventArgs e)
        {
            studentRectangle.Visibility = Visibility.Hidden;
        }
        private void isReserved_Unchecked(object sender, RoutedEventArgs e)
        {
            studentRectangle.Visibility = Visibility.Visible;
        }
    }
}
