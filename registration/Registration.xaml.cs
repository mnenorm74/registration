using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace registration
{
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            var enteredLogin = login.Text;
            var enteredPassword = password.Password;
            var enteredCopyPassword = passwordCopy.Password;

            if (enteredLogin == "" || enteredPassword == "" || enteredCopyPassword == "")
            {
                MessageBox.Show("Заполните все поля");
                return;
            }

            var connectionData = "server=localhost;user=root;database=users;password=admin12345;";
            var connection = new MySqlConnection(connectionData);
            connection.Open();

            var checkData = $"select login from usersdata where login = '{enteredLogin}'";
            var check = new MySqlCommand(checkData, connection);
            check.Prepare();
            check.ExecuteNonQuery();
            
            if (check.ExecuteScalar() != null)
            {
                MessageBox.Show("Такой логин занят!");
                return;
            }

            if (enteredPassword != enteredCopyPassword)
            {
                MessageBox.Show("Введенные пароли не совпадают. Попробуйте еще раз.");
                password.Clear();
                passwordCopy.Clear();
                return;
            }

            var hash = Password.CalculateMD5Hash(enteredPassword);

            var addingData = $"insert into usersdata set login='{enteredLogin}', password='{hash}'";
            var add = new MySqlCommand(addingData, connection);
            add.Prepare();
            add.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Пользователь зарегистрирован");
        }
    }
}
