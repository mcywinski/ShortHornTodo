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
using ShortHorn.Desktop.ViewModels;

namespace ShortHorn.Desktop.Windows
{
    /// <summary>
    /// Interaction logic for CreateTodoListWindow.xaml
    /// </summary>
    public partial class CreateTodoListWindow : Window
    {
        private CreateTodoListViewModel viewModel = null;

        public CreateTodoListWindow()
        {
            InitializeComponent();
            this.viewModel = new CreateTodoListViewModel();
            this.DataContext = this.viewModel;

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.viewModel.TodoListName))
            {
                await this.viewModel.CreateTodoList();
                this.Close();
            }
            else
            {
                MessageBox.Show("List must have a name!");
            }
        }
    }
}
