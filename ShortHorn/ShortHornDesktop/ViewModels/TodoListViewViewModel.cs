using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortHorn.Desktop.Services;
using ShortHorn.DataTransferObjects;

namespace ShortHorn.Desktop.ViewModels
{
    public class TodoListViewViewModel : INotifyPropertyChanged
    {
        public TodoListViewViewModel()
        {
            this.TodoLists = new ObservableCollection<TodoListDTO>();
        }

        public ObservableCollection<TodoListDTO> TodoLists { get; set; }

        private string location;
        public string Location
        {
            get { return location; }
            set { location = value; onPropertyChanged(this, "Location"); }
        }

        // Declare the PropertyChanged event
        public event PropertyChangedEventHandler PropertyChanged;

        // OnPropertyChanged will raise the PropertyChanged event passing the
        // source property that is being updated.
        private void onPropertyChanged(object sender, string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
            }
        }

        public async Task<bool> GetTodoLists()
        {
            TodoListService listService = new TodoListService(ConfigurationManager.GetApiBaseAddress(), AppState.ApiLoginToken);
            List<TodoListDTO> lists = await listService.GetAllLists();
            this.TodoLists.Clear();
            foreach (var list in lists)
            {
                this.TodoLists.Add(list);
            }
            return true;
        }
    }
}
