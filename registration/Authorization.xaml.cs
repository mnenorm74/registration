using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace registration
{
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void registrate_Click(object sender, RoutedEventArgs e)
        {
            var enteredLogin = login.Text;
            var enteredPassword = password.Password;

            if (enteredLogin == "" || enteredPassword == "")
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            var connectionData = "server=localhost;user=root;database=users;password=admin12345;";
            var checkingData = $"select password from usersdata where login ='{enteredLogin}'";
            var connection = new MySqlConnection(connectionData);
            var check = new MySqlCommand(checkingData, connection);

            connection.Open();
            check.Prepare();
            check.ExecuteNonQuery();

            var dataBaseHash = (string)check.ExecuteScalar();

            if (Password.IsEqualPasswords(enteredPassword, dataBaseHash))
            {
                MessageBox.Show($"Добро пожаловать, {enteredLogin}!");
            }
            else
            {
                MessageBox.Show("Логин или пароль введены неверно. Попробуйте еще раз.");
            }

            connection.Close();
        }       
    }
}
