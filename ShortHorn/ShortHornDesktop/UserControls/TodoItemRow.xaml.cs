using ShortHorn.DataTransferObjects;
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

namespace ShortHorn.Desktop.UserControls
{
    /// <summary>
    /// Interaction logic for TodoItemRow.xaml
    /// </summary>
    public partial class TodoItemRow : UserControl
    {
        public TodoItemRow()
        {
            InitializeComponent();
        }

        private async void checkboxTodoItemComplete_Checked(object sender, RoutedEventArgs e)
        {
            TodoItemDTO item = (TodoItemDTO)this.DataContext;
            TodoItemsService service = new TodoItemsService(ConfigurationManager.GetApiBaseAddress(), AppState.ApiLoginToken);
            bool result = await service.UpdateItem(item);
            if (!result)
            {
                MessageBox.Show("Error while updating todo item status!");
                CheckBox control = (CheckBox)sender;
                control.IsChecked = !control.IsChecked; //Revert changes

            }
        }
    }
}
