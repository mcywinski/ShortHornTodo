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
using ShortHorn.DataTransferObjects;
using ShortHorn.Desktop.ViewModels.UserControls;

namespace ShortHorn.Desktop.Windows
{
    /// <summary>
    /// Interaction logic for TodoListViewWindow.xaml
    /// </summary>
    public partial class TodoListViewWindow : Window
    {
        private TodoListViewViewModel todoListViewViewModel;

        private CreateTodoListWindow createTodoListWindow = null;

        public TodoListViewWindow()
        {
            InitializeComponent();
            this.todoListViewViewModel = new TodoListViewViewModel();
            this.DataContext = this.todoListViewViewModel;
        }

        private async void ListBox_Loaded(object sender, RoutedEventArgs e)
        {
            await this.todoListViewViewModel.GetTodoLists();
        }

        private async void btnAddList_Click(object sender, RoutedEventArgs e)
        {
            createTodoListWindow = new CreateTodoListWindow();
            createTodoListWindow.ShowDialog();
            createTodoListWindow = null;
            await this.todoListViewViewModel.GetTodoLists();
        }

        private async void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.listBoxTodoLists.SelectedItems.Count != 0)
            {
                TodoListDTO selectedItem = (TodoListDTO)this.listBoxTodoLists.SelectedItems[0];
                this.todoListViewViewModel.CurrentListId = selectedItem.Id;
                await this.todoListViewViewModel.GetTodoItems();
            }
        }

        private void btnAddTodoItem_Click(object sender, RoutedEventArgs e)
        {
            string title = this.txtBoxNewTodoItem.Text;
            this.txtBoxNewTodoItem.Text = string.Empty;

            if (!string.IsNullOrWhiteSpace(title))
            {
                this.todoListViewViewModel.CreateTodoItem(title);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = (ListView)sender;
            if (listView.SelectedItems.Count != 0)
            {
                TodoItemDTO selectedItem = (TodoItemDTO)listView.SelectedItems[0];
                TodoItemDetailsViewModel viewModel = new TodoItemDetailsViewModel(selectedItem);
                viewModel.ItemUpdateCompleted += async () =>
                {
                    await this.todoListViewViewModel.GetTodoItems();
                    
                };
                this.ItemDetailsPane.DataContext = viewModel;
            }
            else
            {
                this.ItemDetailsPane.DataContext = null;
            }
        }
    }
}
