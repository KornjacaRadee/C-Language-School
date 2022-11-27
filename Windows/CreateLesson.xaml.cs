using SR50_2021_POP2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for CreateLesson.xaml
    /// </summary>
    public partial class CreateLesson : Window
    {
        public CreateLesson()
        {
            InitializeComponent();
            ProfessorBox();
            StudentBox();
        }
        public void Testing()
        {
            
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

        private Professor FindProfessor()
        {
            string selectedPrfessorString = (String)professorList.SelectedItem;
            string[] professorSelectionSplit = selectedPrfessorString.Split(',');
            string professorId = professorSelectionSplit[0];
            var professors = Data.Instance.ProfessorService.GetActiveProfessorsByEmail (professorId);
            Professor finalProfessor = professors[0];
            return finalProfessor;
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

        private Student FindStudent()
        {
            string selectedStudentString = (String)studentList.SelectedItem;
            string[] studentSelectionSplit = selectedStudentString.Split(',');
            string studentId = studentSelectionSplit[0];
            var professors = Data.Instance.StudentService.GetActiveStudentsByEmail(studentId);
            Student finalStudent = professors[0];
            return finalStudent;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(datePicker.Text);
            string[] list = datePicker.Text.Split('/');
            string[] times = time.Text.Split(':');
            MessageBox.Show(list[2]+" " + (list[1]) + " " + (list[0]) + " ");
            string ide = id.Text;
            Professor profesor = FindProfessor();
            DateTime dateFinal = new DateTime(int.Parse(list[2]), int.Parse(list[0]), int.Parse(list[1]), int.Parse(times[0]), int.Parse(times[1]), 0);
            string trajanje = duration.Text;

            if (isReserved.IsChecked == true)
            {
                Student student = FindStudent();
                Lesson lesson = new Lesson()
                {
                    Id = ide,
                    Professor = profesor,
                    Date = dateFinal,
                    Duration = trajanje,
                    IsReserved = true,
                    Student = student,
                    IsActive = true
                };
                MessageBox.Show(lesson.ToString());
            }
            else
            {
                Lesson lesson = new Lesson()
                {
                    Id = ide,
                    Professor = profesor,
                    Date = dateFinal,
                    Duration = trajanje,
                    IsReserved = true,
                    IsActive = true
                };
            MessageBox.Show(lesson.ToString());
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
