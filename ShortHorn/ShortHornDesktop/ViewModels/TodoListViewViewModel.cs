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
    public class TodoListViewViewModel : BaseViewModel
    {
        public TodoListViewViewModel()
        {
            this.TodoLists = new ObservableCollection<TodoListDTO>();
            this.TodoItems = new ObservableCollection<TodoItemDTO>();
        }

        public ObservableCollection<TodoListDTO> TodoLists { get; set; }
        public ObservableCollection<TodoItemDTO> TodoItems { get; set; }

        private string location;
        public string Location
        {
            get { return location; }
            set { location = value; onPropertyChanged(this, "Location"); }
        }

        public int CurrentListId { get; set; }

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

        public async Task GetTodoItems()
        {
            TodoItemsService itemsService = new TodoItemsService(ConfigurationManager.GetApiBaseAddress(), AppState.ApiLoginToken);
            List<TodoItemDTO> items = await itemsService.GetAllItems(CurrentListId);
            this.TodoItems.Clear();
            foreach (var item in items)
            {
                this.TodoItems.Add(item);
            }
        }

        public async void CreateTodoItem(string title)
        {
            TodoItemsService itemsService = new TodoItemsService(ConfigurationManager.GetApiBaseAddress(), AppState.ApiLoginToken);
            bool result = await itemsService.CreateItem(this.CurrentListId, title);
            await this.GetTodoItems();
        }
    }
}
