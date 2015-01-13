using ShortHorn.DataTransferObjects;
using ShortHorn.Desktop.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ShortHorn.Desktop.ViewModels
{
    /// <summary>
    /// View model for managing tasks and items at main application window.
    /// </summary>
    public class TodoListViewViewModel : BaseViewModel
    {
        #region properties

        /// <summary>
        /// Available todo lists.
        /// </summary>
        public ObservableCollection<TodoListDTO> TodoLists { get; set; }

        /// <summary>
        /// Available todo items of currently selected todo list.
        /// </summary>
        public ObservableCollection<TodoItemDTO> TodoItems { get; set; }

        /// <summary>
        /// Indicates currently selected list ID.
        /// </summary>
        public int CurrentListId { get; set; }

        /// <summary>
        /// Indicates current user's location.
        /// </summary>
        public string Location
        {
            get { return location; }
            set { location = value; onPropertyChanged(this, "Location"); }
        }

        private string location;

        #endregion

        /// <summary>
        /// The constructor.
        /// </summary>
        public TodoListViewViewModel()
        {
            this.TodoLists = new ObservableCollection<TodoListDTO>();
            this.TodoItems = new ObservableCollection<TodoItemDTO>();
        }

        #region methods

        /// <summary>
        /// Fetches all todo lists from the server.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Fetches all todo items belonging to currently selected todo list.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Creates a new todo item and saves it at server.
        /// </summary>
        /// <param name="title"></param>
        public async void CreateTodoItem(string title)
        {
            TodoItemsService itemsService = new TodoItemsService(ConfigurationManager.GetApiBaseAddress(), AppState.ApiLoginToken);
            bool result = await itemsService.CreateItem(this.CurrentListId, title);
            await this.GetTodoItems();
        }

        #endregion
    }
}
