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
using ShortHorn.Desktop.Services;
using ShortHorn.Desktop.Windows;
using ShortHorn.DataTransferObjects;

namespace ShortHorn.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TodoListViewWindow windowTodos;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            UserService userService = new UserService(ConfigurationManager.GetApiBaseAddress());
            string token = await userService.Login(txtBoxLogin.Text, txtBoxPassword.Password);
            if (string.IsNullOrEmpty(token))
            {
                MessageBox.Show("Login or password is incorrect!");
            }
            else
            {
                AppState.ApiLoginToken = token;
                this.Visibility = Visibility.Collapsed;

                UserDTO details = await userService.GetUserDetails(token);
                AppState.Country = details.Country;
                AppState.City = details.City;

                windowTodos = new TodoListViewWindow();
                windowTodos.Show();
            }
        }
    }
}
