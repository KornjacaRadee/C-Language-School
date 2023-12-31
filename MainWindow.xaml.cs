﻿using SR50_2021_POP2022.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SR50_2021_POP2022.Models;
using SR50_2021_POP2022.CustomExceptions;
using System.Net;
using SR50_2021_POP2022.Windows;
using SR50_2021_POP2022.Repositories;
using SR50_2021_POP2022.Models;
using SR50_2021_POP2022.Services;
using static SR50_2021_POP2022.Windows.ShowProfessor;

namespace SR50_2021_POP2022
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Data.Instance.Initialize();
            Data.Instance.UserService.GetAll();
            Data.Instance.StudentService.GetAll();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string jmbgBox = jmbg.Text;
            string passwordBox = password.Text;
            var foundOne = Data.Instance.UserService.GetUserByJMBG(jmbgBox);

            User found = foundOne;
            if(found.Password.Equals(passwordBox))
            {
                if (found.UserType.ToString().Equals("ADMINISTRATOR")) 
                {
                    MessageBox.Show("Ulogovan admin");

                    AdminWindow window1 = new AdminWindow();
                    window1.Show();
                    window1.Title = "Admin panel";
                }else if (found.UserType.ToString().Equals("PROFESOR")) 
                {
                    ProfessorWindow window1 = new ProfessorWindow();
                    window1.Show();
                    window1.Title = found.Email + ",";
                }
                else
                {
                    MessageBox.Show("Ulogovan student");
                    ProfessorWindow window1 = new ProfessorWindow();
                    window1.Show();
                    window1.Title = found.Email+",";
                }
            }
            else
            {
                MessageBox.Show("Pogresni podaci");
            }
        }

        
        private void Unregistered_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(Data.Instance.ProfessorService.GetById("profesor@gmail.com").ToString());
            //MessageBox.Show(Data.Instance.AddressService.GetById("1").ToString());


            UnregisteredPanel window1 = new UnregisteredPanel();
            window1.Show();
            window1.Title = "Neregistrovani korisnik panel";
            
        }
    }
}
