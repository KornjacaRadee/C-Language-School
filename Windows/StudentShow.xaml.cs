﻿using SR50_2021_POP2022.CustomExceptions;
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
    /// Interaction logic for StudentShow.xaml
    /// </summary>
    public partial class StudentShow : Window
    {
        public StudentShow()
        {
            InitializeComponent();
            AddProfessorsData();
            Data.Instance.StudentService.GetAll();
        }
        public class ShowProfessorItem
        {
            public string email { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string jmbg { get; set; }
            public string pol { get; set; }
            public string adresa { get; set; }
        }
        public string SelectedEmail()
        {
            var cellInfo = professorGridShow.SelectedCells[0];
            var emailTable = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;
            return emailTable;
        }
        public void AddProfessorsData()
        {
            var professors = Data.Instance.StudentService.GetActiveStudents();

            foreach (Student professor in professors)
            {
                string adresaCela = professor.User.Address.Street + ", " + professor.User.Address.StreetNumber + ", " + professor.User.Address.City;

                
                professorGridShow.Items.Add(new ShowProfessorItem() { email = professor.User.Email, lastName = professor.User.LastName, firstName = professor.User.FirstName, jmbg = professor.User.JMBG, pol = professor.User.Gender.ToString(), adresa = adresaCela });
            }
        }
        private void deleteProfessorBtn_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                Data.Instance.StudentService.Delete(SelectedEmail());
            }
            catch (UserNotFoundException)
            {
                MessageBox.Show("Skola ne postoji");
            }

            professorGridShow.Items.Clear();

            AddProfessorsData();








        }

        private void updateProfessorBtn_Click(object sender, RoutedEventArgs e)
        {
            StudentUpdate window1 = new StudentUpdate();
            string email = SelectedEmail();
            window1.Title = email + ", " + "update";
            window1.Show();
        }


    }
}
